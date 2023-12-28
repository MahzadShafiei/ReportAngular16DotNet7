using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
using Report.Application.Dto.Include;
using Report.Application.Enum;
using Report.Application.Model;
using Report.Domain;
using Report.Persistance;
using System.Globalization;

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

        public async Task<List<ChartModel>> GetByFilter(FilterParameter filterParameter)
        {
            var result = new List<ChartModel>();
            var hallTags = GetHallTags(filterParameter).Result;

            var tagValue = await dataContext.TagValue.Where
                (c =>
                 hallTags.Select(c => c.Key).Contains(c.TagInfoId) &&
                 c.Timestamp <= filterParameter.EndDate.AddDays(1) &&
                 c.Timestamp >= filterParameter.StartDate
                ).ToListAsync();


            switch (filterParameter.Period)
            {
                case Period.Minute:
                    break;
                case Period.Hour:
                    break;
                case Period.Day:

                    tagValue.Where(c => c.value != 0)
                        .GroupBy(x => x.TagInfoId).ToList().ForEach(

                    tagInfoId =>
                        tagInfoId.GroupBy(x => x.Timestamp.Date)
                            .OrderBy(c => c.Key).ToList().ForEach(

                        timeStampDate =>
                        {
                            var persianCalendar = new PersianCalendar();
                            int year = persianCalendar.GetYear(timeStampDate.Key);
                            int month = persianCalendar.GetMonth(timeStampDate.Key);
                            int day = persianCalendar.GetDayOfMonth(timeStampDate.Key);
                            var persianDate = year + "/" + month + "/" + day;

                            timeStampDate.GroupBy(x => x.Timestamp.Hour)
                                    .OrderBy(c => c.Key).ToList().ForEach(

                                timeStampHour =>
                                {
                                    var lowMinute = 0;
                                    var highMinute = 4;
                                    var dataDic = new Dictionary<int, int>();

                                    timeStampHour.GroupBy(x => x.Timestamp.Minute)
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
                        })
                    );

                    return result;

                case Period.Month:
                    break;
                default:
                    break;
            }
            return new List<ChartModel>();
        }


        public async Task<Dictionary<int, int>> GetHallTags(FilterParameter filterParameter)
        {
            var hallType = (int)filterParameter.HallType;
            var meter = (int)filterParameter.Meter;
            var hallCode = Convert.ToInt32(filterParameter.HallCode);

            var formula = dataContext.Formula.Where(c =>
            c.HallType == hallType &&
            c.Meter == meter &&
            c.HallCode == hallCode).ToList()
            .Select(c => new { c.SensorCode, c.UsagePercent });

            var result = new Dictionary<int, int>();
            var tagInfoPrv = dataContext.TagInfo_prv.Where(c => formula.Select(c => c.SensorCode).Contains(c.Name)).ToList()
                .Select(c => new { c.Id, formula.Where(x => x.SensorCode == c.Name).Single().UsagePercent }).ToList();

            tagInfoPrv.ForEach(c => result.Add(c.Id, c.UsagePercent));

            return result;
        }
    }
}
