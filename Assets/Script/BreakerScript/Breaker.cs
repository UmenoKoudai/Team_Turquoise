using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary> ブレーカーのクラス </summary>
public class Breaker : MonoBehaviour, IAction
{
    [SerializeField,
        Header("アクティブ化された時に発火するイベント")]
    UnityEvent ActivatedAction;

    bool _isActivated = false;

    public void Action(GameInfo info)
    {
        if (!_isActivated)
        {
            ActivatedAction.Invoke();
            _isActivated = true;
        }
        else
        {
            return;
        }
    }

    public bool IsActivated() { return _isActivated; }

}
