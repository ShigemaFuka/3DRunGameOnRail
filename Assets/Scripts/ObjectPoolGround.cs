using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n�ʂ𖳌��������Ă���悤�Ɍ���������
/// ���O�Ɉ�萔�������Ă����A�i�[�E���o�����J��Ԃ�
/// ���������F�n�ʃv���n�u��Z�������̒��������Ƀv���n�u�̕\���ʒu��ύX���āA�n�ʃv���n�u���s�b�^��������
/// ������ނ̒n�ʂ̃v���n�u�����O�ɐ�������
/// �����̃^�C�~���O�����ł��邽�߁A�����ő���ނ̒n�ʂ̃v���n�u�𐶐�����
/// </summary>
public class ObjectPoolGround : MonoBehaviour
{
    [SerializeField, Tooltip("�����������n�ʂ̃v���n�u")] GameObject[] _groundPrefabs = default;
    [Tooltip("���������n�ʂ��i�[����Queue")] static Queue<GameObject> _groundPrefabQueue;
    public Queue<GameObject> GroundPrefabQueue { get => _groundPrefabQueue; set => _groundPrefabQueue = value; }
    [Header("�A���œ����v���n�u���o�邱�Ƃ�����")]
    [SerializeField, Tooltip("�������Ă����n�ʂ̐�")] int _maxNum = 5;
    [SerializeField, Tooltip("�\�����Ă����n�ʂ̐�")] int _visibleNum = 5;
    [Tooltip("�����_���Œn�ʃ��X�g�̗v�f���w�肷�邽�߂̃C���f�b�N�X")] int _randomPrefabsIndex;
    [SerializeField, Tooltip("��̐e�I�u�W�F�N�g")] GameObject _parentObject = default;
    [Tooltip("�v���n�u�̕\���ʒu")] Vector3 _pos;
    [SerializeField, Tooltip("�\���Ԋu�i�v���n�u��Z�������̒����Ɉˑ��j")] float _addZLength = 20;
    [SerializeField, Tooltip("�v���n�u�̕\���ʒu��Z�̒l")] float _setZLength = 0; // �O�X�^�[�g

    /// <summary>
    /// ���O�ɋK�萔�̃I�u�W�F�N�g�𐶐����āA��A�N�e�B�u�ɂ��ėp�� 
    /// </summary>
    void Start()
    {
        //Queue�̏�����
        GroundPrefabQueue = new Queue<GameObject>();
        for (int i = 0; i < _maxNum; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _groundPrefabs.Length);
            GameObject go = Instantiate(_groundPrefabs[_randomPrefabsIndex], _parentObject.transform);
            //Queue�ɒǉ� 
            GroundPrefabQueue.Enqueue(go);
            go.SetActive(false);
        }
        for (int i = 0; i < _visibleNum; i++)
        {
            Launch();
        }
    }

    /// <summary>
    /// queue�֊i�[����
    /// �i�[���ɔ�A�N�e�B�u�ɂ���
    /// </summary>
    /// <param name="go">�Ώۂ̃I�u�W�F�N�g</param>
    public void Collect(GameObject go)
    {
        go.gameObject.SetActive(false);
        //Queue�Ɋi�[
        GroundPrefabQueue.Enqueue(go);
    }

    /// <summary>
    /// Null�łȂ����queue������o��
    /// </summary>
    /// <returns>���o�����I�u�W�F�N�g��Null</returns>
    public GameObject Launch()
    {
        //Queue����Ȃ�null
        if (GroundPrefabQueue.Count <= 0) return null;
        //Queue����I�u�W�F�N�g������o��
        GameObject go = GroundPrefabQueue.Dequeue();
        //�I�u�W�F�N�g��\������
        go.gameObject.SetActive(true);
        //�����ʒu�̒���
        _pos.z = _setZLength; //z.positon���O�͂��߂ɂ���
        go.transform.position = _pos;
        _setZLength += _addZLength;

        //�Ăяo�����ɓn��
        return go;
    }
}
