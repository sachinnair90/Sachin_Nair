using OutputProvider.MovAggr.Common;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.IO;
using System.Text;

namespace PDFOutputProvider.MovAggr.Writer
{
    /// <summary>
    /// PDF Output provider using PDF Sharp
    /// </summary>
    [OutputProviderInfo("This provider helps to give output in text file format.")]
    public class PDFOutputProvider : IOutputProvider
    {
        private string fileName = "PDFOutput_{0}.pdf";
        private string filePath = @"{0}";
        private string format = string.Empty;
        private string name = string.Empty;
        private OutputProviderType type;
        private Encoding defaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Constructor
        /// </summary>
        public PDFOutputProvider()
        {
            format = ".pdf";
            name = "PDFOutput";
            type = OutputProviderType.FILE;
        }

        /// <summary>
        /// Output format
        /// </summary>
        public string Format { get { return format; } }

        /// <summary>
        /// Provider name
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// Output Provider type
        /// </summary>
        public OutputProviderType Type { get { return type; } }

        /// <summary>
        /// Render output using given data
        /// </summary>
        /// <param name="data">Data in byte[] form</param>
        /// <returns>If the output render was successful</returns>
        public bool Output(byte[] data)
        {
            try
            {
                // Split data into string[] to be printed as a single row each time, helps calculate to number of rows to span across multiple pages
                var dataStr = defaultEncoding.GetString(data).Split(new string[] { "\r\n" }, StringSplitOptions.None);

                fileName = String.Format(fileName, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));

                PdfDocument document = new PdfDocument();
                PDFLayoutHelper helper = new PDFLayoutHelper(document, XUnit.FromCentimeter(2.5), XUnit.FromCentimeter(29.7 - 2.5));
                XUnit left = XUnit.FromCentimeter(2.5);

                const int normalFontSize = 10;
                XFont fontNormal = new XFont("Verdana", normalFontSize, XFontStyle.Regular);
                int totalLines = dataStr.Length;

                // Iterate over string[] and write each row
                for (int line = 0; line < totalLines; ++line)
                {
                    // Get new line position
                    XUnit top = helper.GetLinePosition(normalFontSize + 2, normalFontSize);

                    // Write to PDF
                    helper.Gfx.DrawString(dataStr[line], fontNormal, XBrushes.Black, left, top, XStringFormats.TopLeft);
                }

                document.Save(fileName);

                filePath = String.Format(filePath, fileName);

                if (File.Exists(fileName))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to write to file!");
            }

            return false;
        }
    }
}
