using UnityEngine;
/// <summary>
/// ƒhƒA‚ÌŒ®‚ðŠJ‚¯‚é
/// </summary>
public class Open : MonoBehaviour, IAction
{
    Animator _openAnimator;
    [SerializeField] float _closingTime;
    float _eTime;
    bool _isOpened = false;
    public void Action(GameInfo info)
    {
        _openAnimator.SetBool("Open", true);
        _isOpened = (!_isOpened) ? true : _isOpened;
    }

    // Start is called before the first frame update
    void Start()
    {
        _openAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _eTime += (_isOpened) ? Time.deltaTime : 0;

        if (_eTime > _closingTime)
        {
            _openAnimator.SetBool("Open", false);
            _isOpened = false;
            _eTime = 0;
        }
    }
}
