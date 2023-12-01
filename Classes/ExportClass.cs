using System;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;

namespace Lab.Classes
{
    internal class ExportClass
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

        public static void exportToPDF()
        {
            SaveFileDialog save = new SaveFileDialog(); save.Title = "Выберите файл";
            save.Filter = "PDF файлы (*.pdf*)|*.pdf*"; bool? res = save.ShowDialog();
            string filepath = " "; if (res == true)
            {
                filepath = save.FileName;
            }
            Document doc = new Document();

            PdfWriter.GetInstance(doc, new FileStream(filepath, FileMode.Create));
            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(5);

            PdfPCell cell1 = new PdfPCell(new Paragraph("Номер лицевого счета", font));
            PdfPCell cell2 = new PdfPCell(new Paragraph("Фамилия, имя, отчество", font));
            PdfPCell cell3 = new PdfPCell(new Paragraph("Кол-во киловатт", font));
            PdfPCell cell4 = new PdfPCell(new Paragraph("Тариф", font));
            PdfPCell cell5 = new PdfPCell(new Paragraph("Сумма к оплате", font));

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);
            table.AddCell(cell5);

            //Добавим в таблицу общий заголовок
            PdfPCell cell = new PdfPCell(new Phrase(filepath,font));

            var vozmoznocollection = Connect.context.ReferenceTable.ToList();
            foreach (var item in vozmoznocollection)
            {
                table.AddCell(new PdfPCell(new Paragraph(item.FullName.ToString(), font)));
            }

            doc.Add(table);

            doc.Close();

        }

    }
}
