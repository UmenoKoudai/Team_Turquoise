using UnityEngine;
using UnityEngine.UI;

/// <summary>Player�߂��̂ɂ���ActionImage���R���g���[������</summary>
public class PlayerUIControlle : MonoBehaviour
{
    [SerializeField]
    [Header("ActionImage�\���p�L�����o�X")]
    Canvas _actionCanvas;

    [SerializeField]
    [Header("ActionImage")]
    Image _actionImage;

    /// <summary>Player�I�u�W�F�N�g���_�Ƃ���Canvas��Positon</summary>
    Vector2 _localPos;

    private void Start()
    {
        //�ŏ�Player�{�̂�e�I�u�W�F�N�g�ɂ���
        _actionCanvas.transform.parent = transform.GetChild(0).GetComponent<Transform>();
        _localPos = _actionCanvas.transform.localPosition;
        ActionImageActive(false);
    }
    /// <summary>�A�N�V������UI��Image��\���E��\��</summary>
    /// <param name="active">�\���E��\��</param>
    public void ActionImageActive(bool active)
    {
        _actionImage.enabled = active;
    }

    public void PlayerBodyParentChange(Transform parent)
    {
        ActionImageActive(false);
        _localPos = _actionCanvas.transform.localPosition;
        _actionCanvas.transform.parent = parent;
        _actionCanvas.transform.localPosition = _localPos;
    }
}
