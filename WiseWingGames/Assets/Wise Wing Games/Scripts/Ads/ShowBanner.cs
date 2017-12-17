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
			MobileNativeUI.ShowToast ("Banner", true);
			if (bannerPosition == BannerPosition.Top)
				AdManager.instance.ShowBannerAd ("top");
			else
				AdManager.instance.ShowBannerAd ("bottom");
		}

		void OnDisable ()
		{
			AdManager.instance.DestroyBanner ();
		}
	}
}
