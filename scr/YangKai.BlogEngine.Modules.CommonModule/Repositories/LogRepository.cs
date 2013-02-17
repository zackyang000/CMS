using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Modules.CommonModule.Objects;

namespace YangKai.BlogEngine.Modules.CommonModule.Repositories
{
    public class LogRepository : GuidRepository<Log>
    {
        public LogRepository(IUnitOfWork context)
            : base(context)
        {
         
        }

        public IList<int> GetVisitStat(int days)
        {
            var siteVisitType = ActionTypeEnum.SiteVisit.ToString();
            IList<int> data = new List<int>();
            (
                from r in GetAll(p => p.ActionType == siteVisitType)
                group r by new
                    {
                        year = r.CreateDate.Year,
                        month = r.CreateDate.Month,
                        day = r.CreateDate.Day
                    }
                into g
                orderby new
                    {
                        g.Key.year,
                        g.Key.month,
                        g.Key.day
                    } descending
                select new
                    {
                        count = g.Count()
                    }
            ).Take(days).ToList().ForEach(p => data.Add(p.count));
            return data;
        }

        public int SiteVisitCount()
        {
            var siteVisitType = ActionTypeEnum.SiteVisit.ToString();
            return base.Count(p => p.ActionType == siteVisitType);
        }

        public int SiteVisitCountOnToday()
        {
            var siteVisitType = ActionTypeEnum.SiteVisit.ToString();
            var today = DateTime.Now.Date;
            return base.Count(p => p.ActionType == siteVisitType && p.CreateDate >= today);
        }
    }
}