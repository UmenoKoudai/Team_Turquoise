using UnityEngine;

/// <summary>
/// �X�e���X�@�\
/// �E�v���C���[�̃A���t�@�l��������
/// �E���C���[��؂�ւ���i�����蔻�薳�����j
/// �X�e���X�@�\�̐؂�ւ��́A�u_isStealth�v�ōs���Ă���
/// 
/// ���ݒ肷�邱��
/// �@�@�E_target�Ƀv���C���[���A�^�b�`����
/// �@�@�EUI�ɁuUI�v�Ƃ����I�u�W�F�N�g���A�^�b�`����
///     �E�u_player.layer = 15;�v���̃R�[�h�ɂ��A
/// �@�@���C���[��15�ԖڂɁuStealth�v�i���O�͔C�Ӂj��ݒ肵�A
/// �@�@Physics��LayerCollisionMatrix��ύX���K�v������
/// </summary>
public class StealthFunction : MonoBehaviour, IAction
{
    [Tooltip("�X�e���X���")] bool _isStealth = false;
    [SerializeField, Tooltip("�X�e���X����Ώ�")] GameObject _target = default;
    [Tooltip("�X�e���X����Ώۂ�Image")] SpriteRenderer _spriteRenderer = default;
    [Tooltip("�F�̏����̏��")] Color _color = default;
    [SerializeField, Tooltip("�X�e���X�ꏊ�ɕ\������UI")] GameObject _ui = default;
    [SerializeField, Tooltip("�X�e���X���̓����x")][Range(0.0f, 1.0f)] float _alpha = 0.1f;
    [SerializeField, Tooltip("UI��Y���W�i�Ώۂ���Ƃ���j")][Range(0.0f, 3.0f)] float _addY = 2.0f;

    void Start()
    {
        _spriteRenderer = _target.GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _ui.SetActive(false);
    }

    public void Action(GameInfo info)
    {
        Stealth();
    }

    /// <summary>
    /// �A���t�@�l�������ē����x���グ��
    /// ���C���[��ύX
    /// </summary>
    void Stealth()
    {
        _isStealth = !_isStealth;
        if (_isStealth)
        {
            // �����x
            float color_alpha = _alpha;
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, color_alpha);
            // ���C���[
            _target.layer = 15; // Stealth
            Debug.Log("Now LayerName : " + LayerMask.LayerToName(_target.layer));
            // UI�̕\���ʒu
            _ui.transform.position = _target.transform.position + new Vector3(0, _addY, 0);
            _ui.SetActive(true);
        }
        else
        {
            // �����ɖ߂� 
            float color_alpha = 1.0f;
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, color_alpha);
            _target.layer = 0;
            _ui.SetActive(false);
        }
    }
}