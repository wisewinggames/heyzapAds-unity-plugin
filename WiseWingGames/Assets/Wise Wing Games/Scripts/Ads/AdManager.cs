using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

namespace WiseWingGames
{
	
	public class AdManager : MonoBehaviour
	{
		public static AdManager instance { get; private set; }

		AdsSettings adsSettings;
		bool canShowAd;
		float timer;
		bool adsRemoved;

		public delegate void RewardAdDelegate (string adTag);

		public static event RewardAdDelegate OnRewardAdCompleted;


		void Awake ()
		{
			if (instance != null) {
				Destroy (this);
			} else {
				instance = this;
			}

			CheckIfAdsAreRemoved ();
		}

		void OnEnable ()
		{
			SetRewardAdListener ();

			if (adsRemoved)
				return;
			SetInterstitialListener ();
			SetVideoListener ();
		}

		void Start ()
		{
			adsSettings = WiseWingGamesSetup.instance.GetAdsSettings;

			InitHeyZap ();

			AdRequestAvailable ();

			FetchAds ();

			//Show Heyzap test suite........
			if (adsSettings.showTestSuite)
				HeyzapAds.ShowMediationTestSuite ();

		}

		void Update ()
		{
			if (canShowAd)
				return;

			timer -= Time.unscaledDeltaTime;
			if (timer <= 0)
				AdRequestAvailable ();
		}


		#region Remove Ads
		public bool CheckIfAdsAreRemoved ()
		{

			if (PlayerPrefs.GetInt ("AdsRemoved", 0) == 1)
				adsRemoved = true;
			else
				adsRemoved = false;

			return adsRemoved;
		}

		public void RemoveAds ()
		{
			PlayerPrefs.SetInt ("AdsRemoved", 1);
			adsRemoved = true;
			DestroyBanner ();
		}
		#endregion


		void StartTimer ()
		{
			timer = adsSettings.minTimeBetweenAdRequestsInSeconds;
			canShowAd = false;
		}

		void AdRequestAvailable ()
		{
			timer = adsSettings.minTimeBetweenAdRequestsInSeconds;
			canShowAd = true;
		}


		//Init Heyzap with specific appstore options.......
		void InitHeyZap ()
		{
			string heyzapId = adsSettings.heyzapId;

			switch (WiseWingGamesSetup.instance.GetGeneralSettings.appStore) {

			case GeneralSettings.AppStore.GooglePlay:
				HeyzapAds.Start (heyzapId, HeyzapAds.FLAG_NO_OPTIONS);
				break;

			case GeneralSettings.AppStore.Amazon:
				HeyzapAds.Start (heyzapId, HeyzapAds.FLAG_AMAZON);
				break;
			}
		}

		void FetchAds ()
		{
			foreach (string tag in adsSettings.rewardAdTags) {
				HZIncentivizedAd.Fetch (tag);
			}
			HZIncentivizedAd.Fetch ();

			//Dont fetch interstitials if NoAds is baught
			if (adsRemoved)
				return;
			
			HZInterstitialAd.ChartboostFetchForLocation ("Default");

			HZVideoAd.Fetch ();

			foreach (string tag in adsSettings.staticAdTags) {
				HZInterstitialAd.Fetch (tag);
			}
			foreach (string tag in adsSettings.videoAdTags) {
				HZVideoAd.Fetch (tag);
			}

		}

		#region Static Ad

		public void ShowChartboostInterstitial(){
			if (canShowAd && !adsRemoved && HZInterstitialAd.ChartboostIsAvailableForLocation("Default")) {
				HZInterstitialAd.ChartboostShowForLocation ("Default");
			}
		}

		public void ShowInterstitialAd ()
		{
			if (canShowAd && !adsRemoved)
				HZInterstitialAd.Show ();
		}

		public void ShowInterstitialAd (string tag)
		{
			if (canShowAd && !adsRemoved) {
				HZShowOptions options = new HZShowOptions ();
				options.Tag = tag;
				HZInterstitialAd.ShowWithOptions (options);
			}
		}

		#endregion



		#region Video Ad

		public void ShowVideoAd ()
		{
			if (canShowAd && !adsRemoved)
				HZVideoAd.Show ();
		}

		public void ShowVideoAd (string tag)
		{
			if (HZVideoAd.IsAvailable () && canShowAd && !adsRemoved) {
				HZShowOptions options = new HZShowOptions ();
				options.Tag = tag;
				HZVideoAd.ShowWithOptions (options);
			}
		}

		#endregion





		#region Banner Ad

		public void ShowBannerAd (string _position)
		{
			if (adsRemoved)
				return;
			HZBannerShowOptions showOptions = new HZBannerShowOptions ();
			showOptions.Position = _position;
			HZBannerAd.ShowWithOptions (showOptions);
		}

		public void HideBanner ()
		{
			HZBannerAd.Hide ();
		}

		public void DestroyBanner ()
		{
			HZBannerAd.Destroy ();
		}

		#endregion





		#region Reward Ad

		public void ShowRewardAd ()
		{
			if (HZIncentivizedAd.IsAvailable ())
				HZIncentivizedAd.Show ();
		}

		public void ShowRewardAd (string tag)
		{
			if (HZIncentivizedAd.IsAvailable ()) {
				HZIncentivizedShowOptions options = new HZIncentivizedShowOptions ();
				options.Tag = tag;
				HZIncentivizedAd.ShowWithOptions (options);
			}
		}


		void SetRewardAdListener ()
		{
			HZIncentivizedAd.AdDisplayListener listener = delegate(string adState, string adTag) {
				if (adState.Equals ("show")) {
					//Ad showing, pause game

				}
				if (adState.Equals ("hide")) {
					//Ad gone, unpause game
				}
				if (adState.Equals ("click")) {

				}
				if (adState.Equals ("failed")) {

				}
				if (adState.Equals ("available")) {

				}
				if (adState.Equals ("fetch_failed")) {

				}
				if (adState.Equals ("audio_starting")) {
					//mute game sound
				}
				if (adState.Equals ("audio_finished")) {
					//unmute game sound

				}
				if (adState.Equals ("incentivized_result_complete")) {
					//reward player here
					OnRewardAdCompleted (adTag);
				}
				if (adState.Equals ("incentivized_result_incomplete")) {

				}
			};

			HZIncentivizedAd.SetDisplayListener (listener);
		}

		#endregion


		void SetInterstitialListener ()
		{
			HZInterstitialAd.AdDisplayListener listener = delegate(string adState, string adTag) {

			//	Debug.Log ("Static Ad ::: adState: " + adState + " , adTag: " + adTag);

				if (adState.Equals ("show")) {
					//Ad showing, pause game
					StartTimer ();
				}
				if (adState.Equals ("hide")) {
					//Ad gone, unpause game
				}
				if (adState.Equals ("click")) {

				}
				if (adState.Equals ("failed")) {

				}
				if (adState.Equals ("available")) {

				}
				if (adState.Equals ("fetch_failed")) {

				}
				if (adState.Equals ("audio_starting")) {
					//mute game sound
				}
				if (adState.Equals ("audio_finished")) {
					//unmute game sound

				}

			};

			HZInterstitialAd.SetDisplayListener (listener);
		}

		void SetVideoListener ()
		{
			HZVideoAd.AdDisplayListener listener = delegate(string adState, string adTag) {
//				Debug.Log ("Video Ad ::: adState: " + adState + " , adTag: " + adTag);

				if (adState.Equals ("show")) {
					//Ad showing, pause game
					StartTimer ();
				}
				if (adState.Equals ("hide")) {
					//Ad gone, unpause game
				}
				if (adState.Equals ("click")) {

				}
				if (adState.Equals ("failed")) {

				}
				if (adState.Equals ("available")) {

				}
				if (adState.Equals ("fetch_failed")) {

				}
				if (adState.Equals ("audio_starting")) {
					//mute game sound
				}
				if (adState.Equals ("audio_finished")) {
					//unmute game sound

				}

			};

			HZVideoAd.SetDisplayListener (listener);
		}

	}
}
