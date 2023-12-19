// 管理者 菅沼
using System;
using UnityEngine;
namespace SLib
{
    namespace Singleton
    {
        /// <summary> このクラスを継承することによって、シングルトンパターンのビヘイビアの機能を提供する </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class SingletonBaseClass<T> : MonoBehaviour where T : Component
        {
            static T Instance;
            public static T SingletonInstance => Instance;
            protected void Awake()
            {
                if (Instance != null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Instance = this as T;
                    DontDestroyOnLoad(gameObject);
                    ToDoAtAwakeSingleton();
                }
            }
            /// <summary> Awake関数内で呼び出してほしい処理を書く </summary>
            protected abstract void ToDoAtAwakeSingleton();
        }
    }
}