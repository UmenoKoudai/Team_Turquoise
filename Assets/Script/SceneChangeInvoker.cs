using UnityEngine;

public class SceneChangeInvoker : MonoBehaviour
{
    [SerializeField]
    GameManager.SceneState _state;

    public void SceneLoading()
    {
        GameInfo.Instance.GameManager.SceneChange(_state);
    }
}
