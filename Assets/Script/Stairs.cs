using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField]
    private GameManager.SceneState _scene;

    GameInfo _gameInfo;

    private void Awake()
    {
        _gameInfo = GameInfo.Instance.Set();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            _gameInfo.GameManager.SceneChange(_scene);
        }
    }
}
