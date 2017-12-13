using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

namespace WiseWingGames
{

	public class AdsSettings : ScriptableObject
	{

		[Header ("Creds")]
		public string heyzapId = "777897ccb9e638a067849a03190b51c6";

		[Header("Ads tags to fetch")]
		[Space(10)]

		public string[] staticAdTags;
		public string[] videoAdTags;
		public string[] rewardAdTags;


		[Header("Banner")]
		public bool showBanner = false;
		public enum BannerPosition{Top, Bottom} 
		public BannerPosition bannerPosition;

		[Header("Settings")]
		public float minTimeBetweenAdRequestsInSeconds = 0f;

		[Header("Debug")]
		public bool showTestSuite = false;

	
	}
}
