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
    public class CallReports
    {
        PdfFont helvetica = PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA);
        public void generateReport(string rootpath)
        {
            PageSize pg = PageSize.A4.Rotate();
            Image img = new Image(ImageDataFactory.Create(rootpath + "/img/helpdesk.png"))
                 .ScaleAbsolute(200, 100)
                .SetFixedPosition(pg.GetHeight() /1.87F, pg.GetWidth() / 1.75F);

            PdfWriter writer = new PdfWriter(rootpath + "/pdfs/callslist.pdf",
                                new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf,pg); // PageSize(595, 842)
            document.Add(img);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("All Calls")
                .SetFont(helvetica)
                .SetFontSize(24)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                );
                
            Table table = new Table(6);
            table
                .SetWidth(600)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetRelativePosition(0, 0, 0, 0)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.AddCell(addCell("Date Opened", "h",8));
            table.AddCell(addCell("Employee", "h",0));
            table.AddCell(addCell("Technician", "h", 0));
            table.AddCell(addCell("Problem", "h",0));
            table.AddCell(addCell("Status", "h",0));
            table.AddCell(addCell("Date Closed", "h",8));
            CallViewModel call = new CallViewModel();
            List<CallViewModel> calls = call.GetAll();
            foreach (CallViewModel cal in calls)
            {
                table.AddCell(addCell(cal.DateOpened.ToShortDateString(), "d", 0));
                table.AddCell(addCell(cal.EmployeeName, "d",0));
                table.AddCell(addCell(cal.TechName, "d",0));
                table.AddCell(addCell(cal.ProblemDescription, "d",0));
                table.AddCell(addCell(cal.OpenStatus == true ? "Open" : "Closed", "d", 0));
                table.AddCell(addCell(cal.DateClosed != null ? cal.DateClosed?.ToShortDateString() : "-", "d",8));

            }
            document.Add(table);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph("Call report written on - " + DateTime.Now)
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
                    .SetFontSize(14)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetBold()
                    )
                    .SetBorder(Border.NO_BORDER);
            }
            else
            {
                cell = new Cell().Add(
                    new Paragraph(data)
                    .SetFontSize(12)
                    .SetTextAlignment(TextAlignment.LEFT)
                    .SetPaddingLeft(padLeft)
                    )
                    .SetBorder(Border.NO_BORDER);
            }
            return cell;
        }//addCell
    }//class
}//namespace