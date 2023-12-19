using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary> �u���[�J�[�̃N���X </summary>
public class Breaker : MonoBehaviour, IAction
{
    [SerializeField,
        Header("�A�N�e�B�u�����ꂽ���ɔ��΂���C�x���g")]
    UnityEvent ActivatedAction;

    bool _isActivated = false;

    public void Action(GameInfo info)
    {
        _isActivated = (!_isActivated) ? true : _isActivated;
        ActivatedAction.Invoke();
    }

    public bool IsActivated() { return _isActivated; }
}