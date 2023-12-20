using UnityEngine;

public class SceneChangeInvoker : MonoBehaviour
{
    [SerializeField]
    GameManager.SceneState _state;

    public void SceneLoading()
    {
        AudioController.Instance.SePlay(AudioController.SeClass.SE.Click);
        GameInfo.Instance.GameManager.SceneChange(_state);
    }
}
