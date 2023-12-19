// 管理者 菅沼
using System;
namespace SLib
{
    /// <summary> 特定の登録名に対応した値を格納するための機能を提供するクラスで、辞書っぽい機能を提供する </summary>
    /// <typeparam name="TDataKey"></typeparam>
    /// <typeparam name="TDataValue"></typeparam>
    [Serializable]
    public class DataDictionary<TDataKey, TDataValue>
    {
        //汎用データペア
        DataPair<TDataKey, TDataValue>[] _coreDataBase;
        public DataDictionary()//コンストラクタ
        {
            _coreDataBase = new DataPair<TDataKey, TDataValue>[0];//データベースのインスタンス化
        }
        ~DataDictionary()//デストラクタ
        {
            _coreDataBase = null;
        }
        public TDataValue this[TDataKey key]
        {
            get { return this.Find(key); }
            set { this.SetAt(key, value); }
        }
        public TDataKey this[TDataValue dataValue]
        {
            get { return this.Find(dataValue); }
            set { this.SetAt(dataValue, value); }
        }
        /// <summary> データの登録 </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TDataKey key, TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    throw new Exception("It Seems Already Appended Same Key Value");
                }
            }//Search Same Key Value
            Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length + 1);
            //Append Element
            _coreDataBase[_coreDataBase.Length - 1] = new DataPair<TDataKey, TDataValue>(key, value);
        }
        /// <summary> データの登録解除 </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Remove(TDataKey key, TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key)//If The Pair Found
                    && _coreDataBase[i].Value.Equals(value))
                {
                    _coreDataBase[i] = null;//Erase Element
                    for (int j = i; j < _coreDataBase.Length; j++)
                    {
                        _coreDataBase[j] = _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j];
                        _coreDataBase[(j + 1 < _coreDataBase.Length) ? j + 1 : j] = null;
                        Array.Resize<DataPair<TDataKey, TDataValue>>(ref _coreDataBase, _coreDataBase.Length - 1);
                    }//Make No Gap
                }
            }//Serach Pairs
        }
        /// <summary> DictionaryのValueからKeyを返す </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TDataKey Find(TDataValue value)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Value.Equals(value))
                {
                    return _coreDataBase[i].Key;
                }
            }
            return default(TDataKey);
        }
        /// <summary> DictionaryのKeyからValueを返す </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TDataValue Find(TDataKey key)
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    return _coreDataBase[i].Value;
                }
            }
            return default(TDataValue);
        }
        /// <summary> Valueに対応するデータを渡されたKeyの値にする </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public void SetAt(TDataValue value, TDataKey key)//Set Key By Value
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Value.Equals(value))
                {
                    _coreDataBase[i].SetKey(key);
                    break;
                }
            }
        }
        /// <summary> Keyに対応するデータを渡されたValueの値にする </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetAt(TDataKey key, TDataValue value)//Set Value By Key
        {
            for (int i = 0; i < _coreDataBase.Length; i++)
            {
                if (_coreDataBase[i].Key.Equals(key))
                {
                    _coreDataBase[i].SetValue(value);
                    break;
                }
            }
        }
        /// <summary> データペアを返す </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataPair<TDataKey, TDataValue> GetDataPair(int index)
        {
            return (_coreDataBase[index] != null) ? _coreDataBase[index] : null;
        }
        /// <summary> 特定のインデックスにデータを初期化 </summary>
        /// <param name="pair"></param>
        /// <param name="index"></param>
        public void SetDataPair(DataPair<TDataKey, TDataValue> pair, int index)
        {
            _coreDataBase[index] = pair;
        }
    }
    /// <summary> 
    /// Dictionaryみたいなデータペアのクラス。
    /// Listとの併用を想定して設計している。
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class DataPair<TKey, TValue>
    {
        TKey _key;
        public TKey Key => _key;
        TValue _value;
        public TValue Value => _value;
        /// <summary> 文字列型のKeyの値 </summary>
        public string SKey => _key.ToString();
        /// <summary> 文字列型のValueの値 </summary>
        public string SValue => _value.ToString();
        public DataPair(TKey key, TValue value)//コンストラクタ
        {
            _key = key;
            _value = value;
        }
        /// <summary> Keyをセット </summary>
        /// <param name="key"></param>
        public void SetKey(TKey key)
        {
            this._key = key;
        }
        /// <summary> Valueをセット </summary>
        public void SetValue(TValue value)
        {
            this._value = value;
        }
        /// <summary> Keyの型を返す </summary>
        public Type KeyType => _key.GetType();
        /// <summary> Valueの型を返す </summary>
        public Type ValueType => _value.GetType();
    }
}