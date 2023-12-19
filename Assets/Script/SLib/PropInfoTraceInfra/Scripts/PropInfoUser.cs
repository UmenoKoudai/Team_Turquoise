// 管理者 菅沼
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary> プロパティ情報インフラの利用部のクラスが継承するべきインターフェイス </summary>
public interface IPropInfoUser
{
    PropertyInfoHandlerLinker PropInfoHandlerLinker { get; set; }
    PropertyInfoHandler PropInfoHandler { get; set; }
    List<string> ResisterNameList { get; set; }
}
/// <summary> プロパティ情報インフラ利用部の基底クラス。機能を利用するためにこれを継承する </summary>
[RequireComponent(typeof(PropertyInfoHandler))]
public abstract class PropInfoUser : MonoBehaviour
{
    protected abstract void SetUpPropInfoUser();
}