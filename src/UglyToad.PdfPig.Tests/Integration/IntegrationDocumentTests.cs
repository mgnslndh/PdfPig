﻿namespace UglyToad.PdfPig.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public class IntegrationDocumentTests
    {
        private static readonly Lazy<string> DocumentFolder = new Lazy<string>(() => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Integration", "Documents")));

        [Theory]
        [MemberData(nameof(GetAllDocuments))]
        public void CanReadAllPages(string documentName)
        {
            // Add the full path back on, we removed it so we could see it in the test explorer.
            documentName = Path.Combine(DocumentFolder.Value, documentName);

            using (var document = PdfDocument.Open(documentName, new ParsingOptions{ UseLenientParsing = false}))
            {
                for (var i = 0; i < document.NumberOfPages; i++)
                {
                    document.GetPage(i + 1);
                }
            }
        }

        public static IEnumerable<object[]> GetAllDocuments
        {
            get
            {
                var files = Directory.GetFiles(DocumentFolder.Value, "*.pdf");

                // Return the shortname so we can see it in the test explorer.
                return files.Select(x => new object[] {Path.GetFileName(x)});
            }
        }
    }
}
