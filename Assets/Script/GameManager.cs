using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Text __skillCountText;

    [SerializeField]
    GameState _state = GameState.Title;

    //private int _skillCount = 0;
    public int SkillCount = 0;

    public enum GameState
    {
        Title,
        InGame,
        GameOver,
        Result,
    }

    private void OnEnable()
    {
        GameInfo.Instance.GameManager = this;
    }

    void Update()
    {
        if (_state != GameState.InGame) return;
        //_skillCountText.text = _skillCount.ToString();
    }

    public void StateChange(GameState state)
    {
        _state = state;
    }

    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
