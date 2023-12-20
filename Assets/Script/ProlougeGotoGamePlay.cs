using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProlougeGotoGamePlay : MonoBehaviour
{
    [SerializeField]
    UnityEvent EvtOnEnd;

    private void OnEnable()
    {
        EvtOnEnd.Invoke();
    }
}
