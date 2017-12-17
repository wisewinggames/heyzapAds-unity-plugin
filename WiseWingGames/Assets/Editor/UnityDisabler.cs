
using UnityEditor;
using UnityEditor.Advertisements;

public class UnityDisabler : Editor {

	// Use this for initialization
	void Awake () {
		AdvertisementSettings.initializeOnStartup = false;
	}

}
