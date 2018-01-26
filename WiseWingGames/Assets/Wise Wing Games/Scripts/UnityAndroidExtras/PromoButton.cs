using UnityEngine;
using UnityEngine.UI;


namespace WiseWingGames
{
    [RequireComponent(typeof(Button))]
    public class PromoButton : MonoBehaviour
    {
        public enum ButtonType
        {
            RateMyGame, MoreGames, FacebookPage, ShareScreenshot, OpenShare
        }
        public ButtonType _buttonType;

        Button _button;


        void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(delegate
            {
                OnClickCallback();
            });
        }


        void OnClickCallback()
        {

            switch (_buttonType)
            {

                case ButtonType.RateMyGame:
                    RateMyGame();
                    break;

                case ButtonType.MoreGames:
                    MoreGames();
                    break;

                case ButtonType.FacebookPage:
                    OpenFacebookPage();
                    break;


                case ButtonType.ShareScreenshot:
                    ShareScreenshot();
                    break;

                case ButtonType.OpenShare:
                    OpenShareOptions();
                    break;
            }
        }

#if UNITY_ANDROID
        void RateMyGame()
        {
            UnityAndroidExtras.instance.RateMyGame();
        }

        void MoreGames()
        {
            UnityAndroidExtras.instance.OpenDeveloperPage();
        }

        void OpenFacebookPage()
        {
            UnityAndroidExtras.instance.OpenFacebookPage();
        }

        void ShareScreenshot()
        {
            UnityAndroidExtras.instance.ShareScreenshot();

        }

        void OpenShareOptions()
        {
            UnityAndroidExtras.instance.OpenShareGameLink();
        }

#elif UNITY_IOS
        void RateMyGame()
        {

        }

        void MoreGames()
        {

        }

        void OpenFacebookPage()
        {

        }

        void ShareScreenshot()
        {


        }

        void OpenShareOptions()
        {

        }
#endif
    }
}