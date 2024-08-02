using Autodesk.DataManagement.Client.Framework.Internal.ExtensionMethods;
using ClosedXML.Excel;
using ConsoleApp2.Model;

public class ExcelService
{
    public Dictionary<string, List<ExcelErrorData>> ReadExcelFile(string filePath)
    {
        var dataList = new Dictionary<string, List<ExcelErrorData>>();

        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1);
            var range = worksheet.RangeUsed();

            foreach (var row in range.Rows())
            {
                string errorCode = row.Cell(1).GetValue<string>();
                var data = new ExcelErrorData
                {
                    Name = row.Cell(2).GetValue<string>(),
                    Description = row.Cell(3).GetValue<string>()
                };

                if (!dataList.ContainsKey(errorCode))
                {
                    dataList.Add(errorCode, new List<ExcelErrorData> { data });
                }
                else
                {
                    dataList[errorCode].Add(data);
                }
            }
        }
        return dataList;
    }
}
