using UnityEngine;
using WiseWingGames;

public class RateUsPopUp : MonoBehaviour {


    void Start() {
        PopUp.OpenAlertDialog("Please Rate Us!", "Rate us", "Rate!", "Cancel", () =>
        {
            UnityAndroidExtras.instance.RateMyGame();
        }, () => {});
	}
	

}
