/*
This file is part of the iText (R) project.
Copyright (c) 1998-2024 Apryse Group NV
Authors: Apryse Software.

This program is offered under a commercial and under the AGPL license.
For commercial licensing, contact us at https://itextpdf.com/sales.  For AGPL licensing, see below.

AGPL licensing:
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using iText.Html2pdf.Attach;
using iText.Html2pdf.Attach.Util;
using iText.Html2pdf.Attach.Wrapelement;
using iText.Html2pdf.Css;
using iText.Html2pdf.Html;
using iText.Layout;
using iText.Layout.Element;
using iText.StyledXmlParser.Node;

namespace iText.Html2pdf.Attach.Impl.Tags {
    /// <summary>
    /// TagWorker class for the
    /// <c>table</c>
    /// element.
    /// </summary>
    public class TableTagWorker : ITagWorker, IDisplayAware {
        /// <summary>The table wrapper.</summary>
        private TableWrapper tableWrapper;

        /// <summary>The table.</summary>
        private Table table;

        /// <summary>The footer.</summary>
        private bool footer;

        /// <summary>The header.</summary>
        private bool header;

        /// <summary>The parent tag worker.</summary>
        private ITagWorker parentTagWorker;

        /// <summary>The colgroups helper.</summary>
        private WaitingColgroupsHelper colgroupsHelper;

        /// <summary>The display value.</summary>
        private String display;

        /// <summary>
        /// Creates a new
        /// <see cref="TableTagWorker"/>
        /// instance.
        /// </summary>
        /// <param name="element">the element</param>
        /// <param name="context">the context</param>
        public TableTagWorker(IElementNode element, ProcessorContext context) {
            String str = element.GetStyles().Get(CssConstants.DIRECTION);
            bool isRtl = "rtl".Equals(str);
            tableWrapper = new TableWrapper(isRtl);
            parentTagWorker = context.GetState().Empty() ? null : context.GetState().Top();
            if (parentTagWorker is iText.Html2pdf.Attach.Impl.Tags.TableTagWorker) {
                ((iText.Html2pdf.Attach.Impl.Tags.TableTagWorker)parentTagWorker).ApplyColStyles();
            }
            else {
                colgroupsHelper = new WaitingColgroupsHelper(element);
            }
            display = element.GetStyles() != null ? element.GetStyles().Get(CssConstants.DISPLAY) : null;
            String lang = element.GetAttribute(AttributeConstants.LANG);
            if (lang != null) {
                tableWrapper.SetLang(lang);
            }
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processEnd(com.itextpdf.html2pdf.html.node.IElementNode, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual void ProcessEnd(IElementNode element, ProcessorContext context) {
            table = tableWrapper.ToTable(colgroupsHelper);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processContent(java.lang.String, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessContent(String content, ProcessorContext context) {
            return parentTagWorker != null && parentTagWorker.ProcessContent(content, context);
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#processTagChild(com.itextpdf.html2pdf.attach.ITagWorker, com.itextpdf.html2pdf.attach.ProcessorContext)
        */
        public virtual bool ProcessTagChild(ITagWorker childTagWorker, ProcessorContext context) {
            if (childTagWorker is TrTagWorker) {
                TableRowWrapper wrapper = ((TrTagWorker)childTagWorker).GetTableRowWrapper();
                tableWrapper.NewRow();
                foreach (Cell cell in wrapper.GetCells()) {
                    tableWrapper.AddCell(cell);
                }
                return true;
            }
            else {
                if (childTagWorker is iText.Html2pdf.Attach.Impl.Tags.TableTagWorker) {
                    if (((iText.Html2pdf.Attach.Impl.Tags.TableTagWorker)childTagWorker).header) {
                        Table header = ((iText.Html2pdf.Attach.Impl.Tags.TableTagWorker)childTagWorker).tableWrapper.ToTable(colgroupsHelper
                            );
                        String headerLang = header.GetAccessibilityProperties().GetLanguage();
                        tableWrapper.SetHeaderLang(headerLang);
                        for (int i = 0; i < header.GetNumberOfRows(); i++) {
                            tableWrapper.NewHeaderRow();
                            for (int j = 0; j < header.GetNumberOfColumns(); j++) {
                                Cell headerCell = header.GetCell(i, j);
                                if (headerCell != null) {
                                    tableWrapper.AddHeaderCell(headerCell);
                                }
                            }
                        }
                        return true;
                    }
                    else {
                        if (((iText.Html2pdf.Attach.Impl.Tags.TableTagWorker)childTagWorker).footer) {
                            Table footer = ((iText.Html2pdf.Attach.Impl.Tags.TableTagWorker)childTagWorker).tableWrapper.ToTable(colgroupsHelper
                                );
                            String footerLang = footer.GetAccessibilityProperties().GetLanguage();
                            tableWrapper.SetFooterLang(footerLang);
                            for (int i = 0; i < footer.GetNumberOfRows(); i++) {
                                tableWrapper.NewFooterRow();
                                for (int j = 0; j < footer.GetNumberOfColumns(); j++) {
                                    Cell footerCell = footer.GetCell(i, j);
                                    if (footerCell != null) {
                                        tableWrapper.AddFooterCell(footerCell);
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
                else {
                    if (childTagWorker is ColgroupTagWorker) {
                        if (colgroupsHelper != null) {
                            colgroupsHelper.Add(((ColgroupTagWorker)childTagWorker).GetColgroup().FinalizeCols());
                            return true;
                        }
                    }
                    else {
                        if (childTagWorker is CaptionTagWorker) {
                            tableWrapper.SetCaption((Div)childTagWorker.GetElementResult());
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.ITagWorker#getElementResult()
        */
        public virtual IPropertyContainer GetElementResult() {
            return table;
        }

        /* (non-Javadoc)
        * @see com.itextpdf.html2pdf.attach.impl.tags.IDisplayAware#getDisplay()
        */
        public virtual String GetDisplay() {
            return display;
        }

        /// <summary>
        /// Method to indicate that this is actually a
        /// <see cref="TableFooterTagWorker"/>
        /// instance.
        /// </summary>
        public virtual void SetFooter() {
            footer = true;
        }

        /// <summary>
        /// Method to indicate that this is actually a
        /// <see cref="TableHeaderTagWorker"/>
        /// instance.
        /// </summary>
        public virtual void SetHeader() {
            header = true;
        }

        /// <summary>Applies the column styles.</summary>
        public virtual void ApplyColStyles() {
            if (colgroupsHelper != null) {
                colgroupsHelper.ApplyColStyles();
            }
        }
    }
}
