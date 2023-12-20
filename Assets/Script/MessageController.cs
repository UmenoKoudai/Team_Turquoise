using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    /// <summary>メッセージ呼び出し用のインデックス</summary>
    [SerializeField] List<int> _indexList;
    /// <summary></summary>
    private Action<List<int>> _messageAction;
    void Start() => _messageAction = GameInfo.Instance.GameManager.ShowMessage;

    public void CallShowMessage() => _messageAction(_indexList);
}
