using System.Collections;
using UnityEngine;

/// <summary>
/// Z�������ɂ܂������ړ�����
/// �J�[�\���L�[�ō��E�Ɉړ�
/// ���R���[����œ����悤�Ɍ���������
/// �W�����v����Z���ړ����x�������邽�߁A���̂Ƃ�����������������悤�ɁA�֐���p��
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("�O���ƍ��E�̈ړ�����")] bool _isMove;
    //[SerializeField] float _speed = 2;
    [SerializeField, Tooltip("�f�t�H���g�̃X�s�[�h")] float _defaultSpeed = 0;
    [Tooltip("���݂̃X�R�A")] static float _speed = 0;
    public float Speed { get => _speed; set => _speed = value; }
    [SerializeField, Tooltip("X�������ւ̈ړ��͈�")] float _x = 3;
    [Tooltip("�ʒu�ύX��K��������")] Vector3 _changedPos;
    int _count;

    //[SerializeField, Tooltip("���E�ړ��̒x������")] float _wfs = 0.5f;
    readonly WaitForSeconds _wfs = new WaitForSeconds(0.5f);
    [SerializeField, Tooltip("���E�ړ�")] bool _isTransform;
    public bool _isResetSpeed;
    [SerializeField, Tooltip("���x��߂��܂ł̎��Ԃ̌o��")] public float _timer;
    [SerializeField, Tooltip("���x��߂��܂ł̎���")] float _resetTime = 5f;

    void Start()
    {
        _count = 0;
        Speed = _defaultSpeed;
        _isResetSpeed = false;
        _timer = 0;
    }

    /// <summary>
    /// �R���[�`���𔲂���O�Ɏc�@���[���ɂȂ����Ƃ��AFalse�̂܂܂�����
    /// </summary>
    void OnEnable()
    {
        _isTransform = true;
    }

    void FixedUpdate()
    {
        if (_isMove) transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }

    void Update()
    {
        if (_isMove)
        {
            if (_isTransform)
                Move();
        }
        //��莞�Ԍo�߂�����A���x��߂�
        if (Speed != _defaultSpeed)
        {
            _timer += Time.deltaTime;
            if (_timer >= _resetTime)
            {
                Speed = _defaultSpeed;
                _timer = 0;
            }
        }
    }

    /// <summary>
    /// �J�[�\���L�[�ō��E�ړ�
    /// </summary>
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_count < 1)
            {
                _count++;
                StartCoroutine(MoveControll());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_count > -1)
            {
                _count--;
                StartCoroutine(MoveControll());
            }
        }
        if (_count == 1)
            _changedPos = new Vector3(_x, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == 0)
            _changedPos = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == -1)
            _changedPos = new Vector3(-_x, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = _changedPos;
    }

    /// <summary>
    /// �������������т�h��
    /// �኱�̍��E�ړ��̒x�����N����
    /// </summary>
    IEnumerator MoveControll()
    {
        _isTransform = false;
        yield return _wfs;
        _isTransform = true;
    }
}
