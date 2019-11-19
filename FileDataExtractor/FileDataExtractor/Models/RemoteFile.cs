using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;

namespace DataMiner.Models
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
                throw new System.Exception("File is not of type 'text/csv; charset=utf-8.' Please only upload csv files to this program.");
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
                string[] rows = fullText.Split('\n'); // split full file text

                for (int i = 0; i < rows.Count() - 1; i++)
                {
                    string[] rowValues = rows[i].Split(',');//split each row with,
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < rowValues.Count(); j++) // add the row values to each column
                            {
                                dtCsv.Columns.Add(rowValues[j], typeof(double)); // add headers and specify their type.
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int k = 0; k < rowValues.Count(); k++)
                            {
                                dr[k] = rowValues[k];
                            }
                            dtCsv.Rows.Add(dr); //add other rows
                        }
                    }
                }
            }
            return dtCsv;
        }



    }
}