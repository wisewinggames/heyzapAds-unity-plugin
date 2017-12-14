using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Heyzap;

namespace WiseWingGames
{

	public class AdsSettings : ScriptableObject
	{

		[Header ("Creds")]
		public string heyzapId;

		[Header ("Ads tags to fetch")]
		[Space (10)]

		public string[] staticAdTags;
		public string[] videoAdTags;
		public string[] rewardAdTags;

		[Header ("Settings")]
		public float minTimeBetweenAdRequestsInSeconds = 0f;

		[Header ("Debug")]
		public bool showTestSuite = false;

	
	}
}
