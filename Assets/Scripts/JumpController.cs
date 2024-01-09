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
    Rigidbody _rb = default;
    [SerializeField, Tooltip("ジャンプ時の計算で使う")] float _jumpPower = 5;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] bool _isJump = false;
    [Tooltip("アニメーター")] Animator _animator = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //接地フラグが真かつ、ポーズ中でなければ
        if (_isJump && !GM.Instance._isPause)
        {
            if (Input.GetButtonDown("Jump"))
            {
                EffectController.Instance.SePlay(EffectController.SeClass.SE.Jump);
                _animator.SetTrigger("JumpTri");
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
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
