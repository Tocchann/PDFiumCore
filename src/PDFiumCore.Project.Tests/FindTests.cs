using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFiumCore.Project.Tests
{
	public class FindTests
	{
		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			fpdfview.FPDF_InitLibrary();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			fpdfview.FPDF_DestroyLibrary();
		}
		[SetUp]
		public void Setup()
		{
		}
		[Test]
		public void Test_FindStart()
		{
			var document = fpdfview.FPDF_LoadDocument("pdf-sample.pdf", null);
			Assert.That( document, Is.Not.Null );
			var page = fpdfview.FPDF_LoadPage( document, 0 );
			Assert.That( page, Is.Not.Null );
			var textPage = fpdf_text.FPDFTextLoadPage( page );
			Assert.That( textPage, Is.Not.Null );

			var findHandle = fpdf_text.FPDFTextFindStart(textPage, "PDF", 0, 0);
			Assert.That( findHandle, Is.Not.Null );
			Assert.That( fpdf_text.FPDFTextFindNext( findHandle ), Is.Not.Zero );
			Assert.That( fpdf_text.FPDFTextGetSchResultIndex( findHandle ), Is.EqualTo( 14 ) );
			Assert.That( fpdf_text.FPDFTextGetSchCount( findHandle ), Is.EqualTo( 3 ) );

			fpdf_text.FPDFTextFindClose(findHandle);
			fpdfview.FPDF_ClosePage(page);
			fpdfview.FPDF_CloseDocument(document);
		}
	}
}
