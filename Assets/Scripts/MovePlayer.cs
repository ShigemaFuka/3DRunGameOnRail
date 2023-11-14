using UnityEngine;

/// <summary>
/// Z�������ɂ܂������ړ�����
/// �J�[�\���L�[�ō��E�Ɉړ�
/// ���R���[����œ����悤�Ɍ���������
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed = 2;
    [SerializeField] float _speedX = 2;
    [SerializeField] float _x;
    Rigidbody _rb;
    [Space]
    [Header("�ȉ���bool�̂ǂ��炩��^�ɂ���ƁA���E�ړ��\�ɂȂ�")]
    [SerializeField, Tooltip("GetAxis�ō��E�ړ���")] bool _isGetAxis;
    [SerializeField, Tooltip("Transform.x�ō��E�ړ���")] bool _isTransform;
    [Tooltip("�ʒu�ύX��K��������")] Vector3 _changedPos;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (_isGetAxis)
        {
            _rb.velocity = new Vector2(h * _speedX, _rb.velocity.y);
        }
        else if (_isTransform)
        {
            //�E����
            if (h > 0)
            {
                _changedPos = new Vector3(_x, gameObject.transform.position.y);
                //_changedPos.x = _x;
                gameObject.transform.position = _changedPos;
            }
            else if (h < 0)
            {
                _changedPos = new Vector3(-_x, gameObject.transform.position.y);
                //_changedPos.x = - _x;
                gameObject.transform.position = _changedPos;
            }
        }
    }
}
