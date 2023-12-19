using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Text __skillCountText;

    [SerializeField]
    GameState _gameState = GameState.Title;

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
