using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Playerの全体の管理を行うクラス</summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Header("距離制限")]
    float _distanceLimit;

    [SerializeField]
    [Tooltip("本体のGameObject")]
    GameObject _realGO;

    [SerializeField]
    [Tooltip("幽体のGameObject")]
    GameObject _astralGO;



    [SerializeField]
    [Tooltip("Playerが調べるもののLayer")]
    LayerMask _searchLayer;
    /// <summary>移動方向Y</summary>
    float _moveDirY = 0;
    /// <summary>移動方向X</summary>
    float _moveDirX = 0;

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

    Vector2 pos;
    bool isLimit;
    void Start()
    {
        _stateReal = _realGO.GetComponent<IState>();
        _stateAstral = _astralGO.GetComponent<IState>();
        _stateReal.OnStart();
        _stateAstral.OnStart();
        _searchLayOrigin = _realGO.transform;
        _currentState = _stateReal;
        _isReal = true;
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirY = Input.GetAxisRaw("Vertical");
        if ((_moveDirX != 0 || _moveDirY != 0) && (_moveDirX == 0 || _moveDirY == 0))
        {
            _searchLayDir = new Vector2(_moveDirX,_moveDirY);
        }
        Search();

        _currentState.OnUpdate();

        //本体の時
        if(_isReal)
        {
            _astralGO.transform.position = _realGO.transform.position;
        }
        //幽体の時
        else
        {
            float distance = Vector2.Distance(_astralGO.transform.position, _realGO.transform.position);
            if (!isLimit && distance > _distanceLimit)
            {
                pos = _astralGO.transform.position;
                isLimit = true;
            }
            else if(isLimit && distance <= _distanceLimit)
            {
                isLimit = false;
            }
            
            if(isLimit)
            {
                _astralGO.transform.position = pos; 
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RealAstralBodyChange();
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
        _searchLayOrigin = _isReal ? _realGO.transform : _astralGO.transform;
        _currentState = _isReal ? _stateReal : _stateAstral;
        _currentState.OnEnter();
    }

    public bool Search()
    {
        RaycastHit2D hit = Physics2D.Raycast(_searchLayOrigin.position, _searchLayDir, 1, _searchLayer);
        Debug.DrawRay(_searchLayOrigin.position, _searchLayDir, Color.black, 0.5f);
        if (hit.collider == null)
        {
            return false;
        }
        return true;
    }
}
