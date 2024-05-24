using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
using Report.Application.Dto.Include;
using Report.Application.Enum;
using Report.Application.Model;
using Report.Domain;
using Report.Persistance;
using System.Collections;
using System.Globalization;
using System.Reflection.Metadata;

namespace Report.Application.Business
{
    public class TagValueBusiness : ITagValueBusiness
    {
        private readonly DataContext dataContext;

        public TagValueBusiness(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<TagValue>> GetAll()
        {
            return await dataContext.TagValue.ToListAsync();
        }

        /// <summary>
        /// جمع آوری دیتای مربوطه به فیلتر درخواستی برای نمایش گراف
        /// </summary>
        /// <param name="filterParameter"></param>
        /// <returns></returns>
        public async Task<List<ChartModel>> GetGraphDataByFilter(FilterParameter filterParameter)
        {
            var hallTags = await GetHallTags(filterParameter, false);

            var tagValue = await dataContext.TagValue.Where
                (c =>
                 hallTags.Select(c => c.Key).Contains(c.Id) &&
                 c.Timestamp <= filterParameter.EndDate.AddDays(1) &&
                 c.Timestamp >= filterParameter.StartDate &&
                 c.value != 0
                ).ToListAsync();

            return await CalculateDate(tagValue, hallTags, filterParameter.Period);
        }

        /// <summary>
        /// محاسبه ی دیتای مصرفی برای هر کنتور با توجه به فیلتر ورودی
        /// </summary>
        /// <param name="filterParameter"></param>
        /// <returns></returns>
        public async Task<int> GetCalculatedAssumptionByFilter(FilterParameter filterParameter)
        {
            var hallTags = await GetHallTags(filterParameter, true);

            var tagValue = dataContext.TagValue.Where
                (c =>
                 hallTags.Select(c => c.Key).Contains(c.Id) &&
                 c.Timestamp <= filterParameter.EndDate.AddDays(1) &&
                 c.Timestamp >= filterParameter.StartDate &&
                 c.value != 0
                ).ToList();

            Single? totalFirst = 0;
            Single? totalEnd = 0;

            tagValue.GroupBy(c => c.Id).ToList().ForEach(hallTag =>
            {
                var orderedResult = hallTag.OrderBy(c => c.Timestamp).ToList();
                totalFirst += orderedResult.First().value;
                totalEnd += orderedResult.Last().value;
            });

            return ((int)totalEnd - (int)totalFirst);
        }

        /// <summary>
        /// تحویل تک های مربوطه با توجه به سالن و فیلتر درخواستی
        /// </summary>
        /// <param name="filterParameter"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, int>> GetHallTags(FilterParameter filterParameter, bool IsAssupmtion)
        {
            var endsString = string.Empty;
            switch (filterParameter.Meter)
            {
                case Meter.Water:
                    endsString = IsAssupmtion ? EndStringAssumption.Water : EndStringPower.Water;
                    break;
                case Meter.Gas:
                    endsString = IsAssupmtion ? EndStringAssumption.Gas : EndStringPower.Gas;
                    break;
                case Meter.CompresAir:
                    endsString = IsAssupmtion ? EndStringAssumption.CompresAir : EndStringPower.CompresAir;
                    break;
                case Meter.Electricity:
                    endsString = IsAssupmtion ? EndStringAssumption.Electricity : EndStringPower.Electricity;
                    break;
                default:
                    break;
            }

            Queue my_queue = new Queue();
            int hallType = 0;
            int unitId = 0;
            int hallCode = 0;
            List<int> units = new List<int>();

            if (filterParameter.HallType != 0)
            {
                units.Add((int)filterParameter.HallType);
                hallCode = filterParameter.HallCode == null ? 0 : Convert.ToInt32(filterParameter.HallCode);
            }
            else
            if (filterParameter.ManagementType != 0)
                my_queue.Enqueue(filterParameter.ManagementType);
            else
                if (filterParameter.AssisttanceType != 0)
                my_queue.Enqueue(filterParameter.AssisttanceType);
            else
                my_queue.Enqueue(1);

            while (my_queue.Count > 0)
            {
                int top = (int)my_queue.Dequeue();
                var childern = dataContext.Unit.Where(c => c.ParentId == top);
                if (childern.Any())
                {
                    childern.ToList().ForEach(c =>
                    {
                        my_queue.Enqueue(c.Id);
                    });
                }
                else
                    units.Add(top);

            }

            var meter = (int)filterParameter.Meter;

            var formula = dataContext.Formula.Where(c =>
            //c.HallType == hallType &&
            units.Contains(c.UnitId) &&
            c.Meter == meter &&
            (hallCode == 0 ? true : c.HallCode == hallCode) &&
            c.SensorCode.EndsWith(endsString)
            ).ToList()
            .Select(c => new { c.SensorCode, c.UsagePercent });

            var result = new Dictionary<int, int>();
            var tagInfoPrv = dataContext.TagInfo.Where(c => formula.Select(c => c.SensorCode).Contains(c.Name)).ToList()
                .Select(c => new { c.Id, formula.Where(x => x.SensorCode == c.Name).Single().UsagePercent }).ToList();

            //var tagInfo = (from c in formula
            //               join t in tags on c.SensorCode equals t.Name
            //               select new { t.Id, c.UsagePercent }).ToList();

            tagInfoPrv.ForEach(c => result.Add(c.Id, c.UsagePercent));

            return result;
        }

        public async Task<List<ChartModel>> CalculateDate(List<TagValue> tagValue, Dictionary<int, int> hallTags, Period period)
        {
            var result = new List<ChartModel>();
            tagValue.AsParallel()
                        .GroupBy(x => x.Id).ToList().ForEach(

                    tagInfoId =>
                        tagInfoId.AsParallel().GroupBy(x => x.Timestamp.Date)
                            .OrderBy(c => c.Key).ToList().ForEach(

                        timeStampDate =>
                        {
                            var persianCalendar = new PersianCalendar();
                            int year = persianCalendar.GetYear(timeStampDate.Key);
                            int month = persianCalendar.GetMonth(timeStampDate.Key);
                            int day = persianCalendar.GetDayOfMonth(timeStampDate.Key);
                            var persianDate = year + "/" + month + "/" + day;

                            if (period == Period.Day) FillDailyResult(result, persianDate, timeStampDate, hallTags, tagInfoId.Key);

                            else if (period == Period.Minute)
                            {
                                timeStampDate.AsParallel().GroupBy(x => x.Timestamp.Hour)
                                        .OrderBy(c => c.Key).ToList().ForEach(

                                    timeStampHour =>
                                    {
                                        var lowMinute = 0;
                                        var highMinute = 4;
                                        var dataDic = new Dictionary<int, int>();

                                        timeStampHour.AsParallel().GroupBy(x => x.Timestamp.Minute)
                                        .OrderBy(c => c.Key).ToList().ForEach(
                                            timeStampMinute =>
                                            {
                                                dataDic.Add(timeStampMinute.Key, (int)(timeStampMinute.Average(z => z.value) * ((double)(hallTags.Single(z => z.Key == tagInfoId.Key).Value) / 100)));

                                            });

                                        do
                                        {
                                            var finalList = dataDic.ToList().Where(x => x.Key <= highMinute && lowMinute <= x.Key).ToList();

                                            if (finalList.Any())
                                                result.Add(new ChartModel()
                                                {
                                                    Label = persianDate + "ساعت: " + timeStampHour.Key + ":" + lowMinute,
                                                    Data = (int)finalList.Average(x => x.Value),
                                                });

                                            lowMinute += 5;
                                            highMinute += 5;
                                        } while (highMinute < 60);

                                    });
                            }

                        })
                    );
            return result;
        }

        public void FillDailyResult(List<ChartModel> result, string persianDate, IGrouping<DateTime, TagValue> timeStampDate, Dictionary<int, int> hallTags, int tagInfoId)
        {
            result.Add(new ChartModel()
            {
                Label = persianDate,
                Data = (int)(timeStampDate.Average(z => z.value) * ((double)(hallTags.Single(z => z.Key == tagInfoId).Value) / 100)),
            });

        }

        public async Task<List<Unit>> GetManagementByParameter(int parentId)
        {
            var assistance = dataContext.Unit.Where(c => c.ParentId == parentId);
            return assistance.ToList();
        }

    }
}
