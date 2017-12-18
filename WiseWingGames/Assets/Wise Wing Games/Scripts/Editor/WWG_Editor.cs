using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WiseWingGames
{

	public class WWG_Editor
	{
		
		[MenuItem("Wise Wing Games/General Settings", false, -100)]
		public static void SelectGeneralSettings(){
			string[] folder = new string[]{ "Assets/Wise Wing Games/Settings" };

			if (AssetDatabase.FindAssets ("GeneralSettings", folder).Length == 1) {
				Selection.activeObject = AssetDatabase.LoadMainAssetAtPath ("Assets/Wise Wing Games/Settings/GeneralSettings.asset");
			} else {
				if (EditorUtility.DisplayDialog ("Wise Wing Games", "General Settings not found, Create now?", "Yes!", "No")) {
					CreateGeneralSettings ();
				}
			}

		}


		[MenuItem ("Wise Wing Games/Ads Settings", false, 0)]
		public static void SelectAdsSettings ()
		{
		
			string[] folder = new string[]{ "Assets/Wise Wing Games/Settings" };

			if (AssetDatabase.FindAssets ("AdsSettings", folder).Length == 1) {
				Selection.activeObject = AssetDatabase.LoadMainAssetAtPath ("Assets/Wise Wing Games/Settings/AdsSettings.asset");
			} else {
				if (EditorUtility.DisplayDialog ("Wise Wing Games", "Ads Settings not found, Create now?", "Yes!", "No")) {
					CreateAdSettings ();
				}
			}
		}



		public static void CreateGeneralSettings ()
		{
			GeneralSettings asset = ScriptableObject.CreateInstance < GeneralSettings > ();
			if (!AssetDatabase.IsValidFolder ("Assets/Wise Wing Games/Settings")) {
				AssetDatabase.CreateFolder ("Assets/Wise Wing Games", "Settings");
			}
			AssetDatabase.CreateAsset (asset, "Assets/Wise Wing Games/Settings/GeneralSettings.asset");

			AssetDatabase.SaveAssets ();

			string[] folder = new string[]{ "Assets/Wise Wing Games/Settings" };
			if (AssetDatabase.FindAssets ("GeneralSettings", folder).Length == 1) {
				EditorUtility.DisplayDialog ("Wise Wing Games", "General Settings created Successfully!!!  :)", "Acha!");
			} else {
				EditorUtility.DisplayDialog ("Wise Wing Games", "General Settings creation Failed!!!  See Console!  :(", "Acha!");
			}
			SelectGeneralSettings ();

		}


		public static void CreateAdSettings ()
		{
			AdsSettings asset = ScriptableObject.CreateInstance < AdsSettings > ();
			if (!AssetDatabase.IsValidFolder ("Assets/Wise Wing Games/Settings")) {
				AssetDatabase.CreateFolder ("Assets/Wise Wing Games", "Settings");
			}
			AssetDatabase.CreateAsset (asset, "Assets/Wise Wing Games/Settings/AdsSettings.asset");

			AssetDatabase.SaveAssets ();

			string[] folder = new string[]{ "Assets/Wise Wing Games/Settings" };
			if (AssetDatabase.FindAssets ("AdsSettings", folder).Length == 1) {
				EditorUtility.DisplayDialog ("Wise Wing Games", "Ads Settings created Successfully!!!  :)", "Acha!");
			} else {
				EditorUtility.DisplayDialog ("Wise Wing Games", "Ads Settings creation Failed!!!  See Console!  :(", "Acha!");
			}
			SelectAdsSettings ();

		}

		[MenuItem("Wise Wing Games/Clear Saved Data", false , 100)]
		public static void ClearPlayerPrefs(){
			PlayerPrefs.DeleteAll ();
		}



		[CustomEditor(typeof(GeneralSettings))]
		public class GeneralSettingsEditor : Editor {

			override public void  OnInspectorGUI () {
				GeneralSettings general = (GeneralSettings)target;
				DrawDefaultInspector();
				GUILayout.Space(20);
				if(GUILayout.Button("Setup")) {
					general.Setup();
				}

			}
		}

	}
}
