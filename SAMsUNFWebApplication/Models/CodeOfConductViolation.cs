﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMsUNFWebApplication.Models
{
    public class CodeOfConductViolation
    {

        
        public int code_of_conduct_violation_id { get; set; }
        public string duval_violation_code { get; set; }
        public string short_code { get; set; }
        public string name { get; set; }
        public int create_contact_id { get; set; }
        public DateTime create_dt { get; set; }
        public int last_update_contact_id { get; set; }
        public DateTime last_update_dt { get; set; }
        public Boolean is_deleted { get; set; }
        public int  school_year_id { get; set; }


    }
}