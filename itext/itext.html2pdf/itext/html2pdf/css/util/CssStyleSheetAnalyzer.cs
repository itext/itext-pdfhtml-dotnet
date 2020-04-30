using System;
using System.Collections.Generic;
using iText.Html2pdf.Css;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Css.Media;
using iText.StyledXmlParser.Css.Page;
using iText.StyledXmlParser.Css.Parse;

namespace iText.Html2pdf.Css.Util {
    /// <summary>Helper class to analyze the CSS stylesheet, e.g. for presence of some constructs</summary>
    public class CssStyleSheetAnalyzer {
        private CssStyleSheetAnalyzer() {
        }

        /// <summary>Helper method to check if counter(pages) or counters(page) is present anywhere in the CSS.</summary>
        /// <remarks>
        /// Helper method to check if counter(pages) or counters(page) is present anywhere in the CSS.
        /// If the presence is detected, it may require additional treatment
        /// </remarks>
        /// <param name="styleSheet">CSS stylesheet to analyze</param>
        public static bool CheckPagesCounterPresence(CssStyleSheet styleSheet) {
            return CheckPagesCounterPresence(styleSheet.GetStatements());
        }

        private static bool CheckPagesCounterPresence(ICollection<CssStatement> statements) {
            bool pagesCounterPresent = false;
            foreach (CssStatement statement in statements) {
                if (statement is CssMarginRule) {
                    pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence(((CssMarginRule)statement).GetStatements
                        ());
                }
                else {
                    if (statement is CssMediaRule) {
                        pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence(((CssMediaRule)statement).GetStatements
                            ());
                    }
                    else {
                        if (statement is CssPageRule) {
                            pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence(((CssPageRule)statement).GetStatements
                                ());
                        }
                        else {
                            if (statement is CssRuleSet) {
                                pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence((CssRuleSet)statement);
                            }
                        }
                    }
                }
            }
            return pagesCounterPresent;
        }

        private static bool CheckPagesCounterPresence(CssRuleSet ruleSet) {
            bool pagesCounterPresent = false;
            foreach (CssDeclaration declaration in ruleSet.GetImportantDeclarations()) {
                pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence(declaration);
            }
            foreach (CssDeclaration declaration in ruleSet.GetNormalDeclarations()) {
                pagesCounterPresent = pagesCounterPresent || CheckPagesCounterPresence(declaration);
            }
            return pagesCounterPresent;
        }

        private static bool CheckPagesCounterPresence(CssDeclaration declaration) {
            bool pagesCounterPresent = false;
            // MDN: The counters() function can be used with any CSS property, but support for properties other
            // than content is experimental, and support for the type-or-unit parameter is sparse.
            // iText also does not support counter(pages) anywhere else for now
            if (CssConstants.CONTENT.Equals(declaration.GetProperty())) {
                CssDeclarationValueTokenizer tokenizer = new CssDeclarationValueTokenizer(declaration.GetExpression());
                CssDeclarationValueTokenizer.Token token;
                while ((token = tokenizer.GetNextValidToken()) != null) {
                    if (!token.IsString()) {
                        if (token.GetValue().StartsWith(CssConstants.COUNTERS + "(")) {
                            String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTERS.Length + 1, token.GetValue().Length -
                                 1);
                            String[] @params = iText.IO.Util.StringUtil.Split(paramsStr, ",");
                            pagesCounterPresent = pagesCounterPresent || CheckCounterFunctionParamsForPagesReferencePresence(@params);
                        }
                        else {
                            if (token.GetValue().StartsWith(CssConstants.COUNTER + "(")) {
                                String paramsStr = token.GetValue().JSubstring(CssConstants.COUNTER.Length + 1, token.GetValue().Length - 
                                    1);
                                String[] @params = iText.IO.Util.StringUtil.Split(paramsStr, ",");
                                pagesCounterPresent = pagesCounterPresent || CheckCounterFunctionParamsForPagesReferencePresence(@params);
                            }
                        }
                    }
                }
            }
            return pagesCounterPresent;
        }

        private static bool CheckCounterFunctionParamsForPagesReferencePresence(String[] @params) {
            return @params.Length > 0 && CssConstants.PAGES.Equals(@params[0].Trim());
        }
    }
}
