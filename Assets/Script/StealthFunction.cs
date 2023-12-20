using UnityEngine;

/// <summary>
/// ステルス機能
/// ・プレイヤーのアルファ値を下げる
/// ・レイヤーを切り替える（当たり判定無効化）
/// ステルス機能の切り替えは、「_isStealth」で行っている
/// 
/// ※設定すること
/// 　　・_targetにプレイヤーをアタッチする
/// 　　・UIに「UI」というオブジェクトをアタッチする
///     ・「_player.layer = 15;」このコードにより、
/// 　　レイヤーの15番目に「Stealth」（名前は任意）を設定し、
/// 　　PhysicsのLayerCollisionMatrixを変更す必要がある
/// </summary>
public class StealthFunction : MonoBehaviour, IAction
{
    [Tooltip("ステルス状態")] bool _isStealth = false;
    [SerializeField, Tooltip("ステルスする対象")] PlayerRealControlle _target = default;
    [Tooltip("ステルスする対象のImage")] SpriteRenderer _spriteRenderer = default;
    [Tooltip("色の初期の情報")] Color _color = default;
    [SerializeField, Tooltip("ステルス場所に表示するUI")] GameObject _ui = default;
    [SerializeField, Tooltip("ステルス時の透明度")][Range(0.0f, 1.0f)] float _alpha = 0.1f;
    [SerializeField, Tooltip("UIのY座標（対象を基準とする）")][Range(0.0f, 3.0f)] float _addY = 2.0f;

    void Start()
    {
        _spriteRenderer = _target.GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        _ui.SetActive(false);
    }

    public void Action(GameInfo info)
    {
        Debug.Log("呼んだ");
        if(_target == null)
        {
            _target = info.PlayerReal;
        }
        _target.HadeAction();
        Stealth(_target.gameObject);
    }

    /// <summary>
    /// アルファ値を下げて透明度を上げる
    /// レイヤーを変更
    /// </summary>
    void Stealth(GameObject target)
    {
        _isStealth = !_isStealth;
        if (_isStealth)
        {
            // 透明度
            float color_alpha = _alpha;
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, color_alpha);
            // レイヤー
            target.layer = 15; // Stealth
            Debug.Log("Now LayerName : " + LayerMask.LayerToName(target.layer));
            // UIの表示位置
            _ui.transform.position = _target.transform.position + new Vector3(0, _addY, 0);
            _ui.SetActive(true);
        }
        else
        {
            // 初期に戻す 
            float color_alpha = 1.0f;
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, color_alpha);
            target.layer = 6;
            _ui.SetActive(false);
        }
        AudioController.Instance.SePlay(AudioController.SeClass.SE.AstralChange);
    }
}