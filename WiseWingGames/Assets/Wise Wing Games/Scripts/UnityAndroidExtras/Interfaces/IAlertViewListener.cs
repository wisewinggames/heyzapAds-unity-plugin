using UnityEngine;
using System.Collections;

namespace WiseWingGames
{
	
	public interface IAlertViewListener
	{

		void onAlertButtonClicked (string s);

		void onAlertNegativeButtonClicked (string s);
	}
}