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
		public string googlePlayStore;
		public string amazonAppStore;


		public void Setup ()
		{
			#if UNITY_EDITOR
			switch (appStore) {

			case AppStore.GooglePlay:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, googlePlayStore);

			#if UNITY_PURCHASING && UNITY_EDITOR
				UnityPurchasingEditor.TargetAndroidStore (UnityEngine.Purchasing.AndroidStore.GooglePlay);
			#endif

				break;

			case AppStore.Amazon:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, amazonAppStore);
	
			#if UNITY_PURCHASING && UNITY_EDITOR
				UnityPurchasingEditor.TargetAndroidStore (UnityEngine.Purchasing.AndroidStore.AmazonAppStore);
			#endif
				
				break;
			}
			#endif
		}

	}

}
