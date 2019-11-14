using System.Web.Mvc;
using FileDataExtractor.Models;
using System.IO;
using System.Data;
using System.Net;
using System.Web;
using System.Collections.Generic;

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
            GetCSVFile newURL = new GetCSVFile();
            DataTable dt = newURL.GetCSV(url);
            return View(dt);
        }


        // Filter controller calls
        public ActionResult CountColumnData(string column, string url)
        {

            GetCSVFile newURL = new GetCSVFile();
            DataTable dt = newURL.GetCSV(url);

            CountColumnData columnDataCount = new CountColumnData();
            var response = columnDataCount.countData(column, dt);
            ViewBag.response = response;

            return View();
        }

        public ActionResult CountColumnDataFiltered(string column, string sign, int operand, string url)
        {
            //string url = "https://data.cityofnewyork.us/api/views/kku6-nxdu/rows.csv?accessType=DOWNLOAD";
            //string column = "JURISDICTION NAME";
            string sortOrder = "ASC";

            GetCSVFile newURL = new GetCSVFile();
            DataTable dt = newURL.GetCSV(url);
            FilteredDT dtSorted = new FilteredDT();
            DataTable dts = dtSorted.FilterData(column, sign, operand, url, sortOrder, dt);

            CountColumnData columnDataCount = new CountColumnData();
            var response = columnDataCount.countData(column, dts);
            ViewBag.response = response;

            return View();
        }

        public ActionResult FilteredDataView(string column, string sign, int operand, string url, string sortOrder)
        {
            GetCSVFile newURL = new GetCSVFile();
            DataTable dt = newURL.GetCSV(url);
            FilteredDT dtSorted = new FilteredDT();

            return View(dtSorted.FilterData(column, sign, operand, url, sortOrder, dt));
        }


        // Download Controller Calls
        public void DownloadXML(string url)
        {
            GetCSVFile newFile = new GetCSVFile();
            DataTable dt = newFile.GetCSV(url);

            ConvertToXML XMLString = new ConvertToXML();
            DataSet ds = XMLString.DataSetToXML(dt);

            HttpResponseBase response = HttpContext.Response;

            string fileName = "ExportedXML.xml";

            response.StatusCode = 200;

            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(ds.GetXml());
        }

        public void DownloadXMLFiltered(string column, string sign, int operand, string url, string sortOrder)
        {
            GetCSVFile newFile = new GetCSVFile();
            DataTable dt = newFile.GetCSV(url);
            FilteredDT st = new FilteredDT();
            var dsSorted = st.FilterData(column, sign, operand, url, sortOrder, dt);

            ConvertToXML XMLString = new ConvertToXML();
            DataSet ds = XMLString.DataSetToXML(dsSorted);

            HttpResponseBase response = HttpContext.Response;

            string fileName = "ExportedXML.xml";

            response.StatusCode = 200;

            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(ds.GetXml());
        }


        public void DownloadJSON(string url)
        {
            GetCSVFile newFile = new GetCSVFile(); // new GetCSVFile object
            DataTable dt = newFile.GetCSV(url); // use the GetCSV parser
            ConvertToJSON newJSONFile = new ConvertToJSON();

            HttpResponseBase response = HttpContext.Response;

            string fileName = "ExportedJSON.json";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(newJSONFile.ConvertDataTabletoString(dt).ToString());
        }

        public void DownloadJSONFiltered(string column, string sign, int operand, string url, string sortOrder)
        {
            GetCSVFile newFile = new GetCSVFile();
            DataTable dt = newFile.GetCSV(url);
            FilteredDT st = new FilteredDT();
            var dsSorted = st.FilterData(column, sign, operand, url, sortOrder, dt);
            ConvertToJSON newJSONFile = new ConvertToJSON();

            HttpResponseBase response = HttpContext.Response;

            string fileName = "ExportedJSON.json";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(newJSONFile.ConvertDataTabletoString(dsSorted).ToString());
        }

        public void DownloadRawData(string url)
        {
            RawData sr = new RawData();
            sr.dataRequest(url);

            HttpResponseBase response = HttpContext.Response;

            string fileName = "ExportedRawData.txt";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(sr);
        }
    }
}