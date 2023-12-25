using UnityEngine;
using DG.Tweening;
/// <summary>Playerの幽体のコントローラー</summary>
public class PlayerAstralControlle : MonoBehaviour, IState 
{
    [SerializeField]
    [Header("歩行速度")]
    float _moveSpeed;

    [SerializeField]
    [Header("Idle状態の上下の動き幅")]
    float _moveYRange;

    CapsuleCollider2D _collider;
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Animator _anim = null;
    Transform _bodyPos;
    /// <summary>移動方向Y</summary>
    float _moveDirY = 0;
    /// <summary>移動方向X</summary>
    float _moveDirX = 0;

    public void OnStart()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CapsuleCollider2D>();
        _bodyPos = transform.GetChild(0).transform;
        _spriteRenderer.sortingOrder = 0;
        SpriteClear(true);
        _collider.enabled = false;
    }
    public void OnEnter()
    {
        SpriteClear(false);
        _anim.SetFloat("move_x", 0);
        _anim.SetFloat("move_y", -1);
        _collider.enabled = true;
        _bodyPos.localPosition = Vector3.zero;
    }
    public void OnUpdate()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirY = Input.GetAxisRaw("Vertical");
        if (_moveDirX != 0 || _moveDirY != 0)
        {
            _anim.SetFloat("move_x", _moveDirX);
            _anim.SetFloat("move_y", _moveDirY);
        }
        _anim.SetFloat("walk", _rb.velocity.magnitude);
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
        _anim.SetFloat("move_x", 0);
        _anim.SetFloat("move_y", -1);
        _collider.enabled = false;
        _rb.velocity = Vector2.zero;
        _bodyPos.localPosition = Vector3.zero;
        _anim.SetFloat("walk", _rb.velocity.magnitude);
    }

    /// <summary>PlayerのSprite透明化</summary>
    /// <param name="isClear">透明にするかどうか</param>
    public void SpriteClear(bool isClear)
    {
        _spriteRenderer.color = isClear ? Color.clear : Color.gray;
    }
}
