using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WiseWingGames
{

	public class ShowBanner : MonoBehaviour
	{
		public enum BannerPosition
		{
			Top,
			Bottom

		}

		public BannerPosition bannerPosition = BannerPosition.Top;

		void OnEnable ()
		{
			
			if (bannerPosition == BannerPosition.Top)
				AdManager.Instance.ShowBannerAd ("top");
			else
				AdManager.Instance.ShowBannerAd ("bottom");
		}

		void OnDisable ()
		{
			AdManager.Instance.DestroyBanner ();
		}
	}
}
