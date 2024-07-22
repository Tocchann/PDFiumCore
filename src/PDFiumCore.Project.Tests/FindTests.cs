using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

			var contextLengthOneHalf = 20;
			var foundCharIndex = fpdf_text.FPDFTextGetSchResultIndex( findHandle );
			var foundTextIndex = fpdf_searchex.FPDFTextGetTextIndexFromCharIndex( textPage, foundCharIndex );
			var foundTextCount = fpdf_text.FPDFTextGetSchCount( findHandle );
			Assert.That( foundCharIndex, Is.EqualTo( 14 ) );
			Assert.That( foundTextIndex, Is.EqualTo( 14 ) );
			Assert.That( foundTextCount, Is.EqualTo( 3 ) );

			var charsOnPage = fpdf_text.FPDFTextCountChars( textPage );
			int startPosition = Math.Max( foundTextIndex - contextLengthOneHalf, 0 );
			int endPosition = Math.Min( foundTextIndex + foundTextCount + contextLengthOneHalf, charsOnPage );
			var result = fpdf_text.FPDFTextGetText( textPage, startPosition, endPosition - startPosition, out var text );
			Assert.That( result, Is.True );
			Assert.That( text.Length, Is.EqualTo( endPosition - startPosition ) );

			Assert.That( text.Contains( "PDF" ), Is.True );
			Trace.WriteLine( $"Found text: {text}" );

			fpdf_text.FPDFTextFindClose(findHandle);
			fpdfview.FPDF_ClosePage(page);
			fpdfview.FPDF_CloseDocument(document);
		}
		[Test]
		public void Test_GetText()
		{
			var document = fpdfview.FPDF_LoadDocument( "pdf-sample.pdf", null );
			Assert.That( document, Is.Not.Null );
			var page = fpdfview.FPDF_LoadPage( document, 0 );
			Assert.That( page, Is.Not.Null );
			var textPage = fpdf_text.FPDFTextLoadPage( page );
			Assert.That( textPage, Is.Not.Null );

			var charsOnPage = fpdf_text.FPDFTextCountChars( textPage );
			Assert.That( charsOnPage, Is.GreaterThan( 0 ) );
			var result = fpdf_text.FPDFTextGetText( textPage, 0, charsOnPage, out var text );
			Assert.That( result, Is.True );
			Assert.That( text.Length, Is.EqualTo( charsOnPage ) );

			Trace.WriteLine( $"Found text: {text}" );

			fpdfview.FPDF_ClosePage( page );
			fpdfview.FPDF_CloseDocument( document );
		}
		[Test]
		public void Test_FindLoop()
		{
			var document = fpdfview.FPDF_LoadDocument( "pdf-sample.pdf", null );
			Assert.That( document, Is.Not.Null );
			var page = fpdfview.FPDF_LoadPage( document, 0 );
			Assert.That( page, Is.Not.Null );
			var textPage = fpdf_text.FPDFTextLoadPage( page );
			Assert.That( textPage, Is.Not.Null );

			var findHandle = fpdf_text.FPDFTextFindStart( textPage, "PDF", 0, 0 );
			Assert.That( findHandle, Is.Not.Null );
			while( fpdf_text.FPDFTextFindNext( findHandle ) > 0 )
			{
				var foundCharIndex = fpdf_text.FPDFTextGetSchResultIndex( findHandle );
				var foundTextIndex = fpdf_searchex.FPDFTextGetTextIndexFromCharIndex( textPage, foundCharIndex );
				var foundTextCount = fpdf_text.FPDFTextGetSchCount( findHandle );

				var charsOnPage = fpdf_text.FPDFTextCountChars( textPage );
				int startPosition = Math.Max( foundTextIndex - 20, 0 );
				int endPosition = Math.Min( foundTextIndex + foundTextCount + 20, charsOnPage );
				var result = fpdf_text.FPDFTextGetText( textPage, startPosition, endPosition - startPosition, out var text );
				Assert.That( result, Is.True );
				Assert.That( text.Length, Is.EqualTo( endPosition - startPosition ) );

				Assert.That( text.Contains( "PDF" ), Is.True );
				Trace.WriteLine( $"Found text: {text}" );
			}
			fpdf_text.FPDFTextFindClose( findHandle );
			fpdfview.FPDF_ClosePage( page );
			fpdfview.FPDF_CloseDocument( document );
		}
	}
}
