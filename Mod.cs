using System;
using MelonLoader;
using UnityEngine;

namespace WNP78
{
    public class UnityLogs : MelonMod
    {
        Application.LogCallback callback;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            callback = callback ?? new Action<string, string, LogType>(Log);

            Application.add_logMessageReceived(callback);
        }

        public override void OnApplicationQuit()
        {
            base.OnApplicationQuit();

            Application.remove_logMessageReceived(callback);
        }

        private void Log(string condition, string stackTrace, LogType type)
        {
            var s = condition + "\n" + stackTrace;

            switch (type)
            {
                case LogType.Error:
                case LogType.Exception:
                    MelonLogger.LogError(s);
                    break;
                case LogType.Warning:
                case LogType.Assert:
                    MelonLogger.LogWarning(s);
                    break;
                default:
                    MelonLogger.Log(s);
                    break;
            }
        }
    }
}
