using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
using Report.Application.Dto.Include;
using Report.Application.Enum;
using Report.Domain;
using Report.Persistance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var hallTags = GetHallTags(filterParameter).Result;
            var tagValue = await dataContext.TagValue.Where
                (c =>
                 hallTags.Contains(c.TagInfoId) &&
                 c.Timestamp <= filterParameter.EndDate.AddDays(1) &&
                 c.Timestamp >= filterParameter.StartDate
                ).ToListAsync();

            var result= new List<IGrouping<int, TagValue>>();
            switch (filterParameter.Period)
            {
                case Period.Hour:
                    break;
                case Period.Day:
                    //var a = tagValue.GroupBy(c => new { Date = c.Timestamp.Day, Month = c.Timestamp.Month })
                    //    .ToDictionary(g => g.Key, g => g.Count());
                    //.Select(c => new { day = c.Key, item = c.Sum(x => x.value) });


                    return (tagValue.GroupBy(c => c.Timestamp.Date)
                        .Select(c=> new ChartModel() { Label=c.Key.ToString(), Data= c.Sum(x=> x.value)})
                        .ToList());
                    
                    break;
                case Period.Month:
                    break;
                default:
                    break;
            }
            return new List<ChartModel>();
        }


        public async Task<List<int>> GetHallTags(FilterParameter filterParameter)
        {
            var hallType = (int)filterParameter.HallType;
            var meter = (int)filterParameter.Meter;
            var hallCode = Convert.ToInt32(filterParameter.HallCode);

            var formula = dataContext.Formula.Where(c =>
            c.HallType == hallType &&
            c.Meter == meter &&
            c.HallCode == hallCode).ToList()
            .Select(c => c.SensorCode);

            var tagInfoPrv = dataContext.TagInfo_prv.Where(c => formula.Contains(c.Name)).ToList()
                .Select(c => c.Id).ToList();

            return tagInfoPrv;
        }
    }
}
