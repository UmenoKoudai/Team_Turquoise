using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���색�C�u����
using SLib.StateSequencer;
using SLib.AI;
using UnityEngine.Playables;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
/// <summary> �GAI�̋@�\��񋟂��� </summary>
public class EnemyCPU : MonoBehaviour, IAction
{
    // ���C���[�}�X�N�� �v���C���[�̌��m Physics.CheckSphere() ���g��
    [SerializeField,
        Header("�p�g���[���̓��؂̃I�u�W�F�N�g���A�^�b�`")]
    PathHolder _pRoot;
    [SerializeField,
        Header("�ړ����x")]
    float _mSpeed = 3;
    [SerializeField,
        Header("�v���C���[�̃��C���[")]
    LayerMask _pLayer;
    [SerializeField,
        Header("�v���C���[")]
    GameObject _player;
    [SerializeField,
        Header("�X�^������")]
    float _stunTime;

    [SerializeField]
    ShowGameOver gameOver;

    StateSequencer _sSeqencer = new StateSequencer();   // �X�e�[�g�}�V��
    StatePatroll _patroll;  //  �p�g���[���X�e�[�g
    StateDetectPlayer _detect;      // �m�ۃX�e�[�g

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

        // �X�e�[�g�o�^
        _sSeqencer.ResistStates(new List<IStateMachineState>() { _patroll, _detect });

        // �J�ړo�^
        _sSeqencer.MakeTransition(_patroll, _detect, PatrollToDetect);

        // �_�~�[�̓o�^ �iNull�Q�Ƃ�h���j
        _sSeqencer.OnEntered += Hoge;
        _sSeqencer.OnUpdated += Hoge;
        _sSeqencer.OnExited += Hoge;

        // �N��
        _sSeqencer.InitStateMachine();
    }

    private void FixedUpdate()
    {
        // �J�ڏ����]��

        // �e�X�e�[�g�e���̍X�V����
        _patroll.FramesTask(transform);

        // �X�e�[�g�V�[�P���T�[�̊e�J�ڂ̕]���ƑJ�ڍX�V
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

    IEnumerator OnAttackEnd()  // �U�����o���I������Ƃ���Animator����Ăяo��
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

    public void Action(GameInfo info)   // �X�^������
    {
        StartCoroutine(StunRoutine(_stunTime));
    }
}
