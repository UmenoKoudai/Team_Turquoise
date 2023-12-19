using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    GameInfo _gameInfo;

    private void Awake()
    {
        _gameInfo = GameInfo.Instance.Set();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _gameInfo.GameManager.SceneChange(_sceneName);
        }
    }
}
