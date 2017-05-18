﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SchoolSite.Domain.Core
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ScriptIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }

}