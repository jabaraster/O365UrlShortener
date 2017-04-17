using System;
using AppKit;

namespace O365UrlShortener.Model
{
	public static class PasteboardUtil
	{
		public static bool SetString(string s)
		{
			var pasteboard = NSPasteboard.GeneralPasteboard;
			pasteboard.DeclareTypes(new string[] { NSPasteboard.NSPasteboardTypeString }, null);
			return pasteboard.SetStringForType(s, NSPasteboard.NSPasteboardTypeString);
		}
	}
}
