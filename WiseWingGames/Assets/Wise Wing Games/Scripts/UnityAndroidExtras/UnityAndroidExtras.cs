using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

namespace WiseWingGames
{
	
	public class UnityAndroidExtras : MonoBehaviour,IWebViewListener,IAlertViewListener
	{

		/** Instance */
		public static UnityAndroidExtras instance;

		// Events for Webview listeners
		public delegate void OnWebViewStartLoading ();

		public static event OnWebViewStartLoading onWebViewStartLoading;

		public delegate void OnWebViewFinishLoading ();

		public static event OnWebViewFinishLoading onWebViewFinishLoading;
		// Events for alertview listeners
		public delegate void OnAlertViewButtonClicked ();

		public static event OnAlertViewButtonClicked onAlertViewButtonClicked;

		public delegate void OnAlertViewNegButtonClicked ();

		public static event OnAlertViewNegButtonClicked onAlertViewNegativeButtonClicked;

	
		GeneralSettings settings;

		void Awake ()
		{

			if (instance != null)
				Destroy (this);
			else
				instance = this;
		}

		void Start ()
		{
			settings = WiseWingGamesSetup.instance.GetGeneralSettings;

		}


		public void ShareScreenshot ()
		{
			switch (settings.appStore) {
			case GeneralSettings.AppStore.GooglePlay:
				ShareScreenshotWithText ("https://play.google.com/store/apps/details?id=" + settings.googlePlayStoreBundleID);
				break;

			case GeneralSettings.AppStore.Amazon:
				ShareScreenshotWithText ("http://www.amazon.com/gp/mas/dl/android?p=" + settings.googlePlayStoreBundleID);
				break;
			}
		}

		public void ShareScreenshotWithText (string text)
		{
			string screenShotPath = Application.persistentDataPath + "/screenshot.png";
			if (File.Exists (screenShotPath))
				File.Delete (screenShotPath);

			Application.CaptureScreenshot ("screenshot.png");

			StartCoroutine (delayedShare (screenShotPath, text));
		}

		//CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
		//for it to save, or set a unique image name and check if the file has been created yet before sharing.
		IEnumerator delayedShare (string screenShotPath, string text)
		{
			while (!File.Exists (screenShotPath)) {
				yield return new WaitForSeconds (.05f);
			}

			NativeShare.Share (text, screenShotPath, "", "", "image/png", true, "");
		}


		public void rateMyGame ()
		{
			switch (settings.appStore) {

			case GeneralSettings.AppStore.GooglePlay:
				OpenAppPage (settings.googlePlayStoreBundleID);
				break;
			
			case GeneralSettings.AppStore.Amazon:
				OpenAppPage (settings.amazonAppStoreBundleID);
				break;
			
			}
		}

		public void OpenAppPage (string bundleId)
		{
			switch (settings.appStore) {

			case GeneralSettings.AppStore.GooglePlay:
				Application.OpenURL ("https://play.google.com/store/apps/details?id=" + bundleId);
				break;

			case GeneralSettings.AppStore.Amazon:
				Application.OpenURL ("amzn://apps/android?p=" + bundleId);
				break;
			}
		}

		public void OpenDeveloperPage ()
		{
			switch (settings.appStore) {

			case GeneralSettings.AppStore.GooglePlay:
				Application.OpenURL (settings.googlePlayDeveloperPageLink);
				break;

			case GeneralSettings.AppStore.Amazon:
				Application.OpenURL (settings.amazonDeveloperPageLink);
				break;
			}
		}

		public void OpenFacebookPage ()
		{
			shareOnFacebook ("https://www.facebook.com/WiseWings2017");
		}

		public void ShareGameLinkOnTwitter ()
		{
			shareOnTwitter ("I'm playing! https://play.google.com/store/apps/details?id=" + settings.googlePlayStoreBundleID,
				"https://twitter.com");
		}

		public void OpenShareGameLink ()
		{
			if (settings.appStore == GeneralSettings.AppStore.GooglePlay)
				openShareIntent ("https://play.google.com/store/apps/details?id=" + settings.googlePlayStoreBundleID);
			else if (settings.appStore == GeneralSettings.AppStore.Amazon)
				openShareIntent ("http://www.amazon.com/gp/mas/dl/android?p=" + settings.googlePlayStoreBundleID);
		}

		/// <summary>
		/// Shares a link on facebook.
		/// </summary>
		/// <param name="fbLink">Fb link.</param>
		public void shareOnFacebook (string fbLink)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("shareOnFacebook", fbLink);
			#endif
		}

		/// <summary>
		/// Shares a text on twitter.If application is not installed opens browser with a twitter link.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="fallBackUrl">Fall back URL.</param>
		public void shareOnTwitter (string message, string fallBackUrl)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("shareOnTwitter", message, fallBackUrl);
			#endif
		}

		/// <summary>
		/// Makes the toast.
		/// </summary>
		/// <param name="toast">Toast.</param>
		/// <param name="length"(must be either 0 or 1!)>Length.</param>
		public void makeToast (string toast, int length)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("makeToast", toast, length);
			#endif
		}

		/// <summary>
		/// Alert the specified message with neutralButton.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="neutralButtonText">Neutral button text.</param>
		public void alert (string message, string neutralButtonText)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("alert", message, neutralButtonText, gameObject.name);
			#endif
		}

		/// <summary>
		/// Alert the specified message,with neutralButton and negativeButton.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="neutralButtonText">Neutral button text.</param>
		/// <param name="negativeButtonText">Negative button text.</param>
		public void alert (string message, string neutralButtonText, string negativeButtonText)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("alert", message, neutralButtonText, negativeButtonText, gameObject.name);
			#endif
		}

		/// <summary>
		/// Opens the android default share intent.
		/// </summary>
		/// <param name="message">Message.</param>
		public void openShareIntent (string message)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openShareIntent", message);
			#endif
		}

		/// <summary>
		/// Sets the immersive mode for supporting devices(devices that have virtual button)
		/// </summary>
		public void setImmersiveMode ()
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("setImmersiveMode");
			#endif
		}

		/// <summary>
		/// Opens fullscreen web view.
		/// </summary>
		/// <param name="url">URL.</param>
		public void openWebView (string url)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openWebView", url, gameObject.name);
			#endif
		}

		/// <summary>
		/// Opens the web view with margins
		/// </summary>
		/// <param name="url">URL.</param>
		/// <param name="marginLeft">Margin left.</param>
		/// <param name="marginTop">Margin top.</param>
		/// <param name="marginRight">Margin right.</param>
		/// <param name="marginBottom">Margin bottom.</param>
		public void openWebView (string url, int marginLeft, int marginTop, int marginRight, int marginBottom)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openWebView", url, gameObject.name, marginLeft, marginTop, marginRight, marginBottom);
			#endif
		}

		/// <summary>
		/// Closes the web view.
		/// </summary>
		public void closeWebView ()
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("closeWebView");
			#endif
		}

		/// <summary>
		/// Checks if a specific application istalled.(usefull for giving gold credit etc...)
		/// </summary>
		/// <returns><c>true</c>, if application istalled was ised, <c>false</c> otherwise.</returns>
		/// <param name="bundleName">Bundle name.</param>
		public bool isApplicationIstalled (string bundleName)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				return jo.Call<bool> ("isApplicationInstalled", bundleName);
			#endif
		}

		/// <summary>
		/// Opens another application if it's installed
		/// </summary>
		/// <param name="bundleName">Bundle name.</param>
		public void openApplication (string bundleName)
		{
			#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openApplication", bundleName);
			#endif
		}

		#region IWebViewListener implementation

		public void onPageStarted (string s)
		{
			if (onWebViewStartLoading != null)
				onWebViewStartLoading ();
		}

		public void onPageFinished (string s)
		{
			if (onWebViewFinishLoading != null)
				onWebViewFinishLoading ();
		}

		#endregion

		#region IAlertViewListener implementation

		public void onAlertButtonClicked (string s)
		{
			if (onAlertViewButtonClicked != null)
				onAlertViewButtonClicked ();
		}

		public void onAlertNegativeButtonClicked (string s)
		{
			if (onAlertViewNegativeButtonClicked != null)
				onAlertViewNegativeButtonClicked ();
		}

		#endregion
	}
}