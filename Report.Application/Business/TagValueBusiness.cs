using Microsoft.EntityFrameworkCore;
using Report.Application.Contract;
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
            return await dataContext.TagValue.ToListAsync();
        }

        public async Task<List<TagValue>> GetByFilter(string hallName, DateTime startDate, DateTime endDate, int meter)
        {
            var hallTags = GetHallTags(hallName).Result;
            var result = await dataContext.TagValue.Where(c => hallTags.Contains(c.Id)).ToListAsync();
            return result;
        }

        public async Task<List<int>> GetHallTags(string hallName)
        {
            var result= new List<int>();
            result.Add(1646);
            result.Add(493);
            return result;
        }
    }
}
