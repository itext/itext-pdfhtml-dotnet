/*
This file is part of the iText (R) project.
Copyright (c) 1998-2025 Apryse Group NV
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
using System.Threading;
using System.Threading.Tasks;
using iText.Commons;
using iText.IO.Source;
using iText.Test;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace iText.Html2pdf {
    [Category("IntegrationTest")]
    public class HtmlConverterMultiThreadedTest : ExtendedITextTest
    {
        private static readonly ILogger LOGGER = ITextLogManager.GetLogger(typeof(iText.Html2pdf.HtmlConverterMultiThreadedTest
        ));
        
        private static readonly String SOURCE_FOLDER =
            TestUtil.GetParentProjectDirectory(TestContext.CurrentContext.TestDirectory) +
            "/resources/itext/html2pdf/HtmlConverterMultiThreadedTest/";

        [Test]
        public void MultiThreadedHtmlToPdfConversionTest()
        {
            var runcount = 75;
            
            //Set time limit of 2 seconds
            var cts = new CancellationTokenSource();
            cts.CancelAfter(2000 * 60);
            
            var options = new ParallelOptions();
            options.CancellationToken = cts.Token;
            options.MaxDegreeOfParallelism = 100;

            ThreadPool.SetMinThreads(runcount, runcount);

            var results = new ParallelLoopResult();

            try
            {
                results = Parallel.For(0, runcount, options, (i, state) =>
                {
                    var outStr = new ByteArrayOutputStream();
                    HtmlConverter.ConvertToPdf(SOURCE_FOLDER + "basicHtml.html", outStr);
                });
            }
            catch (OperationCanceledException e)
            {
                LOGGER.LogError("Thread timed out after 2 seconds.");
            }
            finally
            {
                cts.Dispose();
            }

            Assert.IsTrue(results.IsCompleted, "Not all tasks were successful.");
        }
    }
}
