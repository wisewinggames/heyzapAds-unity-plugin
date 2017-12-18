using UnityEngine;
using System.Collections;

namespace WiseWingGames
{


	public interface IWebViewListener
	{

		void onPageStarted (string s);

		void onPageFinished (string s);
	}
}