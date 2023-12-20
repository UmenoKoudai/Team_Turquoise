using UnityEngine;
using UnityEngine.Playables;

public class ShowGameOver : MonoBehaviour
{
    [SerializeField]
    PlayableDirector _timeLine;

    public void GameOver()
    {
        _timeLine?.Play();
    }
}
