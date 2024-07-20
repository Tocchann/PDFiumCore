using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using __CallingConvention = global::System.Runtime.InteropServices.CallingConvention;
using __IntPtr = global::System.IntPtr;

namespace PDFiumCore
{
	public unsafe partial class fpdf_text
	{
		public partial struct __Internal
		{
			[SuppressUnmanagedCodeSecurity, DllImport( "pdfium", EntryPoint = "FPDFText_FindStart", CallingConvention = __CallingConvention.Cdecl )]
			internal static extern __IntPtr FPDFTextFindStart( __IntPtr text_page, [MarshalAs(UnmanagedType.LPWStr)]string findwhat, uint flags, int start_index );
		}
		/// <summary>
		/// <para>Function: FPDFText_FindStart</para>
		/// <para>Start a search.</para>
		/// <para>Parameters:</para>
		/// <para>text_page   -   Handle to a text page information structure. Returned by FPDFText_LoadPage function.</para>
		/// <para>findwhat    -   A unicode match pattern.</para>
		/// <para>flags       -   Option flags.</para>
		/// <para>start_index -   Start from this character. -1 for end of the page.</para>
		/// <para>Return Value:</para>
		/// <para>A handle for the search context. FPDFText_FindClose must be called</para>
		/// <para>to release this handle.</para>
		/// </summary>
		public static global::PDFiumCore.FpdfSchhandleT FPDFTextFindStart( global::PDFiumCore.FpdfTextpageT text_page, string findwhat, uint flags, int start_index )
		{
			var __arg0 = text_page is null ? __IntPtr.Zero : text_page.__Instance;
			var __ret = __Internal.FPDFTextFindStart( __arg0, findwhat, flags, start_index );
			var __result0 = global::PDFiumCore.FpdfSchhandleT.__GetOrCreateInstance( __ret, false );
			return __result0;
		}
	}
}
