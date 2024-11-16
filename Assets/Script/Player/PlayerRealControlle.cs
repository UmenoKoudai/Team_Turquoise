using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
/// <summary>Playerの本体のコントローラー</summary>
public class PlayerRealControlle : MonoBehaviour, IState
{
    [SerializeField]
    [Header("歩行速度")]
    float _moveSpeed;

    SpriteRenderer _spriteRenderer;
    CapsuleCollider2D _collider;
    Rigidbody2D _rb;
    Animator _anim = null;
    AudioSource _audioSource;
    /// <summary>移動方向Y</summary>
    float _moveDirY = 0;
    /// <summary>移動方向X</summary>
    float _moveDirX = 0;
    /// <summary>物陰に隠れているかどうか</summary>
    bool _isHide = false;

    public bool IsHide => _isHide;
    public void OnStart()
    {
        GameInfo.Instance.PlayerReal = this;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer.sortingOrder = 1;
        _anim.SetFloat("move_x", 0);
        _anim.SetFloat("move_y", -1);
    }

    public void OnEnter()
    {
        _spriteRenderer.sortingOrder = 1;
        _anim.SetFloat("move_x", 0);
        _anim.SetFloat("move_y", -1);
        _collider.enabled = true;
    }
    public void OnUpdate()
    {
        if(!_isHide)
        {
            _moveDirX = Input.GetAxisRaw("Horizontal");
            _moveDirY = Input.GetAxisRaw("Vertical");
            if (_moveDirX != 0 || _moveDirY != 0)
            {
                _anim.SetFloat("move_x", _moveDirX);
                _anim.SetFloat("move_y", _moveDirY);
                if(!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            
            if(_moveDirX == 0 && _moveDirY == 0)
            {
                _audioSource.Stop();
            }
            
            _anim.SetFloat("walk", _rb.velocity.magnitude);
        }
    }
    public void OnFixedUpdate()
    {
        if(!_isHide)
        {
            if (_moveDirX == 0 || _moveDirY == 0)
            {
                _rb.velocity = new Vector2(_moveDirX * _moveSpeed, _moveDirY * _moveSpeed);
            }
        }
    }

    public void OnExit()
    {
        _audioSource.Stop();
        _spriteRenderer.sortingOrder = 0;
        _collider.enabled = false;
        _anim.SetFloat("move_x", 0);
        _anim.SetFloat("move_y", -1);
        _rb.velocity = Vector2.zero;
        _anim.SetFloat("walk", _rb.velocity.magnitude);
    }
    public void HadeAction()
    {
        _isHide = !_isHide;
        _collider.isTrigger = _isHide;
        _rb.velocity = Vector2.zero;
    }
}

    
