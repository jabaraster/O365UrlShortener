using System;

using AppKit;
using Foundation;
using O365UrlShortener.Model;

namespace O365UrlShortener
{
	public partial class ViewController : NSViewController
	{
		PasteboardStringMonitor monitor;
		
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			this.monitor = new PasteboardStringMonitor(NSPasteboard.GeneralPasteboard, this.onPasteboardStringChanged);
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		void onPasteboardStringChanged(string str)
		{
		}
	}
}
