using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// ©ìƒ‰ƒCƒuƒ‰ƒŠ
using SLib.StateSequencer;
public class StateDetectPlayer : IStateMachineState
{
    Rigidbody2D _rb2d;
    Animator _anim;

    public StateDetectPlayer(Rigidbody2D rb2d, Animator anim)
    {
        _rb2d = rb2d;
        _anim = anim;
    }
   
    public void Entry()
    {
        _rb2d.Sleep();
        _anim.SetTrigger("Attack");
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}
