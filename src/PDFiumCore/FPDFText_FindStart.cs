using System;
using System.Runtime.InteropServices;
using System.Security;

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

			[SuppressUnmanagedCodeSecurity, DllImport( "pdfium", EntryPoint = "FPDFText_GetText", CallingConvention = __CallingConvention.Cdecl )]
			internal static extern int FPDFTextGetText( __IntPtr text_page, int start_index, int count, __IntPtr result );
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
		/// <summary>
		/// <para>Function: FPDFText_GetText</para>
		/// <para>Extract unicode text string from the page.</para>
		/// <para>Parameters:</para>
		/// <para>text_page   -   Handle to a text page information structure. Returned by FPDFText_LoadPage function.</para>
		/// <para>start_index -   Index for the start characters.</para>
		/// <para>count       -   Number of UCS-2 values to be extracted.</para>
		/// <para>result      -   A buffer (allocated by application) receiving the extracted UCS-2 values. The buffer must be able to hold `count` UCS-2 values plus a terminator.</para>
		/// <para>Return Value:</para>
		/// <para>Number of characters written into the result buffer, including the</para>
		/// <para>trailing terminator.</para>
		/// <para>Comments: This function ignores characters without UCS-2 representations. It considers all characters on the page, even those that are not visible when the page has a cropbox. To filter out the characters outside of the cropbox, use FPDF_GetPageBoundingBox() and FPDFText_GetCharBox().</para>
		/// </summary>
		public static bool FPDFTextGetText( global::PDFiumCore.FpdfTextpageT text_page, int start_index, int count, out string result )
		{
			var __arg0 = text_page is null ? __IntPtr.Zero : text_page.__Instance;
			result = string.Empty;
			int __ret = 0;
			var allocBuffer = __IntPtr.Zero;
			// 使わない場合も含めて確保しておく(最適化ミスなどで消されないように確実に確保)
			var buffer = stackalloc char[1024];
			__IntPtr ptr = new __IntPtr( buffer );
			// スタック領域以上の文字列を確保したい場合は、別途バッファを確保する
			if( count >= 1024 )
			{
				allocBuffer = Marshal.AllocCoTaskMem( count * sizeof( char ) );
				ptr = allocBuffer;
			}
			__ret = __Internal.FPDFTextGetText( __arg0, start_index, count, ptr );
			if( __ret > 0 )
			{
				unsafe
				{
					// 終端分を含んだ長さを返すので、-1する(終端分は外側では考慮しない)
					result = new string( (char*)ptr.ToPointer(), 0, Math.Min(count,__ret) );
				}
			}
			if( allocBuffer != __IntPtr.Zero )
			{
				Marshal.FreeCoTaskMem( allocBuffer );
			}
			return __ret > 0;
		}
	}
}
