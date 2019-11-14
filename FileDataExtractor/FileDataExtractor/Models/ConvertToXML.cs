using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FileDataExtractor.Models
{
    public class ConvertToXML
    {
        public DataSet DataSetToXML(DataTable dt) {
            DataSet ds = new DataSet(); // new data set object
            dt.TableName = "ExportedXML"; // set the datatable name
            ds.Tables.Add(dt); // add dt to dataset to access the write xml method
            ds.DataSetName = "ExportedXML"; // might not need the datatable name ********
            ds.GetXml(); // heres the xml dataset

            return ds;
        }

    }
}