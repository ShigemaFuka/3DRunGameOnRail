using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Z�������ɂ܂������ړ�����
/// �J�[�\���L�[�ō��E�Ɉړ�
/// ���R���[����œ����悤�Ɍ���������
/// ���x�㏸����TrailRenderer�ŋO�Ղ�\��
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("�O���ƍ��E�̈ړ�����")] bool _isMove = default;
    [SerializeField, Tooltip("�f�t�H���g�̃X�s�[�h")] float _defaultSpeed = 0f;
    [Tooltip("���݂̃X�s�[�h")] static float _speed = 0f;
    [SerializeField, Tooltip("X�������ւ̈ړ��͈�")] float _x = 3f;
    [Tooltip("�ʒu�ύX��K��������")] Vector3 _changedPos = default;
    [SerializeField] int _count = 0;

    readonly WaitForSeconds _wfs = new WaitForSeconds(0.5f);
    [SerializeField, Tooltip("���E�ړ�")] bool _isTransform = default;
    [Tooltip("���x��߂��܂ł̎��Ԃ̌o��")] public float _timer = 0f;
    [SerializeField, Tooltip("���x��߂��܂ł̎���")] float _resetTime = 1f;
    //[SerializeField, Tooltip("�������Ƃ���̎c��")] TrailRenderer _trailRenderer = default;

    [SerializeField, Header("�����ŗǂ�"), Tooltip("�v���}�C�ł����̈ړ��͈�")] float _xRange = 2.0f;
    [SerializeField, Tooltip("���E�ړ��̃X�s�[�h")] float _xSpeed = 7f;
    [SerializeField, Tooltip("���x�㏸�̏��")] float _maxSpeed = 40f;
    Rigidbody _rb = default;


    #region"�v���p�e�B"
    public float DefaultSpeed { get => _defaultSpeed; }
    public float Speed { get => _speed; set => _speed = value; }

    public float MaxSpeed { get => _maxSpeed; }
    #endregion


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _count = 0;
        Speed = _defaultSpeed;
        _timer = 0;
        _rb = GetComponent<Rigidbody>();
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
        if (GM.Instance._isInvincible)
            gameObject.tag = "Invincible";
        else
            gameObject.tag = "Player";

        if (GM.Instance._isPause)
            _rb.isKinematic = true;
        else _rb.isKinematic = false;

        //������
        if (transform.position.y <= -3)
        {
            GM.Instance.GameOver();
            var pos = transform.position;
            pos.z -= 10f; // ������O�ɖ߂�
            // �ʒu���C��
            transform.position = new Vector3(pos.x, 0, pos.z);
        }

        //�|�[�Y���͍s����~
        _isMove = !GM.Instance._isPause;
        if (_isMove)
        {
            if (_isTransform) Move2();
                //Move();
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
    /// ���W�w��̈ړ��̂��߁A�u�Ԉړ��̂悤�ȋ���
    /// �~�j�I�����b�V���n
    /// </summary>
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (_count < 1)
            {
                _count++;
                StartCoroutine(MoveControll());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
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
    /// �R���[���̔��肪�Ȃ���
    /// �e���v�������n
    /// </summary>
    void Move2()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < _xRange)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _xSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -_xRange)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _xSpeed);
            }
        }
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
