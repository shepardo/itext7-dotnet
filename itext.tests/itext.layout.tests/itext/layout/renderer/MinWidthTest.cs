using System;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Minmaxwidth;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Layout.Renderer {
    public class MinWidthTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/layout/MinWidthTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/layout/MinWidthTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ParagraphTest() {
            String outFileName = destinationFolder + "paragraphTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_paragraphTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str).SetBorder(new SolidBorder(Color.BLACK, 5))).SetBorder(new SolidBorder
                (Color.BLUE, 5));
            MinMaxWidth result = ((AbstractRenderer)p.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            p.SetWidth(ToEffectiveWidth(p, result.GetMinWidth()));
            doc.Add(p);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DivTest() {
            String outFileName = destinationFolder + "divTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_divTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY);
            Div d = new Div().SetPadding(4f).SetBorder(new SolidBorder(Color.GREEN, 5)).SetMargin(6);
            d.Add(p);
            MinMaxWidth result = ((AbstractRenderer)d.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            d.SetWidth(ToEffectiveWidth(d, result.GetMinWidth()));
            doc.Add(d);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void DivWithSmallRotatedParagraph() {
            String outFileName = destinationFolder + "divSmallRotatedParagraphTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_divSmallRotatedParagraphTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY);
            Div d = new Div().SetPadding(4f).SetBorder(new SolidBorder(Color.GREEN, 5)).SetMargin(6);
            d.Add(new Paragraph(("iText")).SetRotationAngle(Math.PI / 8).SetBorder(new SolidBorder(Color.BLUE, 2f)));
            d.Add(p);
            MinMaxWidth result = ((AbstractRenderer)d.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            d.SetWidth(ToEffectiveWidth(d, result.GetMinWidth()));
            doc.Add(d);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void DivWithBigRotatedParagraph() {
            String outFileName = destinationFolder + "divBigRotatedParagraphTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_divBigRotatedParagraphTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY).SetRotationAngle(Math.PI / 8);
            Div d = new Div().SetPadding(4f).SetBorder(new SolidBorder(Color.GREEN, 5)).SetMargin(6);
            d.Add(p);
            d.Add(new Paragraph(("iText")));
            MinMaxWidth result = ((AbstractRenderer)d.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            d.SetWidth(ToEffectiveWidth(d, result.GetMinWidth()));
            doc.Add(d);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        [LogMessage(iText.IO.LogMessageConstant.ELEMENT_DOES_NOT_FIT_AREA)]
        public virtual void DivWithSmallRotatedDiv() {
            String outFileName = destinationFolder + "divSmallRotatedDivTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_divSmallRotatedDivTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY);
            Div d = new Div().SetPadding(4f).SetBorder(new SolidBorder(Color.GREEN, 5)).SetMargin(6);
            d.Add(p);
            Div dRotated = new Div().SetRotationAngle(Math.PI / 8).SetBorder(new SolidBorder(Color.BLUE, 2f));
            d.Add(dRotated.Add(new Paragraph(("iText"))));
            MinMaxWidth result = ((AbstractRenderer)d.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            d.SetWidth(ToEffectiveWidth(d, result.GetMinWidth()));
            doc.Add(d);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void DivWithBigRotatedDiv() {
            String outFileName = destinationFolder + "divBigRotatedDivTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_divBigRotatedDivTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY);
            Div dRotated = new Div().SetPadding(4f).SetBorder(new SolidBorder(Color.GREEN, 5)).SetMargin(6);
            dRotated.Add(p).SetRotationAngle(Math.PI * 3 / 8);
            Div d = new Div().Add(new Paragraph(("iText"))).Add(dRotated).SetBorder(new SolidBorder(Color.BLUE, 2f));
            MinMaxWidth result = ((AbstractRenderer)d.CreateRendererSubTree().SetParent(doc.GetRenderer())).GetMinMaxWidth
                (doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            d.SetWidth(ToEffectiveWidth(d, result.GetMinWidth()));
            doc.Add(d);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void MultipleDivTest() {
            String outFileName = destinationFolder + "multipleDivTest01.pdf";
            String cmpFileName = sourceFolder + "cmp_multipleDivTest01.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(pdfDocument);
            Border[] borders = new Border[] { new SolidBorder(Color.BLUE, 2f), new SolidBorder(Color.RED, 2f), new SolidBorder
                (Color.GREEN, 2f) };
            Div externalDiv = new Div().SetPadding(2f).SetBorder(borders[2]);
            Div curr = externalDiv;
            for (int i = 0; i < 100; ++i) {
                Div d = new Div().SetBorder(borders[i % 3]);
                curr.Add(d);
                curr = d;
            }
            String str = "Hello. I am a fairly long paragraph. I really want you to process me correctly. You heard that? Correctly!!! Even if you will have to wrap me.";
            Paragraph p = new Paragraph(new Text(str)).SetPadding(1f).SetBorder(new SolidBorder(Color.BLACK, 2)).SetMargin
                (3).SetBackgroundColor(Color.LIGHT_GRAY);
            curr.Add(p);
            MinMaxWidth result = ((AbstractRenderer)externalDiv.CreateRendererSubTree().SetParent(doc.GetRenderer())).
                GetMinMaxWidth(doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            externalDiv.SetWidth(ToEffectiveWidth(externalDiv, result.GetMinWidth()));
            doc.Add(externalDiv);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void SimpleTableTest() {
            String outFileName = destinationFolder + "simpleTableTest.pdf";
            String cmpFileName = sourceFolder + "cmp_simpleTableTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(new PdfDocument(new PdfWriter(outFileName)));
            Cell cell1 = new Cell().Add("I am table").SetBorder(new SolidBorder(Color.RED, 60)).SetBorderBottom(Border
                .NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell cell2 = new Cell().Add("I am table").SetBorder(new SolidBorder(Color.YELLOW, 10)).SetBorderBottom(Border
                .NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Table table = new Table(2).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell(cell1.Clone(true)).AddCell(cell2
                .Clone(true)).AddCell(cell1.Clone(true)).AddCell(cell2.Clone(true));
            TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree().SetParent(doc.GetRenderer());
            MinMaxWidth minMaxWidth = renderer.GetMinMaxWidth(doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            Table minTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMinColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMinWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell
                (cell1.Clone(true)).AddCell(cell2.Clone(true)).AddCell(cell1.Clone(true)).AddCell(cell2.Clone(true));
            Table maxTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMaxColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMaxWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell
                (cell1.Clone(true)).AddCell(cell2.Clone(true)).AddCell(cell1.Clone(true)).AddCell(cell2.Clone(true));
            doc.Add(table);
            doc.Add(minTable);
            doc.Add(maxTable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ColspanTableTest() {
            String outFileName = destinationFolder + "colspanTableTest.pdf";
            String cmpFileName = sourceFolder + "cmp_colspanTableTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(new PdfDocument(new PdfWriter(outFileName)));
            Cell bigCell = new Cell(1, 2).Add("I am veryveryvery big cell").SetBorder(new SolidBorder(Color.RED, 60)).
                SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell cell = new Cell().Add("I am cell").SetBorder(new SolidBorder(Color.YELLOW, 10)).SetBorderBottom(Border
                .NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Table table = new Table(3).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell(cell.Clone(true)).AddCell(bigCell
                .Clone(true)).AddCell(cell.Clone(true)).AddCell(cell.Clone(true)).AddCell(cell.Clone(true));
            TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree().SetParent(doc.GetRenderer());
            MinMaxWidth minMaxWidth = renderer.GetMinMaxWidth(doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            Table minTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMinColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMinWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell
                (cell.Clone(true)).AddCell(bigCell.Clone(true)).AddCell(cell.Clone(true)).AddCell(cell.Clone(true)).AddCell
                (cell.Clone(true));
            Table maxTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMaxColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMaxWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLUE, 20)).AddCell
                (cell.Clone(true)).AddCell(bigCell.Clone(true)).AddCell(cell.Clone(true)).AddCell(cell.Clone(true)).AddCell
                (cell.Clone(true));
            doc.Add(table);
            doc.Add(minTable);
            doc.Add(maxTable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void ColspanRowspanTableTest() {
            String outFileName = destinationFolder + "colspanRowspanTableTest.pdf";
            String cmpFileName = sourceFolder + "cmp_colspanRowspanTableTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(new PdfDocument(new PdfWriter(outFileName)));
            Cell colspanCell = new Cell(1, 2).Add("I am veryveryvery big cell").SetBorder(new SolidBorder(Color.RED, 60
                )).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell rowspanCell = new Cell(2, 1).Add("I am very very very long cell").SetBorder(new SolidBorder(Color.GREEN
                , 60)).SetBorderBottom(Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell cell = new Cell().Add("I am cell").SetBorder(new SolidBorder(Color.BLUE, 10)).SetBorderBottom(Border.
                NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Table table = new Table(3).SetBorder(new SolidBorder(Color.BLACK, 20)).AddCell(cell.Clone(true)).AddCell(cell
                .Clone(true)).AddCell(rowspanCell.Clone(true)).AddCell(colspanCell.Clone(true));
            TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree().SetParent(doc.GetRenderer());
            MinMaxWidth minMaxWidth = renderer.GetMinMaxWidth(doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            Table minTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMinColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMinWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLACK, 20)).AddCell
                (cell.Clone(true)).AddCell(cell.Clone(true)).AddCell(rowspanCell.Clone(true)).AddCell(colspanCell.Clone
                (true));
            Table maxTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMaxColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMaxWidth())).SetMarginTop(10).SetBorder(new SolidBorder(Color.BLACK, 20)).AddCell
                (cell.Clone(true)).AddCell(cell.Clone(true)).AddCell(rowspanCell.Clone(true)).AddCell(colspanCell.Clone
                (true));
            doc.Add(table);
            doc.Add(minTable);
            doc.Add(maxTable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        /// <exception cref="System.IO.IOException"/>
        /// <exception cref="System.Exception"/>
        [NUnit.Framework.Test]
        public virtual void HeaderFooterTableTest() {
            String outFileName = destinationFolder + "headerFooterTableTest.pdf";
            String cmpFileName = sourceFolder + "cmp_headerFooterTableTest.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outFileName));
            Document doc = new Document(new PdfDocument(new PdfWriter(outFileName)));
            Cell bigCell = new Cell().Add("veryveryveryvery big cell").SetBorder(new SolidBorder(Color.RED, 40)).SetBorderBottom
                (Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell mediumCell = new Cell().Add("mediumsize cell").SetBorder(new SolidBorder(Color.GREEN, 30)).SetBorderBottom
                (Border.NO_BORDER).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Cell cell = new Cell().Add("cell").SetBorder(new SolidBorder(Color.BLUE, 10)).SetBorderBottom(Border.NO_BORDER
                ).SetBorderTop(Border.NO_BORDER).SetPadding(0);
            Table table = new Table(3).SetBorder(new SolidBorder(Color.BLACK, 20)).AddCell(mediumCell.Clone(true)).AddCell
                (mediumCell.Clone(true)).AddCell(mediumCell.Clone(true)).AddFooterCell(cell.Clone(true)).AddFooterCell
                (cell.Clone(true)).AddFooterCell(bigCell.Clone(true)).AddHeaderCell(bigCell.Clone(true)).AddHeaderCell
                (cell.Clone(true)).AddHeaderCell(cell.Clone(true));
            TableRenderer renderer = (TableRenderer)table.CreateRendererSubTree().SetParent(doc.GetRenderer());
            MinMaxWidth minMaxWidth = renderer.GetMinMaxWidth(doc.GetPageEffectiveArea(PageSize.A4).GetWidth());
            Table minTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMinColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMinWidth())).SetBorder(new SolidBorder(Color.BLACK, 20)).SetMarginTop(20).AddCell
                (mediumCell.Clone(true)).AddCell(mediumCell.Clone(true)).AddCell(mediumCell.Clone(true)).AddFooterCell
                (cell.Clone(true)).AddFooterCell(cell.Clone(true)).AddFooterCell(bigCell.Clone(true)).AddHeaderCell(bigCell
                .Clone(true)).AddHeaderCell(cell.Clone(true)).AddHeaderCell(cell.Clone(true));
            Table maxTable = new Table(ToEffectiveTableColumnWidth(renderer.GetMaxColumnWidth())).SetWidth(ToEffectiveWidth
                (table, minMaxWidth.GetMaxWidth())).SetBorder(new SolidBorder(Color.BLACK, 20)).SetMarginTop(20).AddCell
                (mediumCell.Clone(true)).AddCell(mediumCell.Clone(true)).AddCell(mediumCell.Clone(true)).AddFooterCell
                (cell.Clone(true)).AddFooterCell(cell.Clone(true)).AddFooterCell(bigCell.Clone(true)).AddHeaderCell(bigCell
                .Clone(true)).AddHeaderCell(cell.Clone(true)).AddHeaderCell(cell.Clone(true));
            doc.Add(table);
            doc.Add(minTable);
            doc.Add(maxTable);
            doc.Close();
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(outFileName, cmpFileName, destinationFolder
                , "diff"));
        }

        private static float ToEffectiveWidth(BlockElement b, float fullWidth) {
            if (b is Table) {
                return fullWidth + ((Table)b).GetNumberOfColumns() * MinMaxWidthUtils.GetEps();
            }
            else {
                return fullWidth - MinMaxWidthUtils.GetBorderWidth(b) - MinMaxWidthUtils.GetMarginsWidth(b) - MinMaxWidthUtils
                    .GetPaddingWidth(b) + MinMaxWidthUtils.GetEps();
            }
        }

        private static float[] ToEffectiveTableColumnWidth(float[] tableColumnWidth) {
            float[] result = tableColumnWidth.Clone();
            for (int i = 0; i < result.Length; ++i) {
                result[i] += MinMaxWidthUtils.GetEps();
            }
            return result;
        }
    }
}
