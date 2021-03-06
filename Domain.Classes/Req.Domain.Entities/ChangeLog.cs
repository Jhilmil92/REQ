﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Classes.Req.Domain.Entities
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string PropertyName { get; set; }
        public int PrimaryKeyValue { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public string LoggedBy { get; set; }
    }
}
