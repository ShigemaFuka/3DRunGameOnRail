using UnityEngine;

/// <summary>
/// ジャンプのみを管理する
/// 一段階ジャンプ
/// ジャンプ時にSE・アニメーションを再生
/// スペースキー押下でジャンプ
/// 地面に再接触したときに、再度ジャンプできるようにしている
/// 接触判定は本体のコライダーでない、足元に設置したisTriggerが真のコライダー
/// </summary>
public class JumpController : MonoBehaviour
{
    [SerializeField, Tooltip("ジャンプ時の計算で使う")] private float _jumpPower = 5;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] private bool _isJump = false;
    private Rigidbody _rb = default;
    private Animator _animator = null;
    private static readonly int JumpTri = Animator.StringToHash("JumpTri");

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //接地フラグが真かつ、ポーズ中でなければ
        //if (_isJump && !GM.Instance._isPause)
        if (_isJump && GM.Instance.NowState == GM.GameState.InGame)
        {
            if (Input.GetButtonDown("Jump"))
            {
                EffectController.Instance.SePlay(EffectController.SeClass.SE.Jump);
                _animator.SetTrigger(JumpTri);
                Vector3 velocity = _rb.velocity;
                velocity.y = _jumpPower;
                _rb.velocity = velocity;

                // _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _isJump = false;
            }
        }

    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground") || coll.gameObject.name == "Board")
        {
            _isJump = true;
        }
    }
}
