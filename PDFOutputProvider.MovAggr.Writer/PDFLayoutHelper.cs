using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PDFOutputProvider.MovAggr.Writer
{
    /// <summary>
    /// PDF layout helper 
    /// 
    /// Provides helper methods to render the PDF
    /// </summary>
    public class PDFLayoutHelper
    {
        private readonly PdfDocument _document;
        private readonly XUnit _topPosition;
        private readonly XUnit _bottomMargin;
        private XUnit _currentPosition;

        /// <summary>
        /// Graphics object to render drawing
        /// </summary>
        public XGraphics Gfx { get; private set; }

        /// <summary>
        /// Page object to render a PDF page
        /// </summary>
        public PdfPage Page { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="document">Pdf Document object</param>
        /// <param name="topPosition">Top positon for the PDF write start position</param>
        /// <param name="bottomMargin">Bottom margin for the PDF</param>
        public PDFLayoutHelper(PdfDocument document, XUnit topPosition, XUnit bottomMargin)
        {
            _document = document;
            _topPosition = topPosition;
            _bottomMargin = bottomMargin;
            // Set a value outside the page - a new page will be created on the first request.
            _currentPosition = bottomMargin + 10000;
        }

        /// <summary>
        /// Get line positon for current write position
        /// </summary>
        /// <param name="requestedHeight">Requested height for next line</param>
        /// <param name="requiredHeight">Required height for next line</param>
        /// <returns></returns>
        public XUnit GetLinePosition(XUnit requestedHeight, XUnit requiredHeight)
        {
            XUnit required = requiredHeight == -1f ? requestedHeight : requiredHeight;

            // Check if new line position will exceed the page limit, if then create a new page
            if (_currentPosition + required > _bottomMargin)
                CreatePage();

            XUnit result = _currentPosition;
            _currentPosition += requestedHeight;
            return result;
        }

        /// <summary>
        /// Create new page
        /// </summary>
        void CreatePage()
        {
            Page = _document.AddPage();
            Page.Size = PageSize.A4;
            Gfx = XGraphics.FromPdfPage(Page);
            _currentPosition = _topPosition;
        }
    }

}
