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
    [SerializeField, Tooltip("ジャンプ時に少し加速")] float _onJumpSpeed = 1.2f;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] bool _isJump = false;
    [SerializeField, Tooltip("Jump時のSE")] AudioSource _audioSource = null;
    [Tooltip("アニメーター")] Animator _animator = null;
    [SerializeField] MovePlayer _movePlayer = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_audioSource) _audioSource.PlayOneShot(_audioSource.clip);
                _animator.SetBool("Jump", true);
                //_movePlayer.ChangeMoveSpeed(_onJumpSpeed); 
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _isJump = false;
            }
        }
    }

    void FixedUpdate()
    {

    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
    }

    /// <summary>
    /// アニメーションイベントで使用する
    /// </summary>
    void ToFalseJumpAnim()
    {
        _animator.SetBool("Jump", false);
    }
}
