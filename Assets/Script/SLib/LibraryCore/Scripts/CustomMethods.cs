// 管理者 菅沼
using System;
namespace SLib
{
    #region 独自メソッド Original Methods
    /// <summary> 独自のメソッドを提供するクラス </summary>
    public static class OriginalMethods
    {
        /// <summary> 
        /// <para>第１引数が真の時のみ第２引数の処理を実行する </para>
        /// When 1st Argument is True, Do 2nd Arguments Process
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Knock(bool condition, Action action)
        {
            if (condition) { action(); }
        }
        /* ------------------------------------------------------------------ */
    }
    #endregion
}