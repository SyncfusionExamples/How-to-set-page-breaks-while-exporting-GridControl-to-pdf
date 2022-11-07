# How to set page breaks while exporting WinForms GridControl to pdf

## PDF Exporting

Page breaks can be set while exporting GridControl as PDF by passing row count for each PDF page in the GridPDFConverter.ExportToPdf method. The PDF pages are merged as a single PDF file and saved finally.

## C#

```C#
private void Exportbutton_Click(object sender, EventArgs e)
{
    var converter = new GridPDFConverter();
    var lowest = 0;
 
    //Adding page breaks to a list.
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
```
