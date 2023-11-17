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
    [SerializeField, Tooltip("���E�ړ��̒x������")] float _wfs = 0.5f;
    [SerializeField, Tooltip("���E�ړ�")] bool _isTransform;

    void Start()
    {
        _count = 0;
        Speed = _defaultSpeed;
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
    /// �W�����v����A�����A�C�e�������g�p����
    /// </summary>
    /// <param name="addSpeed">��j1.5�Ȃ�</param>
    public void ChangeMoveSpeed(float addSpeed)
    {
        Speed *= addSpeed;
    }

    /// <summary>
    /// �A�j���[�V�����̃C�x���g�g���K�[�Ŏg��
    /// </summary>
    public void ResetSpeed()
    {
        Speed = _defaultSpeed;
    }

    /// <summary>
    /// �������������т�h��
    /// �኱�̍��E�ړ��̒x�����N����
    /// </summary>
    IEnumerator MoveControll()
    {
        _isTransform = false;
        yield return new WaitForSeconds(_wfs);
        _isTransform = true;
    }
}
