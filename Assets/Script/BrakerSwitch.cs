using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrakerSwitch : MonoBehaviour
{
    [SerializeField]
    Light2D _activeLight;

    bool _active = false;

    public void SwitchActive()
    {
        _active = !_active;
        if (!_activeLight) return;
        _activeLight.color = Color.green;
        AudioController.Instance.SePlay(AudioController.SeClass.SE.Breaker);
    }
}
