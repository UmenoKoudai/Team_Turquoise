using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ドアのロックコンソールのクラス。 </summary>
public class DoorLockingConsole : MonoBehaviour
{
    [SerializeField,
        Header("これをアクティブにするため必要なブレーカー")]
    List<Breaker> _breakers = new List<Breaker>();

    [SerializeField]
    Animator _anim;

    int _activeCount;

    bool _isActive = false;

    public void NotifyActivated()
    {
        if (_activeCount + 1 <= _breakers.Count)
        {
            _activeCount++;
        }

        if (_activeCount == _breakers.Count && !_isActive)
        {
            _isActive = true;
            _anim?.Play("OpenDoor");
        }
    }
}
