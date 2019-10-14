using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;

namespace QuestionaireScrapper.Classes
{
    public class ExcelManager
    {

        public static byte[] createExcel(DataTable tbl, string sheetName)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(sheetName);
                pck.Workbook.FullCalcOnLoad = true;

                ws.Cells["A1"].LoadFromDataTable(tbl, true);
                foreach (var ch in "ABCD")
                {
                    string rngtxt = string.Format("{0}1:{1}", ch, ch + "" + (tbl.Rows.Count + 1));
                    using (ExcelRange rng = ws.Cells[rngtxt])
                    {

                    }
                }
                return pck.GetAsByteArray();
            }
        }

        public static DataTable GetDataTable(Dictionary<string, string> list)
        {
            var table = new DataTable();
            table.Columns.Add("Question(s)", typeof(string));
            table.Columns.Add("Answer(s)", typeof(string));
            foreach (var item in list)
            {
                var question = item.Key.Trim();
                var answer = item.Value.Trim();
                table.Rows.Add(question, answer);
            }
            return table;
        }
    }
}
