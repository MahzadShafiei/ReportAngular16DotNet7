using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
using Report.Application.Dto.Include;
using Report.Application.Enum;
using Report.Domain;
using Report.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Application.Business
{
    public class TagValueBusiness: ITagValueBusiness
    {
        private readonly DataContext dataContext;

        public TagValueBusiness(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<TagValue>> GetAll()
        {
            return await dataContext.TagValues.ToListAsync();
        }

        public async Task<List<TagValue>> GetByFilter(FilterParameter filterParameter)
        {
            var hallTags = GetHallTags(filterParameter).Result;
            var result = await dataContext.TagValues.Where
                (c =>
                 hallTags.Contains(c.Id) && 
                 c.Timestamp<filterParameter.EndDate &&
                 c.Timestamp>filterParameter.StartDate                    
                ).ToListAsync();
            return result;
        }
             

        public async Task<List<int>> GetHallTags(FilterParameter filterParameter)
        {
            var hallType = (int)filterParameter.HallType;
            var meter= (int)filterParameter.Meter;
            //var formula= dataContext.Formulas.Where(c=> )
            var result= new List<int>();
            result.Add(1646);
            //result.Add(493);
            return result;
        }
    }
}
