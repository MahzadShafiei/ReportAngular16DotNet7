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
            var result1 = new List<ChartModel>();
            var hallTags = GetHallTags(filterParameter).Result;

            var tagValue = await dataContext.TagValue.Where
                (c =>
                 hallTags.Select(c => c.Key).Contains(c.TagInfoId) &&
                 c.Timestamp <= filterParameter.EndDate.AddDays(1) &&
                 c.Timestamp >= filterParameter.StartDate
                ).ToListAsync();

            var result = new List<IGrouping<int, TagValue>>();
            switch (filterParameter.Period)
            {
                case Period.Minute:
                    break;
                case Period.Hour:
                    break;
                case Period.Day:

                    var tagValueGroupingResult = tagValue.GroupBy(x => x.TagInfoId).Select(
                    tagInfoId => new TagInfoGroup()
                    {
                        Key = tagInfoId.Key,
                        Count = tagInfoId.Count(),
                        DateGroup = tagInfoId.GroupBy(x => x.Timestamp.Date).Select(

                        timeStampDate =>
                        
                            new TimeStampDateGroup()
                            {
                                Key = timeStampDate.Key.Date,
                                //PersianDate = year + "/" + month + "/" + day,
                                Count = timeStampDate.Count(),
                                HourGroup = timeStampDate.GroupBy(x => x.Timestamp.Hour).Select(
                                timeStampHour => new TimeStampHourGroup()
                                {
                                    Key = timeStampHour.Key,
                                    Count = timeStampHour.Count(),
                                    MainValue = timeStampHour.Average(z => z.value),
                                    Average = timeStampHour.Average(z => z.value) * ((double)(hallTags.Single(z => z.Key == tagInfoId.Key).Value) / 100),
                                }
                                ).ToList()
                            }
                        ).ToList(),
                    }
                    ).ToList();

                    var a = tagValueGroupingResult.Select(
                        tagInfoId =>

                        tagInfoId.DateGroup.Select(
                            timeStampDate =>

                                timeStampDate.HourGroup.OrderBy(c=> c.Key).Select(
                                    timeStampHour =>
                                        new ChartModel()
                                        {
                                            Label = timeStampDate.Key.ToString().Substring(0,10) + "ساعت: " + timeStampHour.Key,
                                            Data = (int)timeStampHour.Average,
                                        }
                                    ).ToList()
                            ).ToList()
                    ).ToList();

                    foreach (var item in a)
                    {
                        item.ForEach(
                            c => c.ForEach(
                                x =>
                                {
                                    if (x.Data != 0)
                                        result1.Add(x);
                                }
                                ));
                        //result.Add(item);
                    }

                    return result1;

                    return (tagValue.GroupBy(c => c.Timestamp.Date)
                        .Select(c => new ChartModel() { Label = c.Key.ToString(), Data = (int)c.Average(x => x.value) })
                        .ToList());

                    break;
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
