using System;
using AppKit;

namespace O365UrlShortener.Model
{
	public static class PasteboardUtil
	{
		static readonly NSPasteboard _pasteboard;
		static PasteboardUtil()
		{
			_pasteboard = NSPasteboard.GeneralPasteboard;
			_pasteboard.DeclareTypes(new string[] { NSPasteboard.NSPasteboardTypeString }, null);
		}

		public static bool SetString(string s)
		{
			return _pasteboard.SetStringForType(s, NSPasteboard.NSPasteboardTypeString);
		}
	}
}
