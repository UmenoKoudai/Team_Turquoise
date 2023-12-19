using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrakerSwitch : MonoBehaviour
{
    [SerializeField]
    Light2D _activeLight;

    bool _active = false;

    public void SwitchActive()
    {
        if (!_activeLight) return;
        _active = !_active;
        if(_active)
        {
            _activeLight.color = Color.green;
        }
        else
        {
            _activeLight.color = Color.red;
        }
    }
}
