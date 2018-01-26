using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

namespace WiseWingGames
{

    public class UnityAndroidExtras : MonoBehaviour, IWebViewListener, IAlertViewListener
    {

        /** Instance */
        public static UnityAndroidExtras instance;

        // Events for Webview listeners
        public delegate void OnWebViewStartLoading();

        public static event OnWebViewStartLoading onWebViewStartLoading;

        public delegate void OnWebViewFinishLoading();

        public static event OnWebViewFinishLoading onWebViewFinishLoading;
        // Events for alertview listeners
        public delegate void OnAlertViewButtonClicked();

        public static event OnAlertViewButtonClicked onAlertViewButtonClicked;

        public delegate void OnAlertViewNegButtonClicked();

        public static event OnAlertViewNegButtonClicked onAlertViewNegativeButtonClicked;

        string PLAY_STORE_APP_LINK_PREFIX = "https://play.google.com/store/apps/details?id=";
        string AMAZON_APPSTORE_APP_LINK_PREFIX = "http://www.amazon.com/gp/mas/dl/android?p=";   //"amzn://apps/android?p=";
        string FACEBOOK_PAGE_LINK = "https://www.facebook.com/WiseWings2017";

        public string PlayStoreAppLink { get { return PLAY_STORE_APP_LINK_PREFIX + Application.identifier; } }
        public string AmazonAppStoreAppLink { get { return AMAZON_APPSTORE_APP_LINK_PREFIX + Application.identifier; } }

        GeneralSettings settings;

        void Awake()
        {

            if (instance != null)
                Destroy(this);
            else
                instance = this;
        }

        void Start()
        {
            settings = WiseWingGamesSetup.instance.GetGeneralSettings;
        }


        public void RateMyGame()
        {
#if WWG_GOOGLE 
            Application.OpenURL(PlayStoreAppLink);
#elif WWG_AMAZON 
            Application.OpenURL(AmazonAppStoreAppLink);
#endif
        }


        public void OpenDeveloperPage()
        {
#if WWG_GOOGLE 
            Application.OpenURL(settings.googlePlayDeveloperPageLink);
#elif WWG_AMAZON
            Application.OpenURL(settings.amazonDeveloperPageLink);
#endif
        }

        public void OpenFacebookPage()
        {
            shareOnFacebook(FACEBOOK_PAGE_LINK);
        }

        public void ShareScreenshot()
        {
#if WWG_GOOGLE
            ShareScreenshotWithText(settings.ShareAppLinkText +" "+ PlayStoreAppLink);
#elif WWG_AMAZON
            ShareScreenshotWithText(settings.ShareAppLinkText + " " + AmazonAppStoreAppLink);
#endif
        }

        public void ShareGameLinkOnTwitter()
        {
#if WWG_GOOGLE
            shareOnTwitter(settings.ShareAppLinkText + " " + PlayStoreAppLink, "https://twitter.com");
#elif WWG_AMAZON
            shareOnTwitter(settings.ShareAppLinkText + " " + AmazonAppStoreAppLink, "https://twitter.com");
#endif
        }

        public void OpenShareGameLink()
        {
#if WWG_GOOGLE
            openShareIntent(settings.ShareAppLinkText + " " + PlayStoreAppLink);
#elif WWG_AMAZON
            openShareIntent(settings.ShareAppLinkText + " " + AmazonAppStoreAppLink);
#endif
        }


        public void MakeToast(string toast, int length)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("makeToast", toast, length);
#endif
        }

        public void Alert(string message, string neutralButtonText)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("alert", message, neutralButtonText, gameObject.name);
#endif
        }

        public void Alert(string message, string neutralButtonText, string negativeButtonText)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("alert", message, neutralButtonText, negativeButtonText, gameObject.name);
#endif
        }










        void ShareScreenshotWithText(string text)
        {
            string screenShotPath = Application.persistentDataPath + "/screenshot.png";
            if (File.Exists(screenShotPath))
                File.Delete(screenShotPath);

            Application.CaptureScreenshot("screenshot.png");

            StartCoroutine(delayedShare(screenShotPath, text));
        }

        //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
        //for it to save, or set a unique image name and check if the file has been created yet before sharing.
        IEnumerator delayedShare(string screenShotPath, string text)
        {
            while (!File.Exists(screenShotPath))
            {
                yield return new WaitForSeconds(.05f);
            }

            NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
        }

        /// <summary>
        /// Shares a link on facebook.
        /// </summary>
        /// <param name="fbLink">Fb link.</param>
        void shareOnFacebook(string fbLink)
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
        void shareOnTwitter(string message, string fallBackUrl)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("shareOnTwitter", message, fallBackUrl);
#endif
        }

        /// <summary>
        /// Opens the android default share intent.
        /// </summary>
        /// <param name="message">Message.</param>
        void openShareIntent(string message)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openShareIntent", message);
#endif
        }

        /// <summary>
        /// Sets the immersive mode for supporting devices(devices that have virtual button)
        /// </summary>
        void setImmersiveMode()
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
        void openWebView(string url)
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
        void openWebView(string url, int marginLeft, int marginTop, int marginRight, int marginBottom)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openWebView", url, gameObject.name, marginLeft, marginTop, marginRight, marginBottom);
#endif
        }

        /// <summary>
        /// Closes the web view.
        /// </summary>
        void closeWebView()
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
#if !DEBUGMODE && UNITY_ANDROID
		public bool isApplicationIstalled (string bundleName)
		{
			
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				return jo.Call<bool> ("isApplicationInstalled", bundleName);
    }
#endif

        /// <summary>
        /// Opens another application if it's installed
        /// </summary>
        /// <param name="bundleName">Bundle name.</param>
        void openApplication(string bundleName)
        {
#if !DEBUGMODE && UNITY_ANDROID
			using (AndroidJavaObject jo = new AndroidJavaObject ("com.nevzatarman.unityextras.UnityExtras"))
				jo.Call ("openApplication", bundleName);
#endif
        }

#region IWebViewListener implementation

        public void onPageStarted(string s)
        {
            if (onWebViewStartLoading != null)
                onWebViewStartLoading();
        }

        public void onPageFinished(string s)
        {
            if (onWebViewFinishLoading != null)
                onWebViewFinishLoading();
        }

#endregion

#region IAlertViewListener implementation

        public void onAlertButtonClicked(string s)
        {
            if (onAlertViewButtonClicked != null)
                onAlertViewButtonClicked();
        }

        public void onAlertNegativeButtonClicked(string s)
        {
            if (onAlertViewNegativeButtonClicked != null)
                onAlertViewNegativeButtonClicked();
        }

#endregion
    }
}