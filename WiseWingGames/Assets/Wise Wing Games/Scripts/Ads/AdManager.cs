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

		public delegate void RewardAdDelegate (string adTag);

		public static event RewardAdDelegate OnRewardAdCompleted;


		void Awake ()
		{
			if (instance != null) {
				Destroy (this);
			} else {
				instance = this;
			}
		
		}

		void OnEnable ()
		{
			SetRewardAdListner ();
		}

		void Start ()
		{

			adsSettings = WiseWingGamesSetup.instance.GetAdsSettings;

			AdRequestAvailable ();

			InitHeyZap ();

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

			HZVideoAd.Fetch ();

			HZIncentivizedAd.Fetch ();

			foreach (string tag in adsSettings.staticAdTags) {
				HZInterstitialAd.Fetch (tag);
			}
			foreach (string tag in adsSettings.videoAdTags) {
				HZVideoAd.Fetch (tag);
			}
			foreach (string tag in adsSettings.rewardAdTags) {
				HZIncentivizedAd.Fetch (tag);
			}
		}

		#region Static Ad

		public void ShowInterstitialAd ()
		{
			if (canShowAd)
				HZInterstitialAd.Show ();
		}

		public void ShowInterstitialAd (string tag)
		{
			if (canShowAd) {
				HZShowOptions options = new HZShowOptions ();
				options.Tag = tag;
				HZInterstitialAd.ShowWithOptions (options);
			}
		}

		#endregion



		#region Video Ad

		public void ShowVideoAd ()
		{
			HZVideoAd.Show ();
		}

		public void ShowVideoAd (string tag)
		{
			if (HZVideoAd.IsAvailable () && canShowAd) {
				HZShowOptions options = new HZShowOptions ();
				options.Tag = tag;
				HZVideoAd.ShowWithOptions (options);
			}
		}

		#endregion





		#region Banner Ad

		public void ShowBannerAd ()
		{
			HZBannerShowOptions showOptions = new HZBannerShowOptions ();
			showOptions.Position = HZBannerShowOptions.POSITION_TOP;
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


		void SetRewardAdListner ()
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


		void SetInterstitialListner ()
		{
			HZInterstitialAd.AdDisplayListener listener = delegate(string adState, string adTag) {

				Debug.Log ("Static Ad ::: adState: " + adState + " , adTag: " + adTag);

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

		void SetVideoListner ()
		{
			HZVideoAd.AdDisplayListener listener = delegate(string adState, string adTag) {
				Debug.Log ("Video Ad ::: adState: " + adState + " , adTag: " + adTag);

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
