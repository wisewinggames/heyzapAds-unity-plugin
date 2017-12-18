using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace WiseWingGames
{
	public class ShowRewardAd : MonoBehaviour
	{
		[Space (20)]
		public string adTag = "free-currency";
		[Space (20)]
		public UnityEvent AdCompletedCallback;

		Button button;

		void Start ()
		{
			button = GetComponent<Button> ();

			button.onClick.AddListener (() => {
				UnityAndroidExtras.instance.makeToast("SHowing Reward ad", 1);
				AdManager.instance.ShowRewardAd (adTag);
			});
		}


		void OnEnable ()
		{
			AdManager.OnRewardAdCompleted += RewardAdCompleted;
		}

		void OnDisable ()
		{
			AdManager.OnRewardAdCompleted -= RewardAdCompleted;
		}


		void RewardAdCompleted (string _tag)
		{
			if (_tag == adTag)
				AdCompletedCallback.Invoke ();
		}
	
	
	}
		
}