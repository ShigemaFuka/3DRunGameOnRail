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
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump = false;
    [Tooltip("�A�j���[�^�[")] Animator _animator = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        //�ڒn�t���O���^���A�|�[�Y���łȂ����
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
