using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FileDataExtractor.Models
{
    public class CountColumnData
    {
        public string countData(string column, DataTable dt)
        {
            int numberOfRecords = dt.Select("[" + column + "]" + "<>" + "'null'").Length;
            string response = column + " contains " + numberOfRecords + " entries";
            return response;
        }
    }
}