using UnityEngine;
using UnityEngine.UI;

/// <summary>Player近くのにあるActionImageをコントロールする</summary>
public class PlayerUIControlle : MonoBehaviour
{
    [SerializeField]
    [Header("ActionImage表示用キャンバス")]
    Canvas _actionCanvas;

    [SerializeField]
    [Header("ActionImage")]
    Image _actionImage;

    /// <summary>Playerオブジェクト原点としたCanvasのPositon</summary>
    Vector2 _localPos;

    private void Start()
    {
        //最初Player本体を親オブジェクトにする
        _actionCanvas.transform.parent = transform.GetChild(0).GetComponent<Transform>();
        _localPos = _actionCanvas.transform.localPosition;
        ActionImageActive(false);
    }
    /// <summary>アクションのUIのImageを表示・非表示</summary>
    /// <param name="active">表示・非表示</param>
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
