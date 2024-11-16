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
    [SerializeField]
    AudioController.BgmClass.BGM _sceneBGM;

    bool _isOpen = false;
    bool _isMessageEnd = false;
    public bool IsMessageEnd
    {
        get => _isMessageEnd;
        set
        {
            _anim.Play("Close");
            _isOpen = false;
        }
    }

    public enum GameState
    {
        Title,
        InGame,
        GameOver,
        Text,
    }

    public enum SceneState
    {
        Title,
        Opening,
        B2F,
        B1F,
        Result,
        GameOver,
        StaffRoll,
    }

    private void OnEnable()
    {
        GameInfo.Instance.GameManager = this;
        //ShowMessage(_messageIndex);
    }

    private void Start()
    {
        AudioController.Instance.BgmPlay(_sceneBGM);
    }

    public void MessageWindow()
    {
        if (!_isOpen)
        {
            _anim.Play("Show");
            _isOpen = true;
        }
    }

    public void ShowMessage(List<int> messageIndex)
    {
        StateChange(GameState.Text);
        MessageWindow();
        while (messageIndex.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                int index = messageIndex[0];
                //GameInfo.Instance.Printer.CallPrintOneByOne(index);
                messageIndex.RemoveAt(0);
            }
        }
        IsMessageEnd = true;
    }

    public void StateChange(GameState state)
    {
        _gameState = state;
    }

    public void SceneChange(SceneState scene)
    {
        switch (scene)
        {
            case SceneState.Title:
                StartCoroutine(SceneChange("Title"));
                break;
            case SceneState.Opening:
                StartCoroutine(SceneChange("Opening"));
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
            case SceneState.StaffRoll:
                StartCoroutine(SceneChange("StaffRoll"));
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
