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
        _isActivated = (!_isActivated) ? true : _isActivated;
        ActivatedAction.Invoke();
    }

    public bool IsActivated() { return _isActivated; }
}
