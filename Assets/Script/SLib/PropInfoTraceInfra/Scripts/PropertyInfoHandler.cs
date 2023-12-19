// 管理者 菅沼
using System;
using UnityEngine;
using SLib;
/*
* 特定のインスタンスのプロパティ値プールクラス
* のプロパティを監視するクラス
*/
/// <summary> レジスタとデータのペアからなるデータ構造で、
/// レジスタ名に応じたデータをためておくデータベース </summary>
public class PropertyInfoHandler : MonoBehaviour
{
    DataDictionary<string, object> _dataMap = new();
    public DataDictionary<string, object> DataMap => _dataMap;
    public event Action OnPropResisted = () => {Debug.Log("プロパティがレジストされた"); };
    public event Action OnPropUnResisted = () => { Debug.Log("プロパティがアンレジストされた"); };
    /// <summary> レジスタ名とデータの登録 </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resistName"></param>
    /// <param name="value"></param>
    public void Resist<T>(string resistName, T value)
    {
        _dataMap.Add(resistName, value);
        if (OnPropResisted != null) { OnPropResisted(); }
        else { throw new Exception("Assing Some Function!"); }
    }
    /// <summary> レジスタ名を指定して、 そのレジスタ名にあった登録情報をデータベースから消す</summary>
    /// <param name="resistedName"></param>
    public void UnResist(string resistedName)
    {
        _dataMap.Remove(resistedName, _dataMap[resistedName]);
        if (OnPropUnResisted != null) { OnPropUnResisted(); }
        else { throw new Exception("Assing Some Function!"); }
    }
    /// <summary> 指定したレジスタ名に対応したデータペアがデータベースに存在するか検索する </summary>
    /// <param name="resistName"></param>
    /// <returns></returns>
    public bool GetExist(string resistName)
    {
        return _dataMap[resistName] != null;
    }
}