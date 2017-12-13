using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
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


		public void SetBundleIdentifier ()
		{
			#if UNITY_EDITOR
			switch (appStore) {

			case AppStore.GooglePlay:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, googlePlayStore);
				break;

			case AppStore.Amazon:
				PlayerSettings.SetApplicationIdentifier (BuildTargetGroup.Android, amazonAppStore);
				break;
			}
			#endif
		}

	}

}
