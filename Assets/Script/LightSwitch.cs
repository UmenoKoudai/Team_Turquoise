using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject _lightObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != 6) return;
        _lightObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;
        _lightObject.SetActive(false);
    }
}
