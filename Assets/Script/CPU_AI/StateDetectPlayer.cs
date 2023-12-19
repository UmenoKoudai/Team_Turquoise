using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// ©ìƒ‰ƒCƒuƒ‰ƒŠ
using SLib.StateSequencer;
public class StateDetectPlayer : IState
{
    Rigidbody2D _rb2d;

    public StateDetectPlayer(Rigidbody2D rb2d)
    {
        _rb2d = rb2d;
    }
   
    public void Entry()
    {
        Debug.Log("You Are Died");
        _rb2d.Sleep();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
