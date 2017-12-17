using UnityEngine;
using UnityEngine.UI;

namespace WiseWingGames
{
	

	public class GameServicesButton : MonoBehaviour
	{

		Button _button;

		public enum GS_Button
		{
			ShowLeaderboard,
			ShowAchievements,
			SignIn,
			SignOut
		}

		public GS_Button buttonType;


		void Start ()
		{
			_button = GetComponent<Button> ();		

			_button.onClick.AddListener (delegate() {});
		
		}


		void OnClickButton(){
			switch (buttonType) {

			case GS_Button.ShowLeaderboard:
				GameServicesManager.instance.ShowLeaderboardUI ();
				break;

			case GS_Button.ShowAchievements:
				GameServicesManager.instance.ShowAchievementUI ();
				break;

			case GS_Button.SignIn:
				GameServicesManager.instance.SignIn ();
				break;

			case GS_Button.SignOut:
				GameServicesManager.instance.SignOut ();
				break;

			}
		}

	}
}
