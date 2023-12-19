// 管理者 菅沼
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace SLib
{
    /// <summary> 拡張メソッドを提供するクラス </summary>
    public static class MethodsExtension
    {
        /* GameObjects */
        /// <summary>指定されたトランスフォームの子オブジェクトにする</summary>
        /// <param name="obj"></param>
        /// <param name="parent"></param>
        public static void ToChildObject(this GameObject obj, Transform parent)
        {
            obj.transform.parent = parent;
        }
        /// <summary>オブジェクトの親子関係を切る</summary>
        /// <param name="obj"></param>
        public static void ToParenObject(this GameObject obj)
        {
            obj.transform.parent = null;
        }
        /// <summary>子オブジェクトのみ取得する</summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Transform> GetChildObjects(this GameObject parent)
        {
            List<Transform> list = new();
            var cnt = parent.transform.childCount;
            for (int i = 0; i < cnt; i++)
            {
                var child = parent.transform.GetChild(i);
                list.Add(child);
            }
            return list;
        }
    }
}