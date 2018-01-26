using TheNextFlow.UnityPlugins;
using System;

namespace WiseWingGames {
    public static class PopUp {


        public static void OpenAlertDialog(string title, string message, string cancel, Action onCancel)
        {
            MobileNativePopups.OpenAlertDialog(title, message, cancel, onCancel);
        }

        public static void OpenAlertDialog(string title, string message, string ok, string cancel, Action onOk, Action onCancel)
        {
            MobileNativePopups.OpenAlertDialog( title,  message,  ok,  cancel,  onOk,  onCancel);
        }

        public static void OpenAlertDialog(string title, string message, string ok, string neutral, string cancel, Action onOk, Action onNeutral, Action onCancel)
        {
            MobileNativePopups.OpenAlertDialog( title,  message,  ok,  neutral,  cancel,  onOk,  onNeutral,  onCancel);
        }

    }
  }