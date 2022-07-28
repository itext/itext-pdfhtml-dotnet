/*
This file is part of the iText (R) project.
Copyright (c) 1998-2022 iText Group NV
Authors: iText Software.

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
using System.IO;
using iText.Kernel.Utils;
using iText.Test;
using iText.Test.Attributes;

namespace iText.Html2pdf {
    [NUnit.Framework.Category("Integration test")]
    public class SurrogatePairsTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/html2pdf/SurrogatePairsTests/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/html2pdf/SurrogatePairsTests/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            CreateOrClearDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        public virtual void SurrogatePairFrom2Chars() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairFrom2Chars.html"), new FileInfo(destinationFolder
                 + "surrogatePairFrom2Chars.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairFrom2Chars.pdf"
                , sourceFolder + "cmp_surrogatePairFrom2Chars.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        public virtual void SurrogatePair2Pairs() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePair2Pairs.html"), new FileInfo(destinationFolder
                 + "surrogatePair2Pairs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePair2Pairs.pdf"
                , sourceFolder + "cmp_surrogatePair2Pairs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        public virtual void SurrogatePairFullCharacter() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairFullCharacter.html"), new FileInfo(destinationFolder
                 + "surrogatePairFullCharacter.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairFullCharacter.pdf"
                , sourceFolder + "cmp_surrogatePairFullCharacter.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-3307: It is required to update cmp files when the ticket will be implemented.
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void SurrogatePairCombingFullSurrs() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombingFullSurrs.html"), new FileInfo
                (destinationFolder + "surrogatePairCombingFullSurrs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombingFullSurrs.pdf"
                , sourceFolder + "cmp_surrogatePairCombingFullSurrs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        //TODO DEVSIX-3307: It is required to update cmp files when the ticket will be implemented.
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.FONT_SUBSET_ISSUE)]
        public virtual void SurrogatePairCombingFullSurrsWithNoSurrs() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombingFullSurrsWithNoSurrs.html"), new 
                FileInfo(destinationFolder + "surrogatePairCombingFullSurrsWithNoSurrs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombingFullSurrsWithNoSurrs.pdf"
                , sourceFolder + "cmp_surrogatePairCombingFullSurrsWithNoSurrs.pdf", destinationFolder));
        }

        [NUnit.Framework.Test]
        [LogMessage(iText.IO.Logs.IoLogMessageConstant.TYPOGRAPHY_NOT_FOUND, Count = 2)]
        public virtual void SurrogatePairCombinationOf3TypesPairs() {
            HtmlConverter.ConvertToPdf(new FileInfo(sourceFolder + "surrogatePairCombinationOf3TypesPairs.html"), new 
                FileInfo(destinationFolder + "surrogatePairCombinationOf3TypesPairs.pdf"));
            NUnit.Framework.Assert.IsNull(new CompareTool().CompareByContent(destinationFolder + "surrogatePairCombinationOf3TypesPairs.pdf"
                , sourceFolder + "cmp_surrogatePairCombinationOf3TypesPairs.pdf", destinationFolder));
        }
    }
}
