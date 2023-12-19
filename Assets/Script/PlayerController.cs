using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Playerの全体の管理を行うクラス</summary>
public class PlayerController : MonoBehaviour
{
    [Header("設定")]

    [SerializeField]
    [Header("本体と幽体の距離制限")]
    float _distanceLimit;

    [SerializeField]
    [Tooltip("本体が起こすActionのオブジェクトLayer")]
    LayerMask _realPlayerSearchLayer;

    [SerializeField]
    [Tooltip("幽体が起こすActionのオブジェクトLayer")]
    LayerMask _astralPlayerSearchLayer;

    [SerializeField]
    [Tooltip("探索範囲")]
    float _sreachRange = 3;

    [SerializeField]
    [Tooltip("幽体が本体の場所に戻るのにかかる時間")]
    float _astralToRealTime = 2;

    [Space]
    [Space]

    [SerializeField]
    [Tooltip("本体のGameObject")]
    GameObject _realGO;

    [SerializeField]
    [Tooltip("幽体のGameObject")]
    GameObject _astralGO;

    [SerializeField]
    [Tooltip("仮想カメラ")]
    CinemachineVirtualCamera _playerVCM;

    /// <summary>現在Actionを起こせるLayer</summary>
    LayerMask _currentSearchLayer;
    /// <summary>移動方向Y</summary>
    float _moveDirY = 0;
    /// <summary>移動方向X</summary>
    float _moveDirX = 0;
    /// <summary>探索に使うRayの向き</summary>
    Vector2 _searchLayDir = Vector2.zero;
    /// <summary>ものの感知用のLayの始点</summary>
    Transform _searchLayOrigin;
    /// <summary>現在のPlayerが本体かどうか</summary>
    bool _isReal;
    /// <summary>現在のState</summary>
    IState _currentState;
    /// <summary>本体のState</summary>
    IState _stateReal;
    /// <summary>幽体のState</summary>
    IState _stateAstral;
    /// <summary>本体と幽体の距離が限界値であるかどうか</summary>
    bool _isBodyDistanceLimit;
    bool _isToRealMoving = false;

    Vector2 _savePos;
    void Start()
    {
        _stateReal = _realGO.GetComponent<IState>();
        _stateAstral = _astralGO.GetComponent<IState>();
        _stateReal.OnStart();
        _stateAstral.OnStart();
        _currentSearchLayer = _realPlayerSearchLayer;
        _playerVCM.Follow = _realGO.transform;
        _searchLayOrigin = _realGO.transform;
        _currentState = _stateReal;
        _isReal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isToRealMoving)
        {
            _moveDirX = Input.GetAxisRaw("Horizontal");
            _moveDirY = Input.GetAxisRaw("Vertical");
            if ((_moveDirX != 0 || _moveDirY != 0) && (_moveDirX == 0 || _moveDirY == 0))
            {
                _searchLayDir = new Vector2(_moveDirX, _moveDirY);
            }

            //Playerの前方にActionを起こすものがあったら
            IAction action = Search();
            if (action != null)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    action.Action(GameInfo.Instance);
                }
            }

            _currentState.OnUpdate();

            //本体の時
            if (_isReal)
            {
                _astralGO.transform.position = _realGO.transform.position;
            }
            //幽体の時
            else
            {
                float distance = Vector2.Distance(_astralGO.transform.position, _realGO.transform.position);
                if (!_isBodyDistanceLimit && distance > _distanceLimit)
                {
                    _savePos = _astralGO.transform.position;
                    _isBodyDistanceLimit = true;
                }
                else if (_isBodyDistanceLimit && distance <= _distanceLimit)
                {
                    _isBodyDistanceLimit = false;
                }

                if (_isBodyDistanceLimit)
                {
                    _astralGO.transform.position = _savePos;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RealAstralBodyChange();
            }
        }
    }

    private void FixedUpdate()
    { 
        _currentState.OnFixedUpdate();
    }

    /// <summary>幽体と本体の入れ替え</summary>
    void RealAstralBodyChange()
    {
        _currentState.OnExit();
        _isReal = !_isReal;
        if(_isReal)
        {
            _isToRealMoving = true;
            _astralGO.transform.DOMove(_realGO.transform.position, _astralToRealTime)
                .OnComplete(() => 
                {
                    _isToRealMoving = false; 
                    _playerVCM.Follow = _realGO.transform; 
                });
        }
        else
        {
            _playerVCM.Follow = _astralGO.transform;
        }
        _currentSearchLayer = _isReal ? _realPlayerSearchLayer : _astralPlayerSearchLayer;
        _searchLayOrigin = _isReal ? _realGO.transform : _astralGO.transform;
        _currentState = _isReal ? _stateReal : _stateAstral;
        _currentState.OnEnter();
    }

    /// <summary>探索した時のActionを起こせるものを探す</summary>
    /// <returns>起こすAction</returns>
    public IAction Search()
    {
        RaycastHit2D hit = Physics2D.Raycast(_searchLayOrigin.position, _searchLayDir * _sreachRange, 1, _currentSearchLayer);
        Debug.DrawRay(_searchLayOrigin.position, _searchLayDir * _sreachRange, Color.black, 0.5f);
        if(hit.collider == null )
        {
            return null;
        }
        return hit.collider.gameObject.GetComponent<IAction>();
    }
}
