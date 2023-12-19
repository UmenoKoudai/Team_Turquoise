using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Playerの幽体・本体のインターフェイス</summary>
public interface IState
{
    /// <summary>OnUpdateの前に１回呼ばれる・ここの中にPlayerのコンポーネント参照保存等を書く</summary>
    public void OnStart();

    public void OnEnter();
    /// <summary>毎フレームで呼ばれる・ここの中にPlayerの操作等を書く</summary>
    public void OnUpdate();
    /// <summary>FixedUpdateで呼ばれるもの・ここの中にPlayerのRigidbodyの操作を行う</summary>
    public void OnFixedUpdate();

    public void OnExit();
}
