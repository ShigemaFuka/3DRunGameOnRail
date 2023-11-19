using UnityEngine;

/// <summary>
/// �W�����v�݂̂��Ǘ�����
/// ��i�K�W�����v
/// �W�����v����SE�E�A�j���[�V�������Đ�
/// �X�y�[�X�L�[�����ŃW�����v
/// �n�ʂɍĐڐG�����Ƃ��ɁA�ēx�W�����v�ł���悤�ɂ��Ă���
/// �ڐG����͖{�̂̃R���C�_�[�łȂ��A�����ɐݒu����isTrigger���^�̃R���C�_�[
/// </summary>
public class JumpController : MonoBehaviour
{
    Rigidbody _rb = default;
    [SerializeField, Tooltip("�W�����v���̌v�Z�Ŏg��")] float _jumpPower = 5;
    [SerializeField, Tooltip("�W�����v���ɏ�������")] float _onJumpSpeed = 1.2f;
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump = false;
    [SerializeField, Tooltip("Jump����SE")] AudioSource _audioSource = null;
    [Tooltip("�A�j���[�^�[")] Animator _animator = null;
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
    /// �A�j���[�V�����C�x���g�Ŏg�p����
    /// </summary>
    void ToFalseJumpAnim()
    {
        _animator.SetBool("Jump", false);
    }
}
