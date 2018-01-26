using UnityEngine;
using Heyzap;

namespace WiseWingGames
{

    public class AdManager : MonoBehaviour
    {
        public static AdManager Instance { get; private set; }

        AdsSettings adsSettings;
        bool canShowAd;
        float timer;
        bool adsRemoved;

        public delegate void RewardAdDelegate(string adTag);

        public static event RewardAdDelegate OnRewardAdCompleted;
        public static event RewardAdDelegate OnRewardAdSkipped;


        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            CheckIfAdsAreRemoved();
        }

        void OnEnable()
        {
            SetRewardAdListener();

            if (adsRemoved)
                return;
            SetInterstitialListener();
            SetVideoListener();
        }

        void Start()
        {
            adsSettings = WiseWingGamesSetup.instance.GetAdsSettings;

            InitHeyZap();

            AdRequestAvailable();

            FetchAds();

            //Show Heyzap test suite........
            if (adsSettings.showTestSuite)
                HeyzapAds.ShowMediationTestSuite();

        }

        void Update()
        {
            if (canShowAd)
                return;

            timer -= Time.unscaledDeltaTime;
            if (timer <= 0)
                AdRequestAvailable();
        }

        void StartTimer()
        {
            timer = adsSettings.minTimeBetweenAdRequestsInSeconds;
            canShowAd = false;
        }

        void AdRequestAvailable()
        {
            timer = adsSettings.minTimeBetweenAdRequestsInSeconds;
            canShowAd = true;
        }

        #region Static Ad

        public void ShowChartboostInterstitial()
        {
            if (canShowAd && !adsRemoved && HZInterstitialAd.ChartboostIsAvailableForLocation("Default"))
            {
                Toaster.ShowDebugToast("Showing Chartboost Interstitial for location; Default");
                HZInterstitialAd.ChartboostShowForLocation("Default");
            }
            else
            {
                Toaster.ShowDebugToast("Can't show Chartboost Interstitial because not available or Ads are removed");
            }
        }

        public void ShowInterstitialAd()
        {
            if (canShowAd && !adsRemoved && HZInterstitialAd.IsAvailable())
            {
                Toaster.ShowDebugToast("Showing Interstitial ad");
                HZInterstitialAd.Show();
            }
            else
            {
                Toaster.ShowDebugToast("Cant show Interstitial ad");
            }
        }

        public void ShowInterstitialAd(string tag)
        {
            if (canShowAd && !adsRemoved && HZInterstitialAd.IsAvailable(tag))
            {
                Toaster.ShowDebugToast("Showing Interstitial for tag; " + tag);

                HZShowOptions options = new HZShowOptions();
                options.Tag = tag;
                HZInterstitialAd.ShowWithOptions(options);
            }
            else
            {
                Toaster.ShowDebugToast("Can't show Interstitial for tag; " + tag);
            }
        }

        #endregion

        #region Video Ad

        public void ShowVideoAd()
        {
            if (canShowAd && !adsRemoved && HZVideoAd.IsAvailable())
            {
                Toaster.ShowDebugToast("Showing Video ad");
                HZVideoAd.Show();
            }
            else
            {
                Toaster.ShowDebugToast("Can't show Video ad");
            }
        }

        public void ShowVideoAd(string tag)
        {
            if (HZVideoAd.IsAvailable(tag) && canShowAd && !adsRemoved)
            {
                Toaster.ShowDebugToast("Showing Video ad for tag; " + tag);

                HZShowOptions options = new HZShowOptions();
                options.Tag = tag;
                HZVideoAd.ShowWithOptions(options);
            }
            else
            {
                Toaster.ShowDebugToast("Can't show Video ad for tag; " + tag);
            }
        }

        #endregion

        #region Banner Ad

        public void ShowBannerAd(string _position)
        {
            if (adsRemoved)
            {
                Toaster.ShowDebugToast("Not showing banner ad because ads are removed");
                return;
            }

            HZBannerShowOptions showOptions = new HZBannerShowOptions();
            showOptions.Position = _position;
            HZBannerAd.ShowWithOptions(showOptions);

            Toaster.ShowDebugToast("Showing banner ad");
        }

        public void HideBanner()
        {
            HZBannerAd.Hide();
            Toaster.ShowDebugToast("Hiding banner ad");
        }

        public void DestroyBanner()
        {
            HZBannerAd.Destroy();
            Toaster.ShowDebugToast("Destroying banner ad");
        }

        #endregion

        #region Reward Ad
        /// <summary>
        /// returns true if this instance is reward video available; otherwise, <c>false</c>.
        /// </summary>
        /// 
        public bool IsRewardVideoAvailable()
        {
            Toaster.ShowDebugToast("IsRewardVideoAvailable: " + HZIncentivizedAd.IsAvailable());
            return HZIncentivizedAd.IsAvailable();
        }

        public bool IsRewardVideoAvailable(string tag)
        {
            Toaster.ShowDebugToast("IsRewardVideoAvailable for tag; " + tag + " = " + HZIncentivizedAd.IsAvailable(tag));
            return HZIncentivizedAd.IsAvailable(tag);
        }

        public void ShowRewardAd()
        {
            if (HZIncentivizedAd.IsAvailable())
            {
                Toaster.ShowDebugToast("Showing reward ad.");

                HZIncentivizedAd.Show();
            }
            else
            {
                Toaster.ShowDebugToast("Can't show reward ad");
            }
        }

        public void ShowRewardAd(string tag)
        {
            if (HZIncentivizedAd.IsAvailable(tag))
            {
                Toaster.ShowDebugToast("Showing reward video for tag; " + tag);

                HZIncentivizedShowOptions options = new HZIncentivizedShowOptions();
                options.Tag = tag;
                HZIncentivizedAd.ShowWithOptions(options);
            }
            else
            {
                Toaster.ShowDebugToast("Can't show reward video for tag; " + tag);
            }
        }


        void SetRewardAdListener()
        {
            HZIncentivizedAd.AdDisplayListener listener = delegate (string adState, string adTag)
            {
                if (adState.Equals("show"))
                {
                    //Ad showing, pause game

                }
                if (adState.Equals("hide"))
                {
                    //Ad gone, unpause game
                }
                if (adState.Equals("click"))
                {

                }
                if (adState.Equals("failed"))
                {

                }
                if (adState.Equals("available"))
                {

                }
                if (adState.Equals("fetch_failed"))
                {

                }
                if (adState.Equals("audio_starting"))
                {
                    //mute game sound
                }
                if (adState.Equals("audio_finished"))
                {
                    //unmute game sound

                }
                if (adState.Equals("incentivized_result_complete"))
                {
                    //reward player here
                    OnRewardAdCompleted(adTag);
                    Toaster.ShowDebugToast("Reward ad Conpleted for tag; " + adTag);
                }
                if (adState.Equals("incentivized_result_incomplete"))
                {
                    OnRewardAdSkipped(adTag);
                    Toaster.ShowDebugToast("Reward ad 'Skipped' for tag; " + adTag);
                }
            };

            HZIncentivizedAd.SetDisplayListener(listener);
        }

        #endregion

        #region Remove Ads
        public bool CheckIfAdsAreRemoved()
        {

            if (PlayerPrefs.GetInt("AdsRemoved", 0) == 1)
                adsRemoved = true;
            else
                adsRemoved = false;

            return adsRemoved;
        }

        public void RemoveAds()
        {
            PlayerPrefs.SetInt("AdsRemoved", 1);
            adsRemoved = true;
            DestroyBanner();
        }
        #endregion

        //Init Heyzap with specific appstore options.......
        void InitHeyZap()
        {
            Toaster.ShowDebugToast("Initializing Heyzap...");

            string heyzapId = adsSettings.heyzapId;

#if (UNITY_ANDROID && WWG_GOOGLE )|| UNITY_IOS
            HeyzapAds.Start(heyzapId, HeyzapAds.FLAG_NO_OPTIONS);
#elif WWG_AMAZON && UNITY_ANDROID
				HeyzapAds.Start (heyzapId, HeyzapAds.FLAG_AMAZON);
#endif
        }

        void FetchAds()
        {
            foreach (string tag in adsSettings.rewardAdTags)
            {
                HZIncentivizedAd.Fetch(tag);
                Toaster.ShowDebugToast("Fetching 'reward' ad for tag; " + tag);
            }

            //Dont fetch interstitials if NoAds is baught
            if (adsRemoved)
            {
                Toaster.ShowDebugToast("Not fetching other ads because 'Remove_ads' is bought.");
                return;
            }

            HZInterstitialAd.ChartboostFetchForLocation("Default");

            foreach (string tag in adsSettings.staticAdTags)
            {
                HZInterstitialAd.Fetch(tag);
                Toaster.ShowDebugToast("Fetching 'static' ad for tag; " + tag);
            }
            foreach (string tag in adsSettings.videoAdTags)
            {
                HZVideoAd.Fetch(tag);
                Toaster.ShowDebugToast("Fetching 'video' ad for tag; " + tag);
            }

        }

        void SetInterstitialListener()
        {
            HZInterstitialAd.AdDisplayListener listener = delegate (string adState, string adTag)
            {

                //	Debug.Log ("Static Ad ::: adState: " + adState + " , adTag: " + adTag);

                if (adState.Equals("show"))
                {
                    //Ad showing, pause game
                    StartTimer();
                }
                if (adState.Equals("hide"))
                {
                    //Ad gone, unpause game
                }
                if (adState.Equals("click"))
                {

                }
                if (adState.Equals("failed"))
                {

                }
                if (adState.Equals("available"))
                {

                }
                if (adState.Equals("fetch_failed"))
                {

                }
                if (adState.Equals("audio_starting"))
                {
                    //mute game sound
                }
                if (adState.Equals("audio_finished"))
                {
                    //unmute game sound

                }

            };

            HZInterstitialAd.SetDisplayListener(listener);
        }

        void SetVideoListener()
        {
            HZVideoAd.AdDisplayListener listener = delegate (string adState, string adTag)
            {
                //				Debug.Log ("Video Ad ::: adState: " + adState + " , adTag: " + adTag);

                if (adState.Equals("show"))
                {
                    //Ad showing, pause game
                    StartTimer();
                }
                if (adState.Equals("hide"))
                {
                    //Ad gone, unpause game
                }
                if (adState.Equals("click"))
                {

                }
                if (adState.Equals("failed"))
                {

                }
                if (adState.Equals("available"))
                {

                }
                if (adState.Equals("fetch_failed"))
                {

                }
                if (adState.Equals("audio_starting"))
                {
                    //mute game sound
                }
                if (adState.Equals("audio_finished"))
                {
                    //unmute game sound

                }

            };

            HZVideoAd.SetDisplayListener(listener);
        }

    }
}
