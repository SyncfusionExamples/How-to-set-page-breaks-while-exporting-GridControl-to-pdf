#region Copyright Syncfusion Inc. 2001 - 2018

//
//  Copyright Syncfusion Inc. 2001 - 2018. All rights reserved.
//
//  Use of this code is subject to the terms of our license.
//  A copy of the current license can be obtained at any time by e-mailing
//  licensing@syncfusion.com. Any infringement will be prosecuted under
//  applicable laws. 
//

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Syncfusion.GridHelperClasses;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.Grid;

namespace PrintPageLayout
{
    public partial class Form1 : Form
    {
        #region Constructor

        public Form1()
        {
            InitializeComponent();
            gridControl1.RowCount = 150;
            gridControl1.ColCount = 10;
        }

        #endregion

        #region Export to PDF

        /// <summary>
        ///     Export to PDF on Button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exportbutton_Click(object sender, EventArgs e)
        {
            var converter = new GridPDFConverter();
            var lowest = 0;

            //Adding PageBreaks to a list.
            var rows = new List<int> {30, 63, 90, 130, 150};

            var pdfDocument = new PdfDocument();
            pdfDocument.Save("Sample.pdf");
            foreach (var rownumber in rows)
            {
                var pdf = new PdfDocument();
                var maximumRow = rownumber;
                converter.ExportToPdf(pdf, gridControl1, GridRangeInfo.Rows(lowest, maximumRow));
                var stream = new MemoryStream();
                pdf.Save(stream);
                var loadedDocument = new PdfLoadedDocument("Sample.pdf");
                loadedDocument =
                    PdfDocumentBase.Merge(loadedDocument, new PdfLoadedDocument(stream)) as PdfLoadedDocument;
                loadedDocument.Save("Sample.pdf");
                loadedDocument.Close(true);
                stream.Dispose();
                lowest = maximumRow + 1;
            }
            var loadedDocument1 = new PdfLoadedDocument("Sample.pdf");
            loadedDocument1.Pages.RemoveAt(0);
            loadedDocument1.Save("Sample.pdf");
            Process.Start("Sample.pdf");
        }

        #endregion
    }
}
