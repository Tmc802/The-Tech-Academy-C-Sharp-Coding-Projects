using System.Data;


namespace DataMiner.Models
{
    public class Filters
    {
        public DataTable FilterData(string column, string sign, double operand, string sortOrder, DataTable dt)
        {
            DataTable filtDataTable = new DataTable();

            //create a new datatable with the original datatable columns
            foreach (DataColumn col in dt.Columns)
            {
                filtDataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            string expression = "[" + column + "]" + " " + sign + " " + operand;

            // store the rows that meet the criteria here
            DataRow[] foundRows;

            // use the expression to filter out rows that don't meet the criteria
            foundRows = dt.Select(expression);

            // use the DataRow array to populate the new datatable with the filtered data
            foreach (DataRow row in foundRows)
            {
                var r = filtDataTable.Rows.Add();
                foreach (DataColumn col in filtDataTable.Columns)
                {
                    r[col.ColumnName] = row[col.ColumnName];
                }
            }

            // return it sorted by the column and specified sort order
            filtDataTable.DefaultView.Sort = "[" + column + "]" + " " + sortOrder;
            DataTable dtSorted = filtDataTable.DefaultView.ToTable();

            return dtSorted;
        }
    }
}