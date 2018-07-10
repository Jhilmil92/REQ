using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Services.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using Req.Enums.Req.Common.Constants;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccessLayer.Req.Data.Services.Classes
{
    public class LogChangeService:ILogChangeService
    {
        private readonly ILogChangeRepository _logChangeRepository;
        public LogChangeService()
        {
            _logChangeRepository = new LogChangeRepository();
        }
        public void LogChanges(DbContext dbContext, Job job)
        {
            var modifiedEntities = dbContext.ChangeTracker.Entries().Where(p=> p.State == EntityState.Modified).ToList();
            var currentDateTime = DateTime.UtcNow;
            int currentUserId = (int)HttpContext.Current.Session[Constants.CurrentUserId];
            var createdBy = ((IReqDataSource)((DataContext)dbContext)).Users.SingleOrDefault(d => d.UserId == currentUserId);
            foreach(var change in modifiedEntities)
            {
                var entityName = job.GetType().Name.Split('_')[0];
                var primaryKey = job.JobId;
                foreach(var prop in change.OriginalValues.PropertyNames)
                {
                    var originalValue = change.OriginalValues[prop] == null ? string.Empty : change.OriginalValues[prop].ToString();
                    //var originalValue = change.GetDatabaseValues().GetValue<object>(prop).ToString();
                    var currentValue = change.CurrentValues[prop] == null ? string.Empty : change.CurrentValues[prop].ToString();
                    if(originalValue != currentValue)
                    {
                        ChangeLog log = new ChangeLog
                        {
                            EntityName = entityName,
                            PrimaryKeyValue = (int)primaryKey,
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = currentDateTime,
                            LoggedBy = createdBy.UserName
                        };
                        _logChangeRepository.AddChangeLog(log);
                    }
                }
                _logChangeRepository.Save();
            }
        }

        public object GetPrimaryKeyValue(DbEntityEntry entry, DbContext dbContext)
        {
            var objectStateEntry = ((IObjectContextAdapter)dbContext).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }


        public void LogAddChanges(Job job)
        {
            foreach (var prop in job.GetType().GetProperties())
            {
                var originalValue = string.Empty;
                //var originalValue = change.GetDatabaseValues().GetValue<object>(prop).ToString();
                var currentValue = prop.GetValue(job);
                if (originalValue != currentValue)
                {
                    ChangeLog log = new ChangeLog
                    {
                        EntityName = typeof(Job).GetType().Name,
                        PrimaryKeyValue = job.JobId,
                        PropertyName = prop.Name,
                        OldValue = originalValue,
                        NewValue = currentValue.ToString(),
                        DateChanged = DateTime.Now
                    };
                    _logChangeRepository.AddChangeLog(log);
                }
            }

            _logChangeRepository.Save();
        }
    }
}
