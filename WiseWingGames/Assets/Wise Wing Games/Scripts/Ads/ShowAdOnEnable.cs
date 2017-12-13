using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WiseWingGames
{
	public class ShowAdOnEnable : MonoBehaviour
	{
		public enum AdType
		{
			Static,
			Video

		}

		public AdType _adType;

		public string adTag;

		void OnEnable ()
		{
			if (_adType == AdType.Static)
				AdManager.instance.ShowInterstitialAd (adTag);
			else
				AdManager.instance.ShowVideoAd (adTag);
		}


	}
}