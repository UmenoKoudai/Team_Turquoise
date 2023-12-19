// ŠÇ—Ò ›À
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SLib
{
    enum LoggingMode
    {
        Normal,
        Warning,
        Error,
    }
    /// <summary> “n‚³‚ê‚½•¶š—ñ‚ğ‚½‚¾DebugƒƒO‚Öo—Í‚·‚éB </summary>
    public class DummyLogger : MonoBehaviour
    {
        [SerializeField] LoggingMode _mode;
        public void DummyLoggerOutputLog(string message)
        {
            switch (_mode)
            {
                case LoggingMode.Warning:
                    Debug.LogWarning(message);
                    break;
                case LoggingMode.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }
    }
}