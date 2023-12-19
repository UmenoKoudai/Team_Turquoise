// 管理者 菅沼
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary> プロパティ情報ハンドラーどうしのリンカの機能を提供する </summary>
public class PropertyInfoHandlerLinker : MonoBehaviour
{
    /// <summary> プロパティ参照元の情報ハンドラー </summary>
    [SerializeField] PropertyInfoHandler _sender; // プロパティ参照元
    public PropertyInfoHandler Sender => _sender;
    /// <summary> プロパティ参照元のデータの初期化先（ターゲット） </summary>
    [SerializeField] PropertyInfoHandler _receiver; // プロパティ参照値の初期化先
    public PropertyInfoHandler Receiver => _receiver;
    List<string> _senderResisters = new();
    public List<string> SenderResisters => _senderResisters;
    List<string> _receiverResisters = new();
    public List<string> ReceiverResisters => _receiverResisters;
    public event Action OnSenderDataUpdated;
    public event Action OnSenderDataSendedToReceiver;
    public event Action OnReceiverDataUpdated;
    public event Action OnReceiverrDataSendedToSender;
    private void Start()
    {
        if (_sender == null)
        { Debug.LogWarning("You Need Assing Sender PropInfoHandler"); }
        if (_sender == null)
        { Debug.LogWarning("You Need Assing Receiver PropInfoHandler"); }
        if (_sender == null && _receiver == null)
        { throw new Exception("You Need Assing Both Sender And Receiver"); }
    }
    #region 共通部
    /// <summary> データの横流しをサポートするメソッドSenderからReceiverへ流す </summary>
    /// <param name="senderPropHandler"></param>
    /// <param name="senderReristerName"></param>
    /// <param name="receiverPropHandler"></param>
    /// <param name="receiverResisterName"></param>
    void PassData(PropertyInfoHandler senderPropHandler, string senderReristerName, PropertyInfoHandler receiverPropHandler, string receiverResisterName)
    {
        receiverPropHandler.DataMap[receiverResisterName] = senderPropHandler.DataMap[senderReristerName];
    }
    /// <summary> 登録した値の更新 </summary>
    /// <param name="resisterName"></param>
    /// <param name="value"></param>
    void UpdateData(PropertyInfoHandler propHandler, string resisterName, object value) // プロパティ参照元に登録されている登録名に対応した値の更新
    {
        propHandler.DataMap[resisterName] = value;
    }
    /// <summary> データベースからのデータの抽出 </summary>
    /// <param name="resisterIndex"></param>
    /// <returns></returns>
    object ExtractData(PropertyInfoHandler propHandler, string resisterName)
    {
        return propHandler.DataMap[resisterName];
    }
    #endregion
    #region プロパティ情報センダ
    /// <summary> センダー登録名リストの登録 </summary>
    /// <param name="resisterNames"></param>
    public void ApplySenderResisterList(List<string> resisterNames) // 参照元から呼び出される
    {
        _senderResisters = resisterNames;
    }
    /// <summary> リンカに指定されたレシーバーのデータをセンダーに登録されてる値にする </summary>
    /// <param name="senderResisterName"> リンカに指定されているセンダーの保持するレジスタ名 </param>
    /// <param name="receiverResisterName"> リンカに指定されてるレシーバーの保持するレジスタ名 </param>
    public void SendDataSenderToReceiver(string senderResisterName, string receiverResisterName)
    {
        PassData(_sender, senderResisterName, _receiver, receiverResisterName);
        if (OnSenderDataSendedToReceiver != null) { OnSenderDataSendedToReceiver(); }
    }
    /// <summary> リンカに指定されているセンダーのレジスタのデータを更新する </summary>
    /// <param name="resisterName"> センダーのレジスタ名 </param>
    /// <param name="value"> 更新後の値 </param>
    public void UpdateSenderData(string resisterName, object value)
    {
        UpdateData(_sender, resisterName, value);
        if (OnSenderDataUpdated != null) { OnSenderDataUpdated(); }
    }
    /// <summary> リンカに指定されているセンダーから値を抽出する </summary>
    /// <param name="resisterName"> センダー側のレジスタ名 </param>
    /// <returns></returns>
    /// <exception cref="Exception"> データが見つからない場合に投げられる </exception>
    public object ExtractDataFromSender(string resisterName)
    {
        var ret = ExtractData(_sender, resisterName);
        return (ret != null) ? ret : throw new Exception("Data Wasnt Found");
    }
    #endregion
    #region プロパティ情報レシーバ
    /// <summary> レシーバー登録名のリストの登録 </summary>
    /// <param name="resisterNames"></param>
    public void ApplyReceiverResisterList(List<string> resisterNames) // 参照元から呼び出される
    {
        _receiverResisters = resisterNames;
    }
    /// <summary> リンカに指定されたセンダーのレジスタの登録値をレシーバーのレジスタに登録されてる値にする </summary>
    /// <param name="receiverResisterName"> リンカに指定されてるレシーバーの保持するレジスタ名 </param>
    /// <param name="senderResisterName"> リンカに指定されているセンダーの保持するレジスタ名 </param>
    public void SendDataReceiverToSender(string receiverResisterName, string senderResisterName)
    {
        PassData(_receiver, receiverResisterName, _sender, senderResisterName);
        if (OnReceiverrDataSendedToSender != null) { OnReceiverrDataSendedToSender(); }
    }
    /// <summary> リンカに指定されているレシーバーのデータの更新 </summary>
    /// <param name="resisterName"> リンカに指定されているレシーバーのレジスタ名 </param>
    /// <param name="value"> 更新後の値 </param>
    public void UpdateReceiverData(string resisterName, object value)
    {
        UpdateData(_receiver, resisterName, value);
        if (OnReceiverDataUpdated != null) { OnReceiverDataUpdated(); }
    }
    /// <summary> リンカに指定されているレシーバーからデータの抽出をする </summary>
    /// <param name="resisterName"> レシーバーのレジスタ名 </param>
    /// <returns></returns>
    /// <exception cref="Exception"> データが見つからない場合に投げられる </exception>
    public object ExtractDataFromReceiver(string resisterName)
    {
        var ret = ExtractData(_receiver, resisterName);
        return (ret != null) ? ret : throw new Exception("Data Wasnt Found");
    }
    #endregion
}