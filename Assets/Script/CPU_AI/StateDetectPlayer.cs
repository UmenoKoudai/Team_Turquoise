using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// ©ìƒ‰ƒCƒuƒ‰ƒŠ
using SLib.StateSequencer;
public class StateDetectPlayer : IStateMachineState
{
    Rigidbody2D _rb2d;

    public StateDetectPlayer(Rigidbody2D rb2d)
    {
        _rb2d = rb2d;
    }
   
    public void Entry()
    {
        _rb2d.Sleep();
        GameInfo.Instance.GameManager.SceneChange(GameManager.SceneState.GameOver);
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
