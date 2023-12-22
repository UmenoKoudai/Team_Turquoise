using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 自作ライブラリ
using SLib.StateSequencer;
using SLib.AI;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
/// <summary> 敵AIの機能を提供する </summary>
public class EnemyCPU : MonoBehaviour, IAction
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
    [SerializeField,
        Header("スタン時間")]
    float _stunTime;

    [SerializeField]
    ShowGameOver gameOver;

    StateSequencer _sSeqencer = new StateSequencer();   // ステートマシン
    StatePatroll _patroll;  //  パトロールステート
    StateDetectPlayer _detect;      // 確保ステート

    Rigidbody2D _rb2d;
    Animator _anim;
    SpriteRenderer _sRenderer;

    bool _isDetectedPlayer;
    bool _isStunning;

    #region TransitionNames
    const string PatrollToDetect = "DetectP";
    #endregion

    void Hoge(string s) { }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _sRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _patroll = new StatePatroll(transform, _pRoot, _rb2d, _sRenderer, _mSpeed);
        _detect = new StateDetectPlayer(_rb2d, _anim);

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
            AudioController.Instance.SePlay(AudioController.SeClass.SE.EnemyDiscover);
            gameOver.GameOver();
            _isDetectedPlayer = true;
        }
    }

    IEnumerator OnAttackEnd()  // 攻撃演出が終わったときにAnimatorから呼び出す
    {
        yield return new WaitForSeconds(1.0f);
        GameInfo.Instance.GameManager.SceneChange(GameManager.SceneState.GameOver);
    }

    IEnumerator StunRoutine(float stunningTime)
    {
        _isStunning = true;
        _sSeqencer.FreezStateMachine();
        yield return new WaitForSeconds(stunningTime);
        _isStunning = false;
        _sSeqencer.InitStateMachine();
    }

    public void Action(GameInfo info)   // スタン処理
    {
        StartCoroutine(StunRoutine(_stunTime));
    }
}
