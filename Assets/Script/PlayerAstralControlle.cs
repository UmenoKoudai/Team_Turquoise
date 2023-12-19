using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
/// <summary>Playerの幽体のコントローラー</summary>
public class PlayerAstralControlle : MonoBehaviour, IState 
{
    [SerializeField]
    [Header("歩行速度")]
    float _moveSpeed;

    BoxCollider2D _collider;
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Animator _anim = null;
    /// <summary>移動方向Y</summary>
    float _moveDirY = 0;
    /// <summary>移動方向X</summary>
    float _moveDirX = 0;
    public void OnStart()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer.sortingOrder = 0;
        _collider.enabled = false;
    }
    public void OnEnter()
    {
        _spriteRenderer.sortingOrder = 1;
        _collider.enabled = true;
    }
    public void OnUpdate()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirY = Input.GetAxisRaw("Vertical");
    }
    public void OnFixedUpdate()
    {
        if (_moveDirX == 0 || _moveDirY == 0)
        {
            _rb.velocity = new Vector2(_moveDirX * _moveSpeed, _moveDirY * _moveSpeed);
        }
    }
    public void OnExit()
    {
        _spriteRenderer.sortingOrder = 0;
        _collider.enabled = false;
        _rb.velocity = Vector2.zero;
    }
}
