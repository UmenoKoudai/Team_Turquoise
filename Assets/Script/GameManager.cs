using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameState _gameState = GameState.Title;
    [SerializeField]
    List<int> _messageIndex = new List<int>();
    [SerializeField]
    Animator _anim;

    bool _isOpen = false;
    bool _isMessageEnd = false;

    public enum GameState
    {
        Title,
        InGame,
        GameOver,
    }
    
    public enum SceneState
    {
        Title,
        B2F,
        B1F,
        Result,
        GameOver,
    }

    private void OnEnable()
    {
        GameInfo.Instance.GameManager = this;
        ShowMessage(_messageIndex);
    }

    public void MessageWindow()
    {
        if(!_isOpen)
        {
            _anim.Play("Show");
            _isOpen = true;
        }
        if(_isMessageEnd)
        {
            _anim.Play("Close");
            _isOpen = false;
        }
    }

    public void ShowMessage(List<int> messageIndex)
    {
        MessageWindow();
        if (messageIndex.Count > 0)
        {
            int index = messageIndex[0];
            GameInfo.Instance.Printer.CallPrintOneByOne(index);
            messageIndex.RemoveAt(0);
        }
        else
        {
            _isMessageEnd = true;
        }
    }

    public void StateChange(GameState state)
    {
        _gameState = state;
    }

    public void SceneChange(SceneState scene)
    {
        switch(scene)
        {
            case SceneState.Title:
                StartCoroutine(SceneChange("Title"));
                break;
            case SceneState.B2F:
                StartCoroutine(SceneChange("B2F"));
                break;
            case SceneState.B1F:
                StartCoroutine(SceneChange("B1F"));
                break;
            case SceneState.Result:
                StartCoroutine(SceneChange("1F"));
                break;
            case SceneState.GameOver:
                StartCoroutine(SceneChange("GameOver"));
                break;
        }
    }

    IEnumerator SceneChange(string sceneName)
    {
        Application.LoadLevelAdditive("Loading");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneName);
    }
}
