using System.Collections.Generic;
using System.Web.Mvc;
using FileDataExtractor.Models;
using LumenWorks.Framework.IO;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using System;

namespace FileDataExtractor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChooseFile()
        {
            return View();
        }

        public ActionResult ViewData(string url)
        {
            //var url = "https://data.cityofnewyork.us/api/views/kku6-nxdu/rows.csv?accessType=DOWNLOAD";
            //Method 1
            GetCSVFile newURL = new GetCSVFile();

            DataTable dt = newURL.GetCSV(url); // feed the url into the parser

            var Model = dt; // pass the data to a datatable object "Model"

            return View(Model);
        }

        public ActionResult FilteredDataView(string column, string sign, int operand, string url, string sortOrder)
        {

            // parse the url again
            GetCSVFile newURL = new GetCSVFile();
             
            DataTable dt = newURL.GetCSV(url); // feed the url into the parser

            DataTable filtDataTable = new DataTable(); // create a datatable to display the results of the filtered data

            // send the column names to a new filtered data table
            foreach(DataColumn col in dt.Columns)
            {
                filtDataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            // Create the expression
            string expression = "[" + column + "]" + sign + operand;
            DataRow[] foundRows; // add the filtered data to a datarow array
            
            // Use the Select method to find all rows matching the filter.
            foundRows = dt.Select(expression);

            //Add the array foundRows to a new datatable
            foreach (DataRow row in foundRows)
            {
                var r = filtDataTable.Rows.Add();
                foreach (DataColumn col in filtDataTable.Columns)
                {
                    r[col.ColumnName] = row[col.ColumnName];
                }
            }

            // set the sort order of filtered data
            filtDataTable.DefaultView.Sort = "[" +column+ "]"+ " " + sortOrder;
            // set the sorted data to a new DataTable object
            DataTable dtSorted = filtDataTable.DefaultView.ToTable();

            var Model = dtSorted;

            return View(Model);
            }
        }
}