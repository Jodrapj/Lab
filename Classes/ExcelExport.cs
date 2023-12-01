using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Linq;

namespace Lab.Classes
{
    public class ExcelExport
    {
        public static void exportToExcel()
        {
            byte _range = 5;
            byte _temp = 0;
            var app = new Excel.Application();
            app.Visible = true;

            app.Workbooks.Add();

            Excel._Worksheet worksheet = (Excel.Worksheet)app.ActiveSheet;

            Excel.Range _excelCells1 = (Excel.Range)worksheet.get_Range("A1", "E1").Cells;
            _excelCells1.Merge(Type.Missing);
            worksheet.Cells[1, 1] = $"Ведомость оплаты за электроэнергию за {DateTime.Now.Month} месяц";

            _excelCells1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            _excelCells1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            worksheet.Cells[2, "A"] = "Табельный номер";
            worksheet.Cells[2, "B"] = "Фамилия, имя, отчество";
            worksheet.Cells[2, "C"] = "Кол-во киловатт";
            worksheet.Cells[2, "D"] = "Тариф";
            worksheet.Cells[2, "E"] = "Сумма к оплате";

            var _row = 2;
            var _row2 = 2;

            var accountingTable = Connect.context.AccountingTable.ToList();
            foreach (var cell in accountingTable)
            {
                _row++;
                worksheet.Cells[_row, 1] = cell.EntryNumber.ToString();
                worksheet.Cells[_row, 3] = cell.KWNumber.ToString();
                worksheet.Cells[_row, 4] = cell.Rate.ToString();
                worksheet.Cells[_row, 5] = $"=C{_row}*D{_row}";
                worksheet.Cells[_row + 1, 5] = $"=@СУММ(E2:E{_row})";
            }
            var referenceTable = Connect.context.ReferenceTable.ToList();
            foreach (var cell in referenceTable)
            {
                _row2++;
                worksheet.Cells[_row2, 2] = cell.FullName.ToString();
            }

            Excel.Range _excelCells2 = (Excel.Range)worksheet.get_Range($"A{_row + 1}", $"D{_row + 1}").Cells;
            _excelCells2.Merge(Type.Missing);
            _excelCells2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            _excelCells2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Cells[_row + 1, 1] = "Итого:";

            while (_range > _temp)
            {
                _temp++;
                ((Excel.Range)worksheet.Columns[_temp]).AutoFit();
                
                if (_temp == _range)
                {
                    _temp = 0;
                    break;
                }
            }
        }
    }
}
