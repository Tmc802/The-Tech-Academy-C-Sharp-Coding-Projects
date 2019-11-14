using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace FileDataExtractor.Models
{



    public class GetCSVFile
    {

        public DataTable GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); // web request
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); // get response
            StreamReader sr = new StreamReader(resp.GetResponseStream()); // response stream? 
            DataTable dtCsv = new DataTable();
            DataTable dt = new DataTable();

            while (!sr.EndOfStream)
            {
                string fullText = sr.ReadToEnd(); // read full file text
                string[] rows = fullText.Split('\n'); // split full file text into

                for (int i = 0; i < rows.Count() - 1; i++)
                {
                    string[] rowValues = rows[i].Split(',');//split each row with,
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < rowValues.Count(); j++)
                            {
                                dtCsv.Columns.Add(rowValues[j], typeof(double)); // add headers
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int k = 0; k < rowValues.Count(); k++)
                            {
                                var rowV = rowValues[k].ToString();

                                dr[k] = Convert.ToDouble(rowV);
                            }
                            dtCsv.Rows.Add(dr); //add other rows
                        }
                    }
                }
            }
                sr.Close();

            return dtCsv;
        }
    }
}