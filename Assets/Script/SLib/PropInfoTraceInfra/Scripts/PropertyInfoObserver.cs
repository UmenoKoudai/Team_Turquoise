// 管理者 菅沼
using SLib;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary> プロパティ情報の構造体。レジスタ名、データ、型を取得できる </summary>
public struct PropInfoCallBackContext // ← どのレジスタ名のデータが変化したかの構造体
{
    public string _resisterName;
    public object _resisterData;
    public Type _resisterDataType;
    public PropInfoCallBackContext(string resisterName, object resisterData)
    {
        _resisterData = resisterData;
        _resisterName = resisterName;
        _resisterDataType = resisterData.GetType();
    }
    public override string ToString()
    {
        return $"[{_resisterName} : {_resisterData} : {_resisterDataType}]";
    }
}
/// <summary> プロパティ情報オブザーバーの派生クラスが継承するべきインターフェイス </summary>
public interface IPropInfoObserver
{
    void OnSenderPropertyValueChanged(PropInfoCallBackContext context);
    void OnReciverPropertyValueChanged(PropInfoCallBackContext context);
}
/// <summary> プロパティ情報の変化を監視するクラス。監視に必要な機能を提供する </summary>
public class PropertyInfoObserver : MonoBehaviour
{
    [SerializeField] PropertyInfoHandlerLinker _targetPropInfoHandlerLinker;
    List<string> _targetSenderResisterList = new();
    List<string> _targetReceiverResisterList = new();
    public delegate void ChangedValueDelegate(PropInfoCallBackContext context);
    public event ChangedValueDelegate OnSenderDataHasChanged;
    public event ChangedValueDelegate OnReceiverDataHasChanged;
    // Use For Compare Data
    DataDictionary<string, object> _pastSenderDataPair = new();
    DataDictionary<string, object> _pastReceiverDataPair = new();
    private void Update()
    {
        _targetSenderResisterList = _targetPropInfoHandlerLinker.SenderResisters;
        _targetReceiverResisterList = _targetPropInfoHandlerLinker.ReceiverResisters;
        CheckSenderDataChange();
        CheckReceiverDataChange();
    }
    private void CheckSenderDataChange()
    {
        foreach (var item in _targetSenderResisterList) // <- SENDER
        {
            if (_pastSenderDataPair[item] == null)
            {
                _pastSenderDataPair.Add(item, _targetPropInfoHandlerLinker.ExtractDataFromSender(item));
            }
            if (!_targetPropInfoHandlerLinker.ExtractDataFromSender(item).Equals(_pastSenderDataPair[item]))
            {
                var cntxt = new PropInfoCallBackContext(item, _targetPropInfoHandlerLinker.ExtractDataFromSender(item));
                if (OnSenderDataHasChanged != null) { OnSenderDataHasChanged(cntxt); }
                else { Debug.Log("Sender Data Changed Event Is NULL"); }
                _pastSenderDataPair[item] = _targetPropInfoHandlerLinker.ExtractDataFromSender(item);
            }
        } // Compare Data Between This Obeserver To Linker Property Info
    }
    private void CheckReceiverDataChange()
    {
        foreach (var item in _targetReceiverResisterList) // <- RECEIVER
        {
            if (_pastReceiverDataPair[item] == null)
            {
                _pastReceiverDataPair.Add(item, _targetPropInfoHandlerLinker.ExtractDataFromReceiver(item));
            }
            if (!_targetPropInfoHandlerLinker.ExtractDataFromReceiver(item).Equals(_pastReceiverDataPair[item]))
            {
                var cntxt = new PropInfoCallBackContext(item, _targetPropInfoHandlerLinker.ExtractDataFromReceiver(item));
                if (OnReceiverDataHasChanged != null) { OnReceiverDataHasChanged(cntxt); }
                else { Debug.Log("Sender Data Changed Event Is NULL"); }
                _pastReceiverDataPair[item] = _targetPropInfoHandlerLinker.ExtractDataFromReceiver(item);
            }
        } // Compare Data Between This Obeserver To Linker Property Info
    }
}
// (object)int -> System.Int32 : (object)float -> System.Single : (object)string -> System.String