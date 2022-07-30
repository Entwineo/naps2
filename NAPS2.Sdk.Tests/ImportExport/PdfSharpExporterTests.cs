using NAPS2.Images.Gdi;
using NAPS2.ImportExport.Pdf;
using NAPS2.Sdk.Tests.Asserts;
using Xunit;

namespace NAPS2.Sdk.Tests.ImportExport;

public class PdfSharpExporterTests : ContextualTests
{
    [Fact]
    public async Task ExportSingleImage()
    {
        var filePath = Path.Combine(FolderPath, "test.pdf");
        using var image = ScanningContext.CreateProcessedImage(new GdiImage(ImageResources.color_image));

        var pdfExporter = new PdfSharpExporter(ScanningContext);
        await pdfExporter.Export(filePath, new[] { image }, new PdfExportParams());
        
        PdfAsserts.AssertImages(filePath, ImageResources.color_image);
    }

    // [Fact]
    // public async Task Export100()
    // {
    //     var filePath = Path.Combine(FolderPath, "test");
    //     using var image = ScanningContext.CreateProcessedImage(new GdiImage(ImageResources.color_image), BitDepth.Color, true, -1, Enumerable.Empty<Transform>());
    //
    //     var pdfExporter = new PdfSharpExporter(ScanningContext);
    //     for (int i = 0; i < 100; i++)
    //     {
    //         await pdfExporter.Export(filePath + i + ".pdf", new[] { image }, new PdfExportParams());
    //     }
    // }
}