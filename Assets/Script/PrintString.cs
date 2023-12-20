using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrintString : MonoBehaviour
{
    ///<summary>表示するテキストの配列</summary>
    [SerializeField] string[] texts = null;
    ///<summary>表示するテキスト</summary>
    [SerializeField] Text _printText = null;
    ///<summary>テキストの表示時間</summary>
    [SerializeField] float _printDuration = 0.5f;

    private string _currentText;
    private int _stringIndex;


    private void Awake()
    {
        GameInfo.Instance.Printer = this;
    }
    /// <summary>テキストを表示するメソッド</summary>
    /// <param name="textnum">表示するテキストの要素番号</param>
    IEnumerator PrintOneByOne(int textnum)
    {
        _printText.text = null;
        _currentText = texts[textnum];
        _stringIndex = _currentText.Length;

        for (int i = 0; i < _stringIndex; i++)
        {
            _printText.text += _currentText[i];
            yield return new WaitForSeconds(_printDuration);
        }

        _currentText = null;
    }

    public void CallPrintOneByOne(int textnum)
    {
        StartCoroutine(PrintOneByOne(textnum));
    }
}
