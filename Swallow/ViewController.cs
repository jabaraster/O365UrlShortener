using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AppKit;
using Foundation;
using Security;
using Swallow.Model;

namespace Swallow
{
	public partial class ViewController : NSViewController
	{
		PasteboardStringMonitor monitor;
		bool webApiProcessing = false;

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

		partial void OnMonitoringCheckChanged(NSObject sender)
		{
			if (this.pasteboardMonitoring.State == NSCellStateValue.On)
			{
				this.monitor.Restart();
			}
			else
			{
				this.monitor.Pause();
			}
		}

		partial void OnApiKeySaverClicked(NSObject sender)
		{
			var res = SecKeyChainUtil.RegisterGoogleApiKey(this.googleApiKey.StringValue);
			if (res.IsError)
			{
				notifyToUser("保存に失敗", res.ErrorMessage);
			}
		}

		void onPasteboardStringChanged(string str)
		{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			shorten(str); // awaitが付けられない
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
		}

		async Task shorten(string pasteboardString)
		{
			if (!pasteboardString.StartsWith("https://", StringComparison.Ordinal)) return;
			if (pasteboardString.StartsWith("https://goo.gl/", StringComparison.Ordinal)) return;
			if (this.webApiProcessing) return;
			try
			{
				this.webApiProcessing = true;
				var res = await UrlShortener.Shorten(pasteboardString, this.googleApiKey.StringValue);

				var b = PasteboardUtil.SetString(res.id);
				if (!b)
				{
					notifyToUser("短縮URLをペーストボードに送れませんでした.", "");
					return;
				}
				notifyToUser("ペーストボードに短縮URLを送りました.", res.longUrl + " >>> " + res.id);
			}
			catch (Exception ex)
			{
				notifyToUser("URLの短縮に失敗しました.", ex.Message);
				System.Diagnostics.Debug.WriteLine(ex);
			}
			finally
			{
				this.webApiProcessing = false;
			}
		}
		void notifyToUser(string title, string informationText)
		{
			NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(new NSUserNotification()
			{
				Title = title,
				InformativeText = informationText,
			});
		}
	}
}
