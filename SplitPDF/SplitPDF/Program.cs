using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PDFSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPdfPath = "E:\\Tasks\\SplitPDF\\Pdf files\\Visa_Q_A.pdf"; // Input PDF file
            string outputFolder = "E:\\Tasks\\SplitPDF\\Splitted Pdf files";     // Output folder
            int[] pageRanges = new[] { 1, 3 };                                  // page numbers

            Directory.CreateDirectory(outputFolder);

            using (PdfReader pdfReader = new PdfReader(inputPdfPath))
            {
                for (int i = 0; i < pageRanges.Length; i += 2)
                {
                    int startPage = pageRanges[i];
                    int endPage = Math.Min(pageRanges[i + 1], pdfReader.NumberOfPages);

                    using (Document document = new Document())
                    {
                        using (FileStream outputStream = new FileStream(Path.Combine(outputFolder, $"output_{startPage}-{endPage}.pdf"), FileMode.Create))
                        {
                            PdfCopy copy = new PdfCopy(document, outputStream);
                            document.Open();

                            for (int page = startPage; page <= endPage; page++)
                            {
                                copy.AddPage(copy.GetImportedPage(pdfReader, page));
                            }

                            document.Close();
                        }
                    }
                }
            }

            Console.WriteLine("PDF split successfully.");
        }
    }
}
