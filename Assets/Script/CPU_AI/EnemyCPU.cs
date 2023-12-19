using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 自作ライブラリ
using SLib.StateSequencer;
using SLib.AI;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary> 敵AIの機能を提供する </summary>
public class EnemyCPU : MonoBehaviour
{
    // レイヤーマスクで プレイヤーの検知 Physics.CheckSphere() を使う
    [SerializeField,
        Header("パトロールの道筋のオブジェクトをアタッチ")]
    PathHolder _pRoot;
    [SerializeField,
        Header("移動速度")]
    float _mSpeed = 3;
    [SerializeField,
        Header("プレイヤーのレイヤー")]
    LayerMask _pLayer;
    [SerializeField,
        Header("プレイヤー")]
    GameObject _player;

    StateSequencer _sSeqencer = new StateSequencer();   // ステートマシン
    StatePatroll _patroll;  //  パトロールステート
    StateDetectPlayer _detect;      // 確保ステート

    Rigidbody2D _rb2d;
    SpriteRenderer _sRenderer;

    bool _isDetectedPlayer;

    #region TransitionNames
    const string PatrollToDetect = "DetectP";
    #endregion

    void Hoge(string s) { }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sRenderer = GetComponent<SpriteRenderer>();

        _patroll = new StatePatroll(transform, _pRoot, _rb2d, _sRenderer, _mSpeed);
        _detect = new StateDetectPlayer(_rb2d);

        // ステート登録
        _sSeqencer.ResistStates(new List<IStateMachineState>() { _patroll, _detect });

        // 遷移登録
        _sSeqencer.MakeTransition(_patroll, _detect, PatrollToDetect);

        // ダミーの登録 （Null参照を防ぐ）
        _sSeqencer.OnEntered += Hoge;
        _sSeqencer.OnUpdated += Hoge;
        _sSeqencer.OnExited += Hoge;

        // 起動
        _sSeqencer.InitStateMachine();
    }

    private void FixedUpdate()
    {
        // 遷移条件評価

        // 各ステート各自の更新処理
        _patroll.FramesTask(transform);

        // ステートシーケンサーの各遷移の評価と遷移更新
        _sSeqencer.UpdateTransition(PatrollToDetect, ref _isDetectedPlayer, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _player.layer)
        {
            _isDetectedPlayer = true;
            GameInfo.Instance.GameManager.SceneChange(GameManager.SceneState.GameOver);
            //collision.gameObject.SetActive(false);
        }
    }
}
