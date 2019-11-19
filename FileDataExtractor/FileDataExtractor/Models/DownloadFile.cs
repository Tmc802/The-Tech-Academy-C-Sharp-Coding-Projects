using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DataMiner.Models;

namespace DataMiner.Models
{
    public class DownloadFile
    {

        //Convert DataTable to string for JSON download
        public string DataTableToString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public void downloadXML(DataTable dt, HttpResponseBase response)
        {
            DownloadFile newJSONFile = new DownloadFile();
            DataSet ds = new DataSet(); // new data set object
            ds.Tables.Add(dt); // add dt to dataset to access the write xml method

            string fileName = "ExportedXML.xml";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(ds.GetXml().Replace("_x0020", ""));

            ds.Tables.Remove(dt); // remove the datatable after the file was created for download
        }

        public void downloadJson(DataTable dt, HttpResponseBase response)
        {
            DownloadFile newJSONFile = new DownloadFile();

            string fileName = "ExportedJSON.json";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(newJSONFile.DataTableToString(dt).ToString());
        }

        public void downloadRawData(string rawData, HttpResponseBase response)
        {
            string fileName = "ExportedRawData.txt";

            response.StatusCode = 200;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Transfer-Encoding", "binary");
            response.ContentType = "application-download";
            response.Write(rawData);
        }
    }
}