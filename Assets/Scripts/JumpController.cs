using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody _rb = default;
    [SerializeField, Tooltip("�W�����v���̌v�Z�Ŏg��")] float _jumpPower = 5;
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump = false;
    [SerializeField, Tooltip("Jump����SE")] AudioSource _audioSource = null;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if(_audioSource) _audioSource.PlayOneShot(_audioSource.clip);
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _isJump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
    }
}
