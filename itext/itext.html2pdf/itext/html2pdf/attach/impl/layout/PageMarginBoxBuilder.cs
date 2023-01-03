/*
This file is part of the iText (R) project.
Copyright (c) 1998-2023 iText Group NV
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
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using iText.Commons;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Css.Apply;
using iText.Html2pdf.Css.Page;
using iText.Html2pdf.Css.Resolve;
using iText.Html2pdf.Logs;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Minmaxwidth;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Layout.Tagging;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Util;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Layout {
    // implementation of algorithm specified in https://drafts.csswg.org/css-page-3/#margin-dimension
    internal class PageMarginBoxBuilder {
        internal IRenderer[] renderers;

        internal PageMarginBoxContextNode[] nodes;

        internal float[] margins;

        internal PageSize pageSize;

        internal IList<PageMarginBoxContextNode> resolvedPageMarginBoxes;

        private const float EPSILON = 0.00001f;

        public PageMarginBoxBuilder(IList<PageMarginBoxContextNode> resolvedPageMarginBoxes, float[] margins, PageSize
             pageSize) {
            this.resolvedPageMarginBoxes = resolvedPageMarginBoxes;
            this.margins = margins;
            this.pageSize = pageSize;
            Rectangle[] marginBoxRectangles = CalculateMarginBoxRectanglesCornersOnly();
            foreach (PageMarginBoxContextNode marginBoxContentNode in this.resolvedPageMarginBoxes) {
                if (marginBoxContentNode.ChildNodes().IsEmpty()) {
                    // margin box node shall not be added to resolvedPageMarginBoxes if it's kids were not resolved from content
                    throw new InvalidOperationException();
                }
                int marginBoxInd = MapMarginBoxNameToIndex(marginBoxContentNode.GetMarginBoxName());
                if (marginBoxRectangles[marginBoxInd] != null) {
                    marginBoxContentNode.SetPageMarginBoxRectangle(new Rectangle(marginBoxRectangles[marginBoxInd]).IncreaseHeight
                        (EPSILON));
                }
                marginBoxContentNode.SetContainingBlockForMarginBox(CalculateContainingBlockSizesForMarginBox(marginBoxInd
                    , marginBoxRectangles[marginBoxInd]));
            }
        }

        public virtual void BuildForSinglePage(int pageNumber, PdfDocument pdfDocument, DocumentRenderer documentRenderer
            , ProcessorContext context) {
            if (resolvedPageMarginBoxes.IsEmpty()) {
                return;
            }
            nodes = new PageMarginBoxContextNode[16];
            foreach (PageMarginBoxContextNode marginBoxContentNode in resolvedPageMarginBoxes) {
                nodes[MapMarginBoxNameToIndex(marginBoxContentNode.GetMarginBoxName())] = marginBoxContentNode;
            }
            IElement[] elements = new IElement[16];
            for (int i = 0; i < 16; i++) {
                if (nodes[i] != null) {
                    elements[i] = ProcessMarginBoxContent(nodes[i], pageNumber, context);
                }
            }
            GetPMBRenderers(elements, documentRenderer, pdfDocument);
        }

        public virtual IRenderer[] GetRenderers() {
            return renderers;
        }

        public virtual PageMarginBoxContextNode[] GetNodes() {
            return nodes;
        }

        private IElement ProcessMarginBoxContent(PageMarginBoxContextNode marginBoxContentNode, int pageNumber, ProcessorContext
             context) {
            marginBoxContentNode.SetStyles(marginBoxContentNode.GetStyles());
            DefaultCssResolver cssResolver = new DefaultCssResolver(marginBoxContentNode, context);
            ITagWorker marginBoxWorker = context.GetTagWorkerFactory().GetTagWorker(marginBoxContentNode, context);
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
                            IDictionary<String, String> stringStringMap = cssResolver.ResolveStyles(childNode, context.GetCssContext()
                                );
                            ((IElementNode)childNode).SetStyles(stringStringMap);
                            ICssApplier cssApplier = context.GetCssApplierFactory().GetCssApplier((IElementNode)childNode);
                            if (cssApplier != null) {
                                cssApplier.Apply(context, (IStylesContainer)childNode, childTagWorker);
                            }
                            childTagWorker.ProcessEnd((IElementNode)childNode, context);
                            marginBoxWorker.ProcessTagChild(childTagWorker, context);
                        }
                    }
                    else {
                        if (childNode is PageMarginRunningElementNode) {
                            PageMarginRunningElementNode runningElementNode = (PageMarginRunningElementNode)childNode;
                            RunningElementContainer runningElement = context.GetCssContext().GetRunningManager().GetRunningElement(runningElementNode
                                .GetRunningElementName(), runningElementNode.GetRunningElementOccurrence(), pageNumber);
                            if (runningElement != null) {
                                marginBoxWorker.ProcessTagChild(runningElement.GetProcessedElementWorker(), context);
                            }
                        }
                        else {
                            ITextLogManager.GetLogger(this.GetType()).LogError(Html2PdfLogMessageConstant.UNKNOWN_MARGIN_BOX_CHILD);
                        }
                    }
                }
            }
            marginBoxWorker.ProcessEnd(marginBoxContentNode, context);
            if (!(marginBoxWorker.GetElementResult() is IElement)) {
                throw new InvalidOperationException("Custom tag worker implementation for margin boxes shall return IElement for #getElementResult() call."
                    );
            }
            ICssApplier cssApplier_1 = context.GetCssApplierFactory().GetCssApplier(marginBoxContentNode);
            cssApplier_1.Apply(context, marginBoxContentNode, marginBoxWorker);
            return (IElement)marginBoxWorker.GetElementResult();
        }

        private void GetPMBRenderers(IElement[] elements, DocumentRenderer documentRenderer, PdfDocument pdfDocument
            ) {
            renderers = new IRenderer[16];
            for (int i = 0; i < 4; i++) {
                renderers[i * 4] = CreateCornerRenderer(elements[i * 4], documentRenderer, pdfDocument, i);
                for (int j = 1; j <= 3; j++) {
                    renderers[i * 4 + j] = CreateRendererFromElement(elements[i * 4 + j], documentRenderer, pdfDocument);
                }
                DetermineSizes(i);
            }
        }

        private IRenderer CreateCornerRenderer(IElement cornerBoxElement, DocumentRenderer documentRenderer, PdfDocument
             pdfDocument, int indexOfCorner) {
            IRenderer cornerRenderer = CreateRendererFromElement(cornerBoxElement, documentRenderer, pdfDocument);
            if (cornerRenderer != null) {
                float rendererWidth = margins[indexOfCorner % 3 == 0 ? 3 : 1] - GetSizeOfOneSide(cornerRenderer, Property.
                    MARGIN_LEFT, Property.BORDER_LEFT, Property.PADDING_LEFT) - GetSizeOfOneSide(cornerRenderer, Property.
                    MARGIN_RIGHT, Property.BORDER_RIGHT, Property.PADDING_RIGHT);
                float rendererHeight = margins[indexOfCorner > 1 ? 2 : 0] - GetSizeOfOneSide(cornerRenderer, Property.MARGIN_TOP
                    , Property.BORDER_TOP, Property.PADDING_TOP) - GetSizeOfOneSide(cornerRenderer, Property.MARGIN_BOTTOM
                    , Property.BORDER_BOTTOM, Property.PADDING_BOTTOM);
                cornerRenderer.SetProperty(Property.WIDTH, UnitValue.CreatePointValue(rendererWidth));
                cornerRenderer.SetProperty(Property.HEIGHT, UnitValue.CreatePointValue(rendererHeight));
                return cornerRenderer;
            }
            return null;
        }

        private IRenderer CreateRendererFromElement(IElement element, DocumentRenderer documentRenderer, PdfDocument
             pdfDocument) {
            if (element != null) {
                IRenderer renderer = element.CreateRendererSubTree();
                RemoveAreaBreaks(renderer);
                renderer.SetParent(documentRenderer);
                if (pdfDocument.IsTagged()) {
                    LayoutTaggingHelper taggingHelper = renderer.GetProperty<LayoutTaggingHelper>(Property.TAGGING_HELPER);
                    LayoutTaggingHelper.AddTreeHints(taggingHelper, renderer);
                }
                return renderer;
            }
            return null;
        }

        /// <summary>Gets rid of all page breaks that might have occurred inside page margin boxes because of the running elements.
        ///     </summary>
        /// <param name="renderer">root renderer of renderers subtree</param>
        private static void RemoveAreaBreaks(IRenderer renderer) {
            IList<IRenderer> areaBreaks = null;
            foreach (IRenderer child in renderer.GetChildRenderers()) {
                if (child is AreaBreakRenderer) {
                    if (areaBreaks == null) {
                        areaBreaks = new List<IRenderer>();
                    }
                    areaBreaks.Add(child);
                }
                else {
                    RemoveAreaBreaks(child);
                }
            }
            if (areaBreaks != null) {
                renderer.GetChildRenderers().RemoveAll(areaBreaks);
            }
        }

        private void DetermineSizes(int side) {
            float[][] marginsBordersPaddingsWidths = new float[][] { new float[4], new float[4], new float[4] };
            for (int i = 0; i < 3; i++) {
                if (renderers[side * 4 + i + 1] != null) {
                    marginsBordersPaddingsWidths[i][0] = GetSizeOfOneSide(renderers[side * 4 + i + 1], Property.MARGIN_TOP, Property
                        .BORDER_TOP, Property.PADDING_TOP);
                    marginsBordersPaddingsWidths[i][1] = GetSizeOfOneSide(renderers[side * 4 + i + 1], Property.MARGIN_RIGHT, 
                        Property.BORDER_RIGHT, Property.PADDING_RIGHT);
                    marginsBordersPaddingsWidths[i][2] = GetSizeOfOneSide(renderers[side * 4 + i + 1], Property.MARGIN_BOTTOM, 
                        Property.BORDER_BOTTOM, Property.PADDING_BOTTOM);
                    marginsBordersPaddingsWidths[i][3] = GetSizeOfOneSide(renderers[side * 4 + i + 1], Property.MARGIN_LEFT, Property
                        .BORDER_LEFT, Property.PADDING_LEFT);
                }
            }
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(margins[0], margins[1], margins[2], margins[3], false
                );
            IDictionary<String, PageMarginBoxContextNode> resolvedPMBMap = new Dictionary<String, PageMarginBoxContextNode
                >();
            for (int i = side * 4 + 1; i < side * 4 + 4; i++) {
                if (nodes[i] != null) {
                    resolvedPMBMap.Put(nodes[i].GetMarginBoxName(), nodes[i]);
                }
            }
            DimensionContainer[] dims = new DimensionContainer[3];
            String[] cssRuleName = GetRuleNames(side);
            float withoutMarginsWidthOrHeight = side % 2 == 0 ? withoutMargins.GetWidth() : withoutMargins.GetHeight();
            for (int i = 0; i < 3; i++) {
                if (side % 2 == 0) {
                    dims[i] = RetrievePageMarginBoxWidths(resolvedPMBMap.Get(cssRuleName[i]), renderers[side * 4 + i + 1], withoutMarginsWidthOrHeight
                        , marginsBordersPaddingsWidths[i][1] + marginsBordersPaddingsWidths[i][3]);
                }
                else {
                    dims[i] = RetrievePageMarginBoxHeights(resolvedPMBMap.Get(cssRuleName[i]), renderers[side * 4 + i + 1], margins
                        [side], withoutMarginsWidthOrHeight, marginsBordersPaddingsWidths[i][0] + marginsBordersPaddingsWidths
                        [i][2]);
                }
            }
            float centerOrMiddleCoord;
            float[] widthOrHeightResults;
            widthOrHeightResults = CalculatePageMarginBoxDimensions(dims[0], dims[1], dims[2], withoutMarginsWidthOrHeight
                );
            if (side % 2 == 0) {
                centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMarginsWidthOrHeight, widthOrHeightResults[
                    1], withoutMargins.GetLeft());
            }
            else {
                centerOrMiddleCoord = GetStartCoordForCenterOrMiddleBox(withoutMarginsWidthOrHeight, widthOrHeightResults[
                    1], withoutMargins.GetBottom());
            }
            Rectangle[] result = GetRectangles(side, withoutMargins, centerOrMiddleCoord, widthOrHeightResults);
            for (int i = 0; i < 3; i++) {
                if (nodes[side * 4 + i + 1] != null) {
                    nodes[side * 4 + i + 1].SetPageMarginBoxRectangle(new Rectangle(result[i]).IncreaseHeight(EPSILON));
                    UnitValue width = UnitValue.CreatePointValue(result[i].GetWidth() - marginsBordersPaddingsWidths[i][1] - marginsBordersPaddingsWidths
                        [i][3]);
                    UnitValue height = UnitValue.CreatePointValue(result[i].GetHeight() - marginsBordersPaddingsWidths[i][0] -
                         marginsBordersPaddingsWidths[i][2]);
                    if (Math.Abs(width.GetValue()) < EPSILON || Math.Abs(height.GetValue()) < EPSILON) {
                        renderers[side * 4 + i + 1] = null;
                    }
                    else {
                        renderers[side * 4 + i + 1].SetProperty(Property.WIDTH, width);
                        renderers[side * 4 + i + 1].SetProperty(Property.HEIGHT, height);
                    }
                }
            }
        }

        private String[] GetRuleNames(int side) {
            switch (side) {
                case 0: {
                    return new String[] { CssRuleName.TOP_LEFT, CssRuleName.TOP_CENTER, CssRuleName.TOP_RIGHT };
                }

                case 1: {
                    return new String[] { CssRuleName.RIGHT_TOP, CssRuleName.RIGHT_MIDDLE, CssRuleName.RIGHT_BOTTOM };
                }

                case 2: {
                    return new String[] { CssRuleName.BOTTOM_RIGHT, CssRuleName.BOTTOM_CENTER, CssRuleName.BOTTOM_LEFT };
                }

                case 3: {
                    return new String[] { CssRuleName.LEFT_BOTTOM, CssRuleName.LEFT_MIDDLE, CssRuleName.LEFT_TOP };
                }
            }
            return new String[3];
        }

        private Rectangle[] GetRectangles(int side, Rectangle withoutMargins, float centerOrMiddleCoord, float[] results
            ) {
            switch (side) {
                case 0: {
                    return new Rectangle[] { new Rectangle(withoutMargins.GetLeft(), withoutMargins.GetTop(), results[0], margins
                        [0]), new Rectangle(centerOrMiddleCoord, withoutMargins.GetTop(), results[1], margins[0]), new Rectangle
                        (withoutMargins.GetRight() - results[2], withoutMargins.GetTop(), results[2], margins[0]) };
                }

                case 1: {
                    return new Rectangle[] { new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop() - results[0], margins
                        [1], results[0]), new Rectangle(withoutMargins.GetRight(), centerOrMiddleCoord, margins[1], results[1]
                        ), new Rectangle(withoutMargins.GetRight(), withoutMargins.GetBottom(), margins[1], results[2]) };
                }

                case 2: {
                    return new Rectangle[] { new Rectangle(withoutMargins.GetRight() - results[0], 0, results[0], margins[2]), 
                        new Rectangle(centerOrMiddleCoord, 0, results[1], margins[2]), new Rectangle(withoutMargins.GetLeft(), 
                        0, results[2], margins[2]) };
                }

                case 3: {
                    return new Rectangle[] { new Rectangle(0, withoutMargins.GetBottom(), margins[3], results[0]), new Rectangle
                        (0, centerOrMiddleCoord, margins[3], results[1]), new Rectangle(0, withoutMargins.GetTop() - results[2
                        ], margins[3], results[2]) };
                }
            }
            return new Rectangle[3];
        }

        private float GetSizeOfOneSide(IRenderer renderer, int marginProperty, int borderProperty, int paddingProperty
            ) {
            float marginWidth = 0;
            float paddingWidth = 0;
            float borderWidth = 0;
            UnitValue temp = renderer.GetProperty<UnitValue>(marginProperty);
            if (null != temp) {
                marginWidth = temp.GetValue();
            }
            temp = renderer.GetProperty<UnitValue>(paddingProperty);
            if (null != temp) {
                paddingWidth = temp.GetValue();
            }
            Border border = renderer.GetProperty<Border>(borderProperty);
            if (null != border) {
                borderWidth = border.GetWidth();
            }
            return marginWidth + paddingWidth + borderWidth;
        }

        private DimensionContainer RetrievePageMarginBoxWidths(PageMarginBoxContextNode pmbcNode, IRenderer renderer
            , float maxWidth, float additionalWidthFix) {
            if (pmbcNode == null) {
                return null;
            }
            else {
                return new WidthDimensionContainer(pmbcNode, maxWidth, renderer, additionalWidthFix);
            }
        }

        private DimensionContainer RetrievePageMarginBoxHeights(PageMarginBoxContextNode pmbcNode, IRenderer renderer
            , float marginWidth, float maxHeight, float additionalHeightFix) {
            if (pmbcNode == null) {
                return null;
            }
            else {
                return new HeightDimensionContainer(pmbcNode, marginWidth, maxHeight, renderer, additionalHeightFix);
            }
        }

        /// <summary>
        /// See the algorithm detailed at https://www.w3.org/TR/css3-page/#margin-dimension
        /// Divide the available dimension along the A,B and C according to their properties.
        /// </summary>
        /// <param name="dimA">object containing the dimension-related properties of A</param>
        /// <param name="dimB">object containing the dimension-related properties of B</param>
        /// <param name="dimC">object containing the dimension-related properties of C</param>
        /// <param name="availableDimension">maximum available dimension that can be taken up</param>
        /// <returns>float[3] containing the distributed dimensions of A at [0], B at [1] and C at [2]</returns>
        private float[] CalculatePageMarginBoxDimensions(DimensionContainer dimA, DimensionContainer dimB, DimensionContainer
             dimC, float availableDimension) {
            float maxContentDimensionA = 0;
            float minContentDimensionA = 0;
            float maxContentDimensionB = 0;
            float minContentDimensionB = 0;
            float maxContentDimensionC = 0;
            float minContentDimensionC = 0;
            float[] dimensions = new float[3];
            if (IsContainerEmpty(dimA) && IsContainerEmpty(dimB) && IsContainerEmpty(dimC)) {
                return dimensions;
            }
            //Calculate widths
            //Check if B is present
            if (IsContainerEmpty(dimB)) {
                //Single box present
                if (IsContainerEmpty(dimA)) {
                    if (dimC.IsAutoDimension()) {
                        //Allocate everything to C
                        dimensions[2] = availableDimension;
                    }
                    else {
                        dimensions[2] = dimC.dimension;
                    }
                }
                else {
                    if (IsContainerEmpty(dimC)) {
                        if (dimA.IsAutoDimension()) {
                            //Allocate everything to A
                            dimensions[0] = availableDimension;
                        }
                        else {
                            dimensions[0] = dimA.dimension;
                        }
                    }
                    else {
                        if (dimA.IsAutoDimension() && dimC.IsAutoDimension()) {
                            //Gather input
                            maxContentDimensionA = dimA.maxContentDimension;
                            minContentDimensionA = dimA.minContentDimension;
                            maxContentDimensionC = dimC.maxContentDimension;
                            minContentDimensionC = dimC.minContentDimension;
                            float[] distributedWidths = DistributeDimensionBetweenTwoBoxes(maxContentDimensionA, minContentDimensionA, 
                                maxContentDimensionC, minContentDimensionC, availableDimension);
                            dimensions = new float[] { distributedWidths[0], 0f, distributedWidths[1] };
                        }
                        else {
                            if (!dimA.IsAutoDimension()) {
                                dimensions[0] = dimA.dimension;
                            }
                            else {
                                dimensions[0] = availableDimension - dimC.dimension;
                            }
                            if (!dimC.IsAutoDimension()) {
                                dimensions[2] = dimC.dimension;
                            }
                            else {
                                dimensions[2] = availableDimension - dimA.dimension;
                            }
                        }
                    }
                }
            }
            else {
                //Check for edge cases
                if (!IsContainerEmpty(dimA)) {
                    if (dimA.IsAutoDimension()) {
                        maxContentDimensionA = dimA.maxContentDimension;
                        minContentDimensionA = dimA.minContentDimension;
                    }
                    else {
                        maxContentDimensionA = dimA.dimension;
                        minContentDimensionA = dimA.dimension;
                    }
                }
                if (!IsContainerEmpty(dimC)) {
                    if (dimC.IsAutoDimension()) {
                        maxContentDimensionC = dimC.maxContentDimension;
                        minContentDimensionC = dimC.minContentDimension;
                    }
                    else {
                        maxContentDimensionC = dimC.dimension;
                        minContentDimensionC = dimC.dimension;
                    }
                }
                if (dimB.IsAutoDimension()) {
                    //Construct box AC
                    float maxContentWidthAC;
                    float minContentWidthAC;
                    maxContentWidthAC = 2 * Math.Max(maxContentDimensionA, maxContentDimensionC);
                    minContentWidthAC = 2 * Math.Max(minContentDimensionA, minContentDimensionC);
                    //Determine width box B
                    maxContentDimensionB = dimB.maxContentDimension;
                    minContentDimensionB = dimB.minContentDimension;
                    float[] distributedDimensions = DistributeDimensionBetweenTwoBoxes(maxContentDimensionB, minContentDimensionB
                        , maxContentWidthAC, minContentWidthAC, availableDimension);
                    //Determine width boxes A & C
                    float newAvailableDimension = (availableDimension - distributedDimensions[0]) / 2;
                    dimensions = new float[] { newAvailableDimension, distributedDimensions[0], newAvailableDimension };
                }
                else {
                    dimensions[1] = dimB.dimension;
                    float newAvailableDimension = (availableDimension - dimensions[1]) / 2;
                    if (newAvailableDimension > float.MaxValue - MinMaxWidthUtils.GetEps()) {
                        newAvailableDimension = float.MaxValue - MinMaxWidthUtils.GetEps();
                    }
                    dimensions[0] = Math.Min(maxContentDimensionA, newAvailableDimension) + MinMaxWidthUtils.GetEps();
                    dimensions[2] = Math.Min(maxContentDimensionC, newAvailableDimension) + MinMaxWidthUtils.GetEps();
                }
                SetManualDimension(dimA, dimensions, 0);
                SetManualDimension(dimC, dimensions, 2);
            }
            if (RecalculateIfNecessary(dimA, dimensions, 0) || RecalculateIfNecessary(dimB, dimensions, 1) || RecalculateIfNecessary
                (dimC, dimensions, 2)) {
                return CalculatePageMarginBoxDimensions(dimA, dimB, dimC, availableDimension);
            }
            RemoveNegativeValues(dimensions);
            return dimensions;
        }

        private bool IsContainerEmpty(DimensionContainer container) {
            return container == null || Math.Abs(container.maxContentDimension) < EPSILON;
        }

        private void RemoveNegativeValues(float[] dimensions) {
            for (int i = 0; i < dimensions.Length; i++) {
                if (dimensions[i] < 0) {
                    dimensions[i] = 0;
                }
            }
        }

        /// <summary>Calculate the starting coordinate in a given dimension for a center of middle box</summary>
        /// <param name="availableDimension">size of the available area</param>
        /// <param name="dimensionResult">the calculated dimensions of the middle (center) box</param>
        /// <param name="offset">offset from the start of the page (page margins and padding included)</param>
        /// <returns>starting coordinate in a given dimension for a center of middle box</returns>
        private float GetStartCoordForCenterOrMiddleBox(float availableDimension, float dimensionResult, float offset
            ) {
            return offset + (availableDimension - dimensionResult) / 2;
        }

        /// <summary>Set the calculated dimension to the manually set dimension in the passed float array</summary>
        /// <param name="dim">Dimension Container containing the manually set dimension</param>
        /// <param name="dimensions">array of calculated auto values for boxes in the given dimension</param>
        /// <param name="index">position in the array to replace</param>
        private void SetManualDimension(DimensionContainer dim, float[] dimensions, int index) {
            if (dim != null && !dim.IsAutoDimension()) {
                dimensions[index] = dim.dimension;
            }
        }

        /// <summary>Distribute the available dimension between two boxes A and C based on their content-needs.</summary>
        /// <remarks>
        /// Distribute the available dimension between two boxes A and C based on their content-needs.
        /// The box with more content will get more space assigned
        /// </remarks>
        /// <param name="maxContentDimensionA">maximum of the dimension the content in A occupies</param>
        /// <param name="minContentDimensionA">minimum of the dimension the content in A occupies</param>
        /// <param name="maxContentDimensionC">maximum of the dimension the content in C occupies</param>
        /// <param name="minContentDimensionC">minimum of the dimension the content in C occupies</param>
        /// <param name="availableDimension">maximum available dimension to distribute</param>
        /// <returns>float[2], distributed dimension for A in [0], distributed dimension for B in [1]</returns>
        private float[] DistributeDimensionBetweenTwoBoxes(float maxContentDimensionA, float minContentDimensionA, 
            float maxContentDimensionC, float minContentDimensionC, float availableDimension) {
            //calculate based on flex space
            //Determine flex factor
            float maxSum = maxContentDimensionA + maxContentDimensionC;
            float minSum = minContentDimensionA + minContentDimensionC;
            if (maxSum < availableDimension) {
                return CalculateDistribution(maxContentDimensionA, maxContentDimensionC, maxContentDimensionA, maxContentDimensionC
                    , maxSum, availableDimension);
            }
            else {
                if (minSum < availableDimension) {
                    return CalculateDistribution(minContentDimensionA, minContentDimensionC, maxContentDimensionA - minContentDimensionA
                        , maxContentDimensionC - minContentDimensionC, maxSum - minSum, availableDimension);
                }
            }
            return CalculateDistribution(minContentDimensionA, minContentDimensionC, minContentDimensionA, minContentDimensionC
                , minSum, availableDimension);
        }

        private float[] CalculateDistribution(float argA, float argC, float flexA, float flexC, float sum, float availableDimension
            ) {
            float flexRatioA;
            float flexRatioC;
            float flexSpace;
            if (CssUtils.CompareFloats(sum, 0f)) {
                flexRatioA = 1;
                flexRatioC = 1;
            }
            else {
                flexRatioA = flexA / sum;
                flexRatioC = flexC / sum;
            }
            flexSpace = availableDimension - (argA + argC);
            return new float[] { argA + flexRatioA * flexSpace, argC + flexRatioC * flexSpace };
        }

        /// <summary>Check if a calculated dimension value needs to be recalculated</summary>
        /// <param name="dim">Dimension container containing min and max dimension info</param>
        /// <param name="dimensions">array of calculated auto values for boxes in the given dimension</param>
        /// <param name="index">position in the array to look at</param>
        /// <returns><c>true</c> if the values in dimensions trigger a recalculation, <c>false</c> otherwise</returns>
        private bool RecalculateIfNecessary(DimensionContainer dim, float[] dimensions, int index) {
            if (dim != null) {
                if (dimensions[index] < dim.minDimension && dim.IsAutoDimension()) {
                    dim.dimension = dim.minDimension;
                    return true;
                }
                if (dimensions[index] > dim.maxDimension && dim.IsAutoDimension()) {
                    dim.dimension = dim.maxDimension;
                    return true;
                }
            }
            return false;
        }

        /// <summary>Calculate the margin boxes given the list of margin boxes that have generated content</summary>
        /// <returns>
        /// Rectangle[12] containing the calculated bounding boxes of the margin-box-nodes. Rectangles with 0 width and/or height
        /// refer to empty boxes. The order is TLC(top-left-corner)-TL-TC-TY-TRC-RT-RM-RB-RBC-BR-BC-BL-BLC-LB-LM-LT
        /// </returns>
        private Rectangle[] CalculateMarginBoxRectanglesCornersOnly() {
            float topMargin = margins[0];
            float rightMargin = margins[1];
            float bottomMargin = margins[2];
            float leftMargin = margins[3];
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(topMargin, rightMargin, bottomMargin, leftMargin, 
                false);
            //Define corner boxes
            Rectangle tlc = new Rectangle(0, withoutMargins.GetTop(), leftMargin, topMargin);
            Rectangle trc = new Rectangle(withoutMargins.GetRight(), withoutMargins.GetTop(), rightMargin, topMargin);
            Rectangle blc = new Rectangle(0, 0, leftMargin, bottomMargin);
            Rectangle brc = new Rectangle(withoutMargins.GetRight(), 0, rightMargin, bottomMargin);
            //Group & return results
            Rectangle[] groupedRectangles = new Rectangle[] { tlc, null, null, null, trc, null, null, null, brc, null, 
                null, null, blc, null, null, null };
            return groupedRectangles;
        }

        /// <summary>Calculate containing block sizes for margin box.</summary>
        /// <param name="marginBoxInd">the margin box index</param>
        /// <param name="pageMarginBoxRectangle">
        /// a
        /// <see cref="iText.Kernel.Geom.Rectangle"/>
        /// defining dimensions of the page margin box corresponding to the given index
        /// </param>
        /// <returns>the corresponding rectangle</returns>
        private Rectangle CalculateContainingBlockSizesForMarginBox(int marginBoxInd, Rectangle pageMarginBoxRectangle
            ) {
            if (marginBoxInd == 0 || marginBoxInd == 4 || marginBoxInd == 8 || marginBoxInd == 12) {
                return pageMarginBoxRectangle;
            }
            Rectangle withoutMargins = pageSize.Clone().ApplyMargins(margins[0], margins[1], margins[2], margins[3], false
                );
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
                        return new Rectangle(margins[3], withoutMargins.GetHeight());
                    }
                }
            }
        }

        /// <summary>Maps a margin box name to an index.</summary>
        /// <param name="marginBoxName">the margin box name</param>
        /// <returns>the index corresponding with the margin box name</returns>
        internal virtual int MapMarginBoxNameToIndex(String marginBoxName) {
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
    }
}
