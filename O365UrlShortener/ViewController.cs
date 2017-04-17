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

		partial void OnTestClicked(NSObject sender)
		{
			string s = this.googleApiKey.StringValue;
			HttpAccessor.Get<string>("https://www.googleapis.com/urlshortener/v1/url?key=" + s);
		}
	}
}
