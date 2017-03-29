/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2017 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS

    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/

    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.

    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.

    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.

    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com */
using System;
using System.Collections.Generic;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Impl.Tags;
using iText.Html2pdf.Css;
using iText.Html2pdf.Css.Apply.Util;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Util;
using iText.Html2pdf.Html;
using iText.Html2pdf.Html.Impl.Jsoup.Node;
using iText.Html2pdf.Html.Node;
using iText.IO.Log;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace iText.Html2pdf.Attach.Impl.Layout {
    internal class PageContextProcessor {
        private PageSize pageSize;

        private ICollection<String> marks;

        private float? bleed;

        private float[] margins;

        private Border[] borders;

        private float[] paddings;

        private Div pageBackgroundSimulation;

        private Div pageBordersSimulation;

        private Rectangle[] marginBoxRectangles;

        private Div[] marginBoxElements;

        internal PageContextProcessor(PageContextProperties properties, ProcessorContext context, PageSize defaultPageSize
            ) {
            IDictionary<String, String> styles = properties.GetResolvedPageContextNode().GetStyles();
            float em = CssUtils.ParseAbsoluteLength(styles.Get(CssConstants.FONT_SIZE));
            float rem = context.GetCssContext().GetRootFontSize();
            pageSize = PageSizeParser.FetchPageSize(styles.Get(CssConstants.SIZE), em, rem, defaultPageSize);
            UnitValue bleedValue = CssUtils.ParseLengthValueToPt(styles.Get(CssConstants.BLEED), em, rem);
            if (bleedValue != null && bleedValue.IsPointValue()) {
                bleed = bleedValue.GetValue();
            }
            marks = ParseMarks(styles.Get(CssConstants.MARKS));
            ParseMargins(styles, em, rem);
            ParseBorders(styles, em, rem);
            ParsePaddings(styles, em, rem);
            CreatePageSimulationElements(styles, context);
            CreateMarginBoxesElements(properties.GetResolvedPageMarginBoxes(), context);
        }

        internal virtual PageSize GetPageSize() {
            return pageSize;
        }

        internal virtual float[] ComputeLayoutMargins() {
            float[] layoutMargins = iText.IO.Util.JavaUtil.ArraysCopyOf(margins, margins.Length);
            for (int i = 0; i < borders.Length; ++i) {
                float width = borders[i] != null ? borders[i].GetWidth() : 0;
                layoutMargins[i] += width;
            }
            for (int i = 0; i < paddings.Length; ++i) {
                layoutMargins[i] += paddings[i];
            }
            return layoutMargins;
        }

        internal virtual void ProcessNewPage(PdfPage page, DocumentRenderer documentRenderer) {
            SetBleed(page);
            DrawMarks(page);
            DrawPageBackgroundAndBorders(page);
            DrawMarginBoxes(page, documentRenderer);
        }

        private void SetBleed(PdfPage page) {
            if (bleed == null && !marks.IsEmpty()) {
                bleed = 6f;
            }
            if (bleed != null) {
                Rectangle box = page.GetMediaBox();
                box.IncreaseHeight((float)bleed * 2);
                box.SetWidth(box.GetWidth() + (float)bleed * 2);
                page.SetMediaBox(box).SetBleedBox(box);
                Rectangle trimBox = page.GetTrimBox();
                trimBox.MoveUp((float)bleed);
                trimBox.MoveRight((float)bleed);
                page.SetTrimBox(trimBox);
            }
        }

        private void DrawMarks(PdfPage page) {
            if (marks.IsEmpty()) {
                return;
            }
            float horizontalIndent = 48;
            float verticalIndent = 57;
            Rectangle mediaBox = page.GetMediaBox();
            mediaBox.IncreaseHeight(verticalIndent * 2);
            mediaBox.SetWidth(mediaBox.GetWidth() + horizontalIndent * 2);
            page.SetMediaBox(mediaBox);
            Rectangle bleedBox = page.GetBleedBox();
            bleedBox.MoveUp(verticalIndent);
            bleedBox.MoveRight(horizontalIndent);
            page.SetBleedBox(bleedBox);
            Rectangle trimBox = page.GetTrimBox();
            trimBox.MoveUp(verticalIndent);
            trimBox.MoveRight(horizontalIndent);
            page.SetTrimBox(trimBox);
            PdfCanvas canvas = new PdfCanvas(page);
            if (marks.Contains(CssConstants.CROP)) {
                float cropLineLength = 24;
                float verticalCropStartIndent = verticalIndent - cropLineLength;
                float horizontalCropStartIndent = horizontalIndent - cropLineLength;
                canvas.SaveState().SetLineWidth(0.1f).MoveTo(trimBox.GetLeft(), verticalCropStartIndent).LineTo(trimBox.GetLeft
                    (), verticalIndent).MoveTo(horizontalCropStartIndent, trimBox.GetTop()).LineTo(horizontalIndent, trimBox
                    .GetTop()).MoveTo(trimBox.GetRight(), verticalCropStartIndent).LineTo(trimBox.GetRight(), verticalIndent
                    ).MoveTo(mediaBox.GetWidth() - horizontalCropStartIndent, trimBox.GetTop()).LineTo(mediaBox.GetWidth()
                     - horizontalIndent, trimBox.GetTop()).MoveTo(trimBox.GetLeft(), mediaBox.GetHeight() - verticalCropStartIndent
                    ).LineTo(trimBox.GetLeft(), mediaBox.GetHeight() - verticalIndent).MoveTo(mediaBox.GetWidth() - horizontalCropStartIndent
                    , trimBox.GetBottom()).LineTo(mediaBox.GetWidth() - horizontalIndent, trimBox.GetBottom()).MoveTo(trimBox
                    .GetRight(), mediaBox.GetHeight() - verticalCropStartIndent).LineTo(trimBox.GetRight(), mediaBox.GetHeight
                    () - verticalIndent).MoveTo(horizontalCropStartIndent, trimBox.GetBottom()).LineTo(horizontalIndent, trimBox
                    .GetBottom()).Stroke().RestoreState();
            }
            if (marks.Contains(CssConstants.CROSS)) {
                float horCrossCenterIndent = verticalIndent - 12;
                float verCrossCenterIndent = horizontalIndent - 12;
                float x;
                float y;
                canvas.SaveState().SetLineWidth(0.1f);
                x = mediaBox.GetWidth() / 2;
                y = mediaBox.GetHeight() - horCrossCenterIndent;
                DrawCross(canvas, x, y, true);
                x = mediaBox.GetWidth() / 2;
                y = horCrossCenterIndent;
                DrawCross(canvas, x, y, true);
                x = verCrossCenterIndent;
                y = mediaBox.GetHeight() / 2;
                DrawCross(canvas, x, y, false);
                x = mediaBox.GetWidth() - verCrossCenterIndent;
                y = mediaBox.GetHeight() / 2;
                DrawCross(canvas, x, y, false);
                canvas.RestoreState();
            }
        }

        private void DrawCross(PdfCanvas canvas, float x, float y, bool horizontalCross) {
            float xLineHalf;
            float yLineHalf;
            float circleR = 6;
            if (horizontalCross) {
                xLineHalf = 30;
                yLineHalf = 12;
            }
            else {
                xLineHalf = 12;
                yLineHalf = 30;
            }
            canvas.MoveTo(x - xLineHalf, y).LineTo(x + xLineHalf, y).MoveTo(x, y - yLineHalf).LineTo(x, y + yLineHalf);
            canvas.Circle(x, y, circleR);
            canvas.Stroke();
        }

        private void DrawPageBackgroundAndBorders(PdfPage page) {
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(new PdfCanvas(page), page.GetDocument(), page.GetBleedBox
                ());
            canvas.Add(pageBackgroundSimulation);
            canvas.Close();
            canvas = new iText.Layout.Canvas(new PdfCanvas(page), page.GetDocument(), page.GetTrimBox());
            canvas.Add(pageBordersSimulation);
            canvas.Close();
        }

        private void DrawMarginBoxes(PdfPage page, DocumentRenderer documentRenderer) {
            for (int i = 0; i < 16; ++i) {
                if (marginBoxElements[i] != null) {
                    Div curBoxElement = marginBoxElements[i];
                    IRenderer renderer = curBoxElement.CreateRendererSubTree();
                    renderer.SetParent(documentRenderer);
                    LayoutResult result = renderer.Layout(new LayoutContext(new LayoutArea(page.GetDocument().GetPageNumber(page
                        ), marginBoxRectangles[i])));
                    IRenderer rendererToDraw = result.GetStatus() == LayoutResult.FULL ? renderer : result.GetSplitRenderer();
                    rendererToDraw.SetParent(documentRenderer).Draw(new DrawContext(page.GetDocument(), new PdfCanvas(page)));
                }
            }
        }

        private static ICollection<String> ParseMarks(String marksStr) {
            ICollection<String> marks = new HashSet<String>();
            if (marksStr == null) {
                return marks;
            }
            String[] split = iText.IO.Util.StringUtil.Split(marksStr, " ");
            foreach (String mark in split) {
                if (CssConstants.CROP.Equals(mark) || CssConstants.CROSS.Equals(mark)) {
                    marks.Add(mark);
                }
                else {
                    marks.Clear();
                    break;
                }
            }
            return marks;
        }

        private void ParseMargins(IDictionary<String, String> styles, float em, float rem) {
            float defaultMargin = 36;
            PageSize pageSize = GetPageSize();
            margins = ParseBoxProps(styles, em, rem, defaultMargin, pageSize, CssConstants.MARGIN_TOP, CssConstants.MARGIN_RIGHT
                , CssConstants.MARGIN_BOTTOM, CssConstants.MARGIN_LEFT);
        }

        private void ParsePaddings(IDictionary<String, String> styles, float em, float rem) {
            float defaultPadding = 0;
            PageSize pageSize = GetPageSize();
            paddings = ParseBoxProps(styles, em, rem, defaultPadding, pageSize, CssConstants.PADDING_TOP, CssConstants
                .PADDING_RIGHT, CssConstants.PADDING_BOTTOM, CssConstants.PADDING_LEFT);
        }

        private void ParseBorders(IDictionary<String, String> styles, float em, float rem) {
            borders = BorderStyleApplierUtil.GetBordersArray(styles, em, rem);
        }

        private void CreatePageSimulationElements(IDictionary<String, String> styles, ProcessorContext context) {
            pageBackgroundSimulation = new Div().SetFillAvailableArea(true);
            BackgroundApplierUtil.ApplyBackground(styles, context, pageBackgroundSimulation);
            pageBordersSimulation = new Div().SetFillAvailableArea(true);
            pageBordersSimulation.SetMargins(margins[0], margins[1], margins[2], margins[3]);
            pageBordersSimulation.SetBorderTop(borders[0]);
            pageBordersSimulation.SetBorderRight(borders[1]);
            pageBordersSimulation.SetBorderBottom(borders[2]);
            pageBordersSimulation.SetBorderLeft(borders[3]);
        }

        private void CreateMarginBoxesElements(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes, ProcessorContext
             context) {
            marginBoxRectangles = CalculateMarginBoxRectangles(resolvedPageMarginBoxes);
            marginBoxElements = new Div[16];
            foreach (PageMarginBoxContextNode marginBoxContentNode in resolvedPageMarginBoxes) {
                int marginBoxInd = MapMarginBoxNameToIndex(marginBoxContentNode.GetMarginBoxName());
                Div marginBox = new Div();
                marginBoxElements[marginBoxInd] = marginBox;
                IDictionary<String, String> boxStyles = marginBoxContentNode.GetStyles();
                BackgroundApplierUtil.ApplyBackground(boxStyles, context, marginBox);
                FontStyleApplierUtil.ApplyFontStyles(boxStyles, context, marginBoxContentNode, marginBox);
                BorderStyleApplierUtil.ApplyBorders(boxStyles, context, marginBox);
                VerticalAlignmentApplierUtil.ApplyVerticalAlignmentForCells(boxStyles, context, marginBox);
                float em = CssUtils.ParseAbsoluteLength(boxStyles.Get(CssConstants.FONT_SIZE));
                float rem = context.GetCssContext().GetRootFontSize();
                float[] boxMargins = ParseBoxProps(boxStyles, em, rem, 0, CalculateContainingBlockSizesForMarginBox(marginBoxInd
                    ), CssConstants.MARGIN_TOP, CssConstants.MARGIN_RIGHT, CssConstants.MARGIN_BOTTOM, CssConstants.MARGIN_LEFT
                    );
                float[] boxPaddings = ParseBoxProps(boxStyles, em, rem, 0, CalculateContainingBlockSizesForMarginBox(marginBoxInd
                    ), CssConstants.PADDING_TOP, CssConstants.PADDING_RIGHT, CssConstants.PADDING_BOTTOM, CssConstants.PADDING_LEFT
                    );
                marginBox.SetMargins(boxMargins[0], boxMargins[1], boxMargins[2], boxMargins[3]);
                marginBox.SetPaddings(boxPaddings[0], boxPaddings[1], boxPaddings[2], boxPaddings[3]);
                marginBox.SetProperty(Property.FONT_PROVIDER, context.GetFontProvider());
                marginBox.SetProperty(Property.FONT_SET, context.GetTempFonts());
                marginBox.SetFillAvailableArea(true);
                if (marginBoxContentNode.ChildNodes().IsEmpty()) {
                    // margin box node shall not be added to resolvedPageMarginBoxes if it's kids were not resolved from content
                    throw new InvalidOperationException();
                }
                // TODO it would be great to reuse DefaultHtmlProcessor, but it seems there is no convenient way of doing so, and maybe it would be an overkill
                IElementNode dummyMarginBoxNode = new JsoupElementNode(new Org.Jsoup.Nodes.Element(Org.Jsoup.Parser.Tag.ValueOf
                    (TagConstants.DIV), ""));
                DivTagWorker marginBoxWorker = new DivTagWorker(dummyMarginBoxNode, context);
                for (int i = 0; i < marginBoxContentNode.ChildNodes().Count; i++) {
                    INode childNode = marginBoxContentNode.ChildNodes()[i];
                    if (childNode is ITextNode) {
                        String text = ((ITextNode)marginBoxContentNode.ChildNodes()[i]).WholeText();
                        marginBoxWorker.ProcessContent(text, context);
                    }
                    else {
                        if (childNode is IElementNode) {
                            ITagWorker childTagWorker = context.GetTagWorkerFactory().GetTagWorker((IElementNode)childNode, context);
                            if (childTagWorker != null) {
                                childTagWorker.ProcessEnd((IElementNode)childNode, context);
                                marginBoxWorker.ProcessTagChild(childTagWorker, context);
                            }
                        }
                        else {
                            LoggerFactory.GetLogger(GetType()).Error(iText.Html2pdf.LogMessageConstant.UNKNOWN_MARGIN_BOX_CHILD);
                        }
                    }
                }
                marginBoxWorker.ProcessEnd(dummyMarginBoxNode, context);
                IPropertyContainer workerResult = marginBoxWorker.GetElementResult();
                if (workerResult is Div) {
                    foreach (IElement child in ((Div)workerResult).GetChildren()) {
                        if (child is IBlockElement) {
                            marginBox.Add((IBlockElement)child);
                        }
                        else {
                            if (child is Image) {
                                marginBox.Add((Image)child);
                            }
                        }
                    }
                }
            }
        }

        private Rectangle[] CalculateMarginBoxRectangles(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes) {
            // TODO It's a very basic implementation for now. In future resolve rectangles based on presence of certain margin boxes, 
            //      also height and width properties should be taken into account.
            float topMargin = margins[0];
            float rightMargin = margins[1];
            float bottomMargin = margins[2];
            float leftMargin = margins[3];
            Rectangle withoutMargins = ((PageSize)pageSize.Clone()).ApplyMargins<Rectangle>(topMargin, rightMargin, bottomMargin
                , leftMargin, false);
            float topBottomMarginWidth = withoutMargins.GetWidth() / 3;
            float leftRightMarginHeight = withoutMargins.GetHeight() / 3;
            Rectangle[] hardcodedBoxRectangles = new Rectangle[] { new Rectangle(0, withoutMargins.GetTop(), leftMargin
                , topMargin), new Rectangle(rightMargin, withoutMargins.GetTop(), topBottomMarginWidth, topMargin), new 
                Rectangle(rightMargin + topBottomMarginWidth, withoutMargins.GetTop(), topBottomMarginWidth, topMargin
                ), new Rectangle(withoutMargins.GetRight() - topBottomMarginWidth, withoutMargins.GetTop(), topBottomMarginWidth
                , topMargin), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop(), topBottomMarginWidth, 
                topMargin), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop() - leftRightMarginHeight, 
                rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetBottom
                () + leftRightMarginHeight, rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight
                (), withoutMargins.GetBottom(), rightMargin, leftRightMarginHeight), new Rectangle(withoutMargins.GetRight
                (), 0, rightMargin, bottomMargin), new Rectangle(withoutMargins.GetRight() - topBottomMarginWidth, 0, 
                topBottomMarginWidth, bottomMargin), new Rectangle(rightMargin + topBottomMarginWidth, 0, topBottomMarginWidth
                , bottomMargin), new Rectangle(rightMargin, 0, topBottomMarginWidth, bottomMargin), new Rectangle(0, 0
                , leftMargin, bottomMargin), new Rectangle(0, withoutMargins.GetBottom(), leftMargin, leftRightMarginHeight
                ), new Rectangle(0, withoutMargins.GetBottom() + leftRightMarginHeight, leftMargin, leftRightMarginHeight
                ), new Rectangle(0, withoutMargins.GetTop() - leftRightMarginHeight, leftMargin, leftRightMarginHeight
                ) };
            return hardcodedBoxRectangles;
        }

        private Rectangle CalculateContainingBlockSizesForMarginBox(int marginBoxInd) {
            if (marginBoxInd == 0 || marginBoxInd == 4 || marginBoxInd == 8 || marginBoxInd == 12) {
                return marginBoxRectangles[marginBoxInd];
            }
            Rectangle withoutMargins = ((PageSize)pageSize.Clone()).ApplyMargins<Rectangle>(margins[0], margins[1], margins
                [2], margins[3], false);
            if (marginBoxInd < 4) {
                return new Rectangle(withoutMargins.GetWidth(), margins[0]);
            }
            else {
                if (marginBoxInd < 8) {
                    return new Rectangle(margins[1], withoutMargins.GetHeight());
                }
                else {
                    if (marginBoxInd < 12) {
                        return new Rectangle(withoutMargins.GetWidth(), margins[2]);
                    }
                    else {
                        return new Rectangle(margins[3], withoutMargins.GetWidth());
                    }
                }
            }
        }

        private int MapMarginBoxNameToIndex(String marginBoxName) {
            switch (marginBoxName) {
                case CssRuleName.TOP_LEFT_CORNER: {
                    return 0;
                }

                case CssRuleName.TOP_LEFT: {
                    return 1;
                }

                case CssRuleName.TOP_CENTER: {
                    return 2;
                }

                case CssRuleName.TOP_RIGHT: {
                    return 3;
                }

                case CssRuleName.TOP_RIGHT_CORNER: {
                    return 4;
                }

                case CssRuleName.RIGHT_TOP: {
                    return 5;
                }

                case CssRuleName.RIGHT_MIDDLE: {
                    return 6;
                }

                case CssRuleName.RIGHT_BOTTOM: {
                    return 7;
                }

                case CssRuleName.BOTTOM_RIGHT_CORNER: {
                    return 8;
                }

                case CssRuleName.BOTTOM_RIGHT: {
                    return 9;
                }

                case CssRuleName.BOTTOM_CENTER: {
                    return 10;
                }

                case CssRuleName.BOTTOM_LEFT: {
                    return 11;
                }

                case CssRuleName.BOTTOM_LEFT_CORNER: {
                    return 12;
                }

                case CssRuleName.LEFT_BOTTOM: {
                    return 13;
                }

                case CssRuleName.LEFT_MIDDLE: {
                    return 14;
                }

                case CssRuleName.LEFT_TOP: {
                    return 15;
                }
            }
            return -1;
        }

        private float[] ParseBoxProps(IDictionary<String, String> styles, float em, float rem, float defaultValue, 
            Rectangle containingBlock, String topPropName, String rightPropName, String bottomPropName, String leftPropName
            ) {
            String topStr = styles.Get(topPropName);
            String rightStr = styles.Get(rightPropName);
            String bottomStr = styles.Get(bottomPropName);
            String leftStr = styles.Get(leftPropName);
            float? top = ParseBoxValue(topStr, em, rem, containingBlock.GetHeight());
            float? right = ParseBoxValue(rightStr, em, rem, containingBlock.GetWidth());
            float? bottom = ParseBoxValue(bottomStr, em, rem, containingBlock.GetHeight());
            float? left = ParseBoxValue(leftStr, em, rem, containingBlock.GetWidth());
            return new float[] { top != null ? (float)top : defaultValue, right != null ? (float)right : defaultValue, 
                bottom != null ? (float)bottom : defaultValue, left != null ? (float)left : defaultValue };
        }

        private static float? ParseBoxValue(String valString, float em, float rem, float dimensionSize) {
            UnitValue marginUnitVal = CssUtils.ParseLengthValueToPt(valString, em, rem);
            if (marginUnitVal != null) {
                if (marginUnitVal.IsPointValue()) {
                    return marginUnitVal.GetValue();
                }
                if (marginUnitVal.IsPercentValue()) {
                    return marginUnitVal.GetValue() * dimensionSize / 100;
                }
            }
            return null;
        }
    }
}
