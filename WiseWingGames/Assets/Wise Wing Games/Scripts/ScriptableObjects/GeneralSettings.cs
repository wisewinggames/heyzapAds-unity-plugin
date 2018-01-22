using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif

#if UNITY_EDITOR
using UnityEditor;

#if UNITY_PURCHASING 
using UnityEditor.Purchasing;

#endif
#endif

namespace WiseWingGames
{

    public class GeneralSettings : ScriptableObject
    {
        private string GOOGLE_DEFINE = "WWG_GOOGLE";
        private string AMAZON_DEFINE = "WWG_AMAZON";

        public enum Store
        {
            GooglePlay,
            Amazon,
            iTunes
        }

        [Header("App Store ")]
        public Store appStore;
        [Space(10)]
        [Header("Bundle Identifiers")]
        [Tooltip("This id will be used for google and itunes. On amazon '.amz' will be added at the end of the string.")]
        public string bundleID;

        public string GoogleBundleID { get { return bundleID; } }
        public string AmazonBundleID { get { return bundleID + ".amz"; } }
        public string ITunesBundleID { get { return bundleID; } }
        [Space(10)]
        [Header("Developer page links for MoreGames")]
        public string googlePlayDeveloperPageLink = "https://play.google.com/store/apps/dev?id=8294473886068372842";
        public string amazonDeveloperPageLink = "http://www.amazon.com/gp/mas/dl/android?p=com.wisewinggames.dunkhithoopshoot.amz&showAll=1";
        [Space(10)]
        [SerializeField]
        string VERSION = "1.0";
        [SerializeField]
        int BUILD_NUMBER = 1;

#if UNITY_EDITOR
        public void Setup()
        {

            switch (appStore)
            {

                case Store.GooglePlay:
                    SetBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                    SetBundleID(BuildTargetGroup.Android, GoogleBundleID);
                    AddDefineSymbols(BuildTargetGroup.Android, GOOGLE_DEFINE);

#if UNITY_PURCHASING
                    SetUnityIAPStore_Android(AppStore.GooglePlay);
#endif
                    break;

                case Store.Amazon:
                    SetBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                    SetBundleID(BuildTargetGroup.Android, AmazonBundleID);
                    AddDefineSymbols(BuildTargetGroup.Android, AMAZON_DEFINE);
#if UNITY_PURCHASING
                    SetUnityIAPStore_Android(AppStore.AmazonAppStore);
#endif
                    break;

                case Store.iTunes:
                    SetBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
                    SetBundleID(BuildTargetGroup.iOS, ITunesBundleID);
                    PlayerSettings.iOS.requiresFullScreen = true;
                    break;

            }
            SetVersionAndBuild(VERSION, BUILD_NUMBER);
        }

        void SetVersionAndBuild(string version, int build)
        {
            PlayerSettings.bundleVersion = version;
            if (appStore == Store.GooglePlay || appStore == Store.Amazon)
                PlayerSettings.Android.bundleVersionCode = build;
            else
                PlayerSettings.iOS.buildNumber = build.ToString();
        }

        void AddDefineSymbols(BuildTargetGroup targetGroup, string define)
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);
            definesString = definesString.Replace(GOOGLE_DEFINE, "");
            definesString = definesString.Replace(AMAZON_DEFINE, "");
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.Add(define);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, string.Join(";", allDefines.ToArray()));
        }

        void SetBuildTarget(BuildTargetGroup targetGroup, BuildTarget targetDevice)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(targetGroup, targetDevice);
        }

        void SetBundleID(BuildTargetGroup targetGroup, string bundleID)
        {
            PlayerSettings.SetApplicationIdentifier(targetGroup, bundleID);
        }
#if UNITY_PURCHASING 
        void SetUnityIAPStore_Android(AppStore appStore)
        {
            UnityPurchasingEditor.TargetAndroidStore(appStore);
        }
#endif
#endif
    }

}
