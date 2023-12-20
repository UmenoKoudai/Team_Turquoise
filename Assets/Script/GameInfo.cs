using UnityEngine;

public class GameInfo : MonoBehaviour
{
    static GameInfo _instance;
    public static GameInfo Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType<GameInfo>();
                if(!_instance)
                {
                    Debug.LogError("GameInfoスクリプトがシーン上に存在しません");
                }
            }
            return _instance;
        }
    }

    //public Player Player;
    private GameManager _gameManager;
    public GameManager GameManager
    {
        get => _gameManager; set => _gameManager = value;
    }

    private PrintString _printer;
    public PrintString Printer
    {
        get => _printer; set => _printer = value;
    }

    public GameInfo Set()
    {
        AudioController.Instance.SePlay(AudioController.SeClass.SE.Walk);
        return GameInfo.Instance;
    }
}
