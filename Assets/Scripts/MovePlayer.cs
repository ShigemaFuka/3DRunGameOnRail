using UnityEngine;

/// <summary>
/// Z�������ɂ܂������ړ�����
/// �J�[�\���L�[�ō��E�Ɉړ�
/// ���R���[����œ����悤�Ɍ���������
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("�O���ƍ��E�̈ړ�����")] bool _isMove;
    [SerializeField] float _speed = 2;
    [SerializeField] float _speedX = 2;
    [SerializeField, Tooltip("X�������ւ̈ړ��͈�")] float _x = 3;
    Rigidbody _rb;
    [Space]
    [Header("�ȉ���bool�̂ǂ��炩��^�ɂ���ƁA���E�ړ��\�ɂȂ�")]
    [SerializeField, Tooltip("���炩")] bool _isGetAxis;
    [SerializeField, Tooltip("�u��")] bool _isTransform;
    [Tooltip("�ʒu�ύX��K��������")] Vector3 _changedPos;
    int _count;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _count = 0;
    }

    void FixedUpdate()
    {
        if (_isMove) transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (_isMove)
        {


            if (_isGetAxis)
            {
                _rb.velocity = new Vector2(h * _speedX, _rb.velocity.y);
            }
            else if (_isTransform)
            {
                //�E����
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (_count < 1)
                        _count++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (_count > -1)
                        _count--;
                }
                if (_count == 1)
                    _changedPos = new Vector3(_x, gameObject.transform.position.y, gameObject.transform.position.z);
                else if (_count == 0)
                    _changedPos = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
                else if (_count == -1)
                    _changedPos = new Vector3(-_x, gameObject.transform.position.y, gameObject.transform.position.z);
                gameObject.transform.position = _changedPos;
            }
        }
    }
}
