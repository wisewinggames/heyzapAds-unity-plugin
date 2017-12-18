using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WiseWingGames
{
	[RequireComponent (typeof(Button))]
	public class ClearPurchases : MonoBehaviour
	{

		Button button;

		void Start ()
		{
			button = GetComponent<Button> ();

			button.onClick.AddListener (() => {
				UnityPurchasing.ClearTransactionLog ();
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			});
		}
	

	}
}