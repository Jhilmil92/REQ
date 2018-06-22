using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Services.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Services.Classes
{
    class LogChangeService:ILogChangeService
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
            foreach(var change in modifiedEntities)
            {
                var entityName = change.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(change);
                foreach(var prop in change.OriginalValues.PropertyNames)
                {
                    //var originalValue = change.OriginalValues[prop].ToString();
                    var originalValue = change.GetDatabaseValues().GetValue<object>(prop).ToString();
                    var currentValue = change.CurrentValues[prop].ToString();
                    if(originalValue != currentValue)
                    {
                        ChangeLog log = new ChangeLog
                        {
                            EntityName = entityName,
                            //PrimaryKeyValue = primaryKey.ToString(),
                            PropertyName = prop,
                            OldValue = originalValue,
                            NewValue = currentValue,
                            DateChanged = currentDateTime
                        };
                        _logChangeRepository.AddChangeLog(log);
                    }
                }
            }
        }

        public object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}
