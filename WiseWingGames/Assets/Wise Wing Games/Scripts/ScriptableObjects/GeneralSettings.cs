using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#if UNITY_PURCHASING 
using UnityEditor.Purchasing;
#endif

#endif

namespace WiseWingGames
{
	
	public class GeneralSettings : ScriptableObject
	{
		
		public enum AppStore
		{
			GooglePlay,
			Amazon

		}

		[Header ("App Store")]
		public AppStore appStore;

		[Header ("Bundle Identifiers")]
		public string googlePlayStoreBundleID;
		public string amazonAppStoreBundleID;


		public string googlePlayDeveloperPageLink = "https://play.google.com/store/apps/dev?id=8294473886068372842";
		public string amazonDeveloperPageLink = "amzn://apps/android?p=com.wisewinggames.dunkhithoopshoot.amz&showAll=1";

		public void Setup ()
		{
			#if UNITY_EDITOR
			switch (appStore) {

			case AppStore.GooglePlay:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, googlePlayStoreBundleID);

			#if UNITY_PURCHASING && UNITY_EDITOR
				UnityPurchasingEditor.TargetAndroidStore (UnityEngine.Purchasing.AndroidStore.GooglePlay);
			#endif

				break;

			case AppStore.Amazon:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, amazonAppStoreBundleID);
	
			#if UNITY_PURCHASING && UNITY_EDITOR
				UnityPurchasingEditor.TargetAndroidStore (UnityEngine.Purchasing.AndroidStore.AmazonAppStore);
			#endif
				
				break;
			}
			#endif
		}

	}

}
