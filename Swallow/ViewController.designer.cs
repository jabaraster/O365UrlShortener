// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Swallow
{
	[Register("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton apiKeySaver { get; set; }

		[Outlet]
		AppKit.NSSecureTextField googleApiKey { get; set; }

		[Outlet]
		AppKit.NSButton pasteboardMonitoring { get; set; }

		[Action ("OnApiKeySaverClicked:")]
		partial void OnApiKeySaverClicked (Foundation.NSObject sender);

		[Action ("OnMonitoringCheckChanged:")]
		partial void OnMonitoringCheckChanged (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (googleApiKey != null)
			{
				googleApiKey.Dispose();
				googleApiKey = null;
			}

			if (pasteboardMonitoring != null)
			{
				pasteboardMonitoring.Dispose();
				pasteboardMonitoring = null;
			}

			if (apiKeySaver != null) {
				apiKeySaver.Dispose ();
				apiKeySaver = null;
			}
		}
	}
}
