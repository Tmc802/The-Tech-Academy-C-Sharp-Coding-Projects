using System.Web.Mvc;
using DataMiner.Models;
using System.IO;
using System.Data;
using System.Web;

namespace DataMiner.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        public ActionResult ChooseFile()
        {
            return View();
        }

        [ActionName("ViewData")]
        public ActionResult viewData(string url)
        {
            if (url == null)
            url = Session["url"] as string;

            //DataTable stored in session
            Session.Add("url", url);
            
            RemoteFile URL = new RemoteFile();
            StreamReader sr = URL.GetCSV(url);
            DataTable dt = URL.parseFile(sr);

            DataTable Model = dt;
            
            //Store DataTable in session
            Session.Add("dt", Model);
            Session.Add("FullDT", Model);

            return View("ViewData");
        }

        //ViewData override for filtered data
        [ActionName("FilterData")]
        public ActionResult viewData(string column, string sign, double operand, string sortOrder)
        {

            //call DataTable stored in session
            var dt = Session["FullDT"] as DataTable;

            Filters dtSorted = new Filters();
            
            DataTable Model = dtSorted.FilterData(column, sign, operand, sortOrder, dt);

            //Remove the original DT from session
            Session.Remove("dt");
            //Store sorted DataTable in session
            Session.Add("dt", Model);

            return View("ViewData");

        }


        // Download Controller Calls
        public void DownloadXML()
        {
            DownloadFile XMLFile = new DownloadFile();

            var dt = Session["dt"] as DataTable;

            HttpResponseBase response = HttpContext.Response;

            XMLFile.downloadXML(dt, response);
        }


        public void DownloadJSON()
        {
            DownloadFile JSONFile = new DownloadFile();

            var dt = Session["dt"] as DataTable;

            HttpResponseBase response = HttpContext.Response;

            JSONFile.downloadJson(dt, response);
        }

        public void DownloadRawData()
        {
            var url = Session["url"] as string;

            //need to store the url in session as well
            RemoteFile request = new RemoteFile();

            StreamReader rawData = request.GetCSV(url);

            DownloadFile rawDataString = new DownloadFile();

            HttpResponseBase response = HttpContext.Response;

            rawDataString.downloadRawData(rawData.ReadToEnd(), response);
        }
    }
}