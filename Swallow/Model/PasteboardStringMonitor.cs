using System;
using AppKit;
using Foundation;

namespace O365UrlShortener.Model
{
	public class PasteboardStringMonitor
	{
		readonly NSPasteboard pasteboard;
		readonly Action<string> handler;

		NSTimer timer;
		string preString = "";
		nint preChangeCount = -1;
		
		public PasteboardStringMonitor(NSPasteboard pPasteboard, Action<string> pHandler)
		{
			this.pasteboard = pPasteboard;
			this.handler = pHandler;
			this.preChangeCount = this.pasteboard.ChangeCount;
			this.preString = this.pasteboard.GetStringForType(NSPasteboard.NSPasteboardTypeString);

			this.timer = createTimer();
		}

		public void Pause()
		{
			this.timer.Invalidate();
			this.timer = null;
		}

		public void Restart()
		{
			this.timer = createTimer();
		}

		void fire(NSTimer pTimer)
		{
			var nowChangeCount = this.pasteboard.ChangeCount;
			if (nowChangeCount == this.preChangeCount) return;

			this.preChangeCount = nowChangeCount;

			var nowString = this.pasteboard.GetStringForType(NSPasteboard.NSPasteboardTypeString);
			if (nowString == this.preString) return;

			this.preString = nowString;
			this.handler(nowString);
		}

		NSTimer createTimer()
		{
			return NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(1), this.fire);
		}
	}
}
