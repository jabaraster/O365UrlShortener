using System;
using System.Diagnostics;
using Foundation;
using Security;
using Swallow.Model;

namespace Swallow.Model
{
	public static class SecKeyChainUtil
	{
		public static MayFail<object> RegisterGoogleApiKey(string googleApiKey)
		{
			var dic = NSBundle.MainBundle.InfoDictionary;
			var appName = dic["CFBundleName"];
			var service = dic["CFBundleIdentifier"] + "." + appName;
			var rec = new SecRecord(SecKind.GenericPassword)
			{
				Label = appName + " - Google Shortener APIのキー",
				Description = appName + "がURLの短縮に使うGoogle Shortener APIのキーを保存します.",
				Service = service,
				ValueData = NSData.FromString(googleApiKey),
			};
			var addRet = SecKeyChain.Add(rec);
			switch (addRet)
			{
				case SecStatusCode.DuplicateItem:
					{
						var query = rec;
						var updateRet = SecKeyChain.Update(query, rec);
						if (updateRet != SecStatusCode.Success)
						{
							Debug.WriteLine(updateRet);
							return MayFailFactory.NewFail<object>("キーチェーンへの保存に失敗しました.");
						}
						return MayFailFactory.NewSuccess<object>(null);
					}
				case SecStatusCode.Success:
					{
						return MayFailFactory.NewSuccess<object>(null);
					}
				default:
					{
						Debug.WriteLine(addRet);
							return MayFailFactory.NewFail<object>("キーチェーンへの保存に失敗しました.");
					}
			}
		}

		static string getKeyChainService()
		{
			var dic = NSBundle.MainBundle.InfoDictionary;
			return dic["CFBundleIdentifier"] + "." + dic["CFBundleName"];
		}

	}
}
