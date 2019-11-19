using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace FileDataExtractor.Models
{
    public class RemoteFile
    {

        public StreamReader GetCSV(string url)
        {
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url); // web request
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); // get response
            // check for content type here
            if(resp.ContentType != "text/csv; charset=utf-8")
            {
                throw new System.Exception("File is of type '.csv' Please only upload csv files to this program.");
            }
            StreamReader sr = new StreamReader(resp.GetResponseStream()); // response stream? 

            // getcsv will return a StreamReader object

            return sr;
        }

        public DataTable parseFile(StreamReader sr) {
            //make parse seperate function
            DataTable dtCsv = new DataTable();

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
                                dtCsv.Columns.Add(rowValues[j],typeof(Double)); // add headers
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int k = 0; k < rowValues.Count(); k++)
                            {
                                /*var rowV =*/
                                /* Needs .ToString(); */
                                dr[k] = rowValues[k];

                                //dr[k] = Convert.ToDouble(rowV);
                            }
                            dtCsv.Rows.Add(dr); //add other rows
                        }
                    }
                }
            }

            return dtCsv;
            //make parse seperate function
        }
    }
}