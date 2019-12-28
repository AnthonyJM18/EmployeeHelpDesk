using HelpdeskViewModels;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;

namespace ExercisesWebsite.Reports
{
    public class EmployeeReport
    {
        PdfFont helvetica = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
        public void generateReport(string rootpath)
        {
            PageSize pg = PageSize.A4;
            Image img = new Image(ImageDataFactory.Create(rootpath + "/img/helpdesk.png"))
                .ScaleAbsolute(200, 100)
                .SetFixedPosition(((pg.GetWidth() - 200) / 2), 710);

            PdfWriter writer = new PdfWriter(rootpath + "/pdfs/employeelist.pdf",
                                new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf); // PageSize(595, 842)
            document.Add(img);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("Current Employees")
                .SetFont(helvetica)
                .SetFontSize(24)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER));
            Table table = new Table(3);
            table
                .SetWidth(228)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetRelativePosition(0, 0, 0, 0)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(addCell("Title","h",0));
            table.AddCell(addCell("Firstname","h",0));
            table.AddCell(addCell("Lastname","h",0));
            table.AddCell(addCell(" ","d"));
            table.AddCell(addCell(" ","d"));
            table.AddCell(addCell(" ","d"));
            EmployeeViewModels employee = new EmployeeViewModels();
            List<EmployeeViewModels> employees = employee.GetAll();
            foreach (EmployeeViewModels emp in employees)
            {
                table.AddCell(addCell(emp.Title, "d", 8));
                table.AddCell(addCell(emp.Firstname, "d"));
                table.AddCell(addCell(emp.Lastname, "d"));
            }
            document.Add(table);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("Employee report written on - " + DateTime.Now)
                .SetFontSize(6)
                .SetTextAlignment(TextAlignment.CENTER));
            document.Close();

        } // Generates the report
        private Cell addCell(string data, string celltype, int padLeft = 16)
        {
            Cell cell;

            if (celltype == "h")
            {
                cell = new Cell().Add(
                    new Paragraph(data)
                    .SetFontSize(16)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetBold()
                    )
                    .SetBorder(Border.NO_BORDER);
            }
            else
            {
                cell = new Cell().Add(
                    new Paragraph(data)
                    .SetFontSize(14)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetPaddingLeft(padLeft)
                    )
                    .SetBorder(Border.NO_BORDER);
            }
            return cell;
        }//addCell
    }//class
}//namespace