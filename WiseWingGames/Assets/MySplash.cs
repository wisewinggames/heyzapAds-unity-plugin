using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySplash : MonoBehaviour
{

	AsyncOperation sceneAO;

	public enum SkipScreen
	{
None,
		Splash_Screen,
		Loading_Screen,
		Both

	}


	[Header ("Variables")]
	[SerializeField] GameObject splashUI;
	[SerializeField] GameObject loadingUI;
	[SerializeField] Slider loadingProgbar;
	[SerializeField] Text loadingText;

	[Header ("Settings")]
	public SkipScreen skipScreen;
	public bool tapToContinue = true;
	[Tooltip ("How long the splash screen should take to go to loading screen (in seconds)")]
	[SerializeField] float splashScreenTime = 5f;
	[Tooltip ("Exact name of the scene to load after this scene")]
	[SerializeField] string sceneNameToLoad = "2 Main Menu";


	// the actual percentage while scene is fully loaded
	private const float LOAD_READY_PERCENTAGE = 0.9f;

	void Start ()
	{
		switch (skipScreen) {

		case SkipScreen.None:
			splashUI.SetActive (true);
			loadingUI.SetActive (false);

			StartCoroutine (SplashRoutine ());

			break;

		case SkipScreen.Splash_Screen:
			splashUI.SetActive (false);
			loadingUI.SetActive (true);

			LoadScene (sceneNameToLoad);

			break;

		case SkipScreen.Loading_Screen:
			splashUI.SetActive (true);
			loadingUI.SetActive (false);

			LoadScene (sceneNameToLoad);

			break;

		case SkipScreen.Both:
			LoadScene (sceneNameToLoad);
			break;
		
		} 	
	}

	IEnumerator SplashRoutine ()
	{
		yield return new WaitForSeconds (splashScreenTime);
		splashUI.SetActive (false);
		loadingUI.SetActive (true);
		LoadScene (sceneNameToLoad);
	}

	public void LoadScene (string sceneName)
	{

		if (loadingText)
			loadingText.text = "LOADING...";
		StartCoroutine (LoadingSceneRealProgress (sceneName));
	}

	IEnumerator LoadingSceneRealProgress (string sceneName)
	{
		yield return new WaitForSeconds (1);
		sceneAO = SceneManager.LoadSceneAsync (sceneName);

		// disable scene activation while loading to prevent auto load
		sceneAO.allowSceneActivation = false;

		while (!sceneAO.isDone) {
			if (loadingProgbar)
				loadingProgbar.value = sceneAO.progress;

			if (sceneAO.progress >= LOAD_READY_PERCENTAGE) {
				if (loadingProgbar)
					loadingProgbar.value = 1f;

				if (loadingText)
					loadingText.text = "TAP TO CONTINUE";

				if (Input.GetMouseButtonDown (0) || skipScreen == SkipScreen.Loading_Screen || !tapToContinue) {

					sceneAO.allowSceneActivation = true;
				}

			}
			yield return null;
		}
	}

	void OnDestroy ()
	{
		Resources.UnloadUnusedAssets ();
	}
}
