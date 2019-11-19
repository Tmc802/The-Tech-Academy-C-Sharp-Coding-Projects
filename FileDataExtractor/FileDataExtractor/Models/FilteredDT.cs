using System.Data;


namespace FileDataExtractor.Models
{
    public class FilteredDT
    {
        public DataTable FilterData(string column, string sign, int operand, string sortOrder, DataTable dt)
        {
            DataTable filtDataTable = new DataTable();

            foreach (DataColumn col in dt.Columns)
            {
                filtDataTable.Columns.Add(col.ColumnName, col.DataType);
            }
            string expression = "[" + column + "]" + " " + sign + " " + operand;

            DataRow[] foundRows;

            foundRows = dt.Select(expression);

            foreach (DataRow row in foundRows)
            {
                var r = filtDataTable.Rows.Add();
                foreach (DataColumn col in filtDataTable.Columns)
                {
                    r[col.ColumnName] = row[col.ColumnName];
                }
            }
            filtDataTable.DefaultView.Sort = "[" + column + "]" + " " + sortOrder;
            DataTable dtSorted = filtDataTable.DefaultView.ToTable();

            return dtSorted;
        }
    }
}