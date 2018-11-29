﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceApprovalMVC.Models
{
    public class BestPriceQuotes
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public DateTime DOB { get; set; }
        public int carYear { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public string DUI { get; set; }
        public int speedingTickets { get; set; }
        public string fullCoverage { get; set; }

    }
}