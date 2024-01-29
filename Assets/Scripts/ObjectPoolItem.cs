using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�C�e����G�L�����Ȃǂ̑�ʐ�������
/// �i�[����֐����R���C�_�[���̃I�u�W�F�N�g���ŌĂ�
/// �����̃C���^�[�o�����e�A�C�e����G�̎�ނ̐������������Ă��邽�߁A
/// ���̕�����������A�^�b�`����K�v������
/// </summary>
public class ObjectPoolItem : MonoBehaviour
{
    [SerializeField, Tooltip("�����������v���n�u")] GameObject _prefab = default;
    [Tooltip("���������v���n�u���i�[����Queue")] public Queue<GameObject> PrefabQueue;
    [SerializeField, Tooltip("�������Ă�����")] int _maxCount = 10;
    //[SerializeField, Tooltip("��̐e�I�u�W�F�N�g")] GameObject _parentObject = default;

    [SerializeField, Tooltip("�X�|�[���ꏊ�̐e�I�u�W�F�N�g")] GameObject _spawnsParent;
    [SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[5];
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _intervalTimer = 0;
    [SerializeField, Tooltip("�^�O�̖��O")] string _tagName;

    /// <summary>
    /// ���O�Ɉ��ʂ𐶐����A��A�N�e�B�u�������Ɗi�[���Ă���
    /// </summary>
    void Start()
    {
        PrefabQueue = new Queue<GameObject>();
        _tagName = gameObject.name;
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_prefab, gameObject.transform);
            go.tag = _tagName; //���������M�~�b�N�̂�Collect�ł���悤��
            //Queue�ɒǉ� 
            PrefabQueue.Enqueue(go);
            go.SetActive(false);
        }

        _interval = Random.Range(_intervalMin, _intervalMax);
        for (var i = 0; i < _spawns.Length; i++)
        {
            _spawns[i] = _spawnsParent.transform.GetChild(i).gameObject;
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
        PrefabQueue.Enqueue(go);
    }

    /// <summary>
    /// Null�łȂ����queue������o��
    /// </summary>
    /// <returns>���o�����I�u�W�F�N�g��Null</returns>
    public GameObject Launch()
    {
        //Queue����Ȃ�null
        if (PrefabQueue.Count <= 0) return null;
        //Queue����I�u�W�F�N�g������o��
        GameObject go = PrefabQueue.Dequeue();
        //�I�u�W�F�N�g��\������
        go.gameObject.SetActive(true);
        //�Ăяo�����ɓn��
        return go;
    }

    void Update()
    {
        //InGame�����A�|�[�Y��ʂłȂ��Ƃ��Ɏ��s
        if (GM.Instance._inGame && !GM.Instance._isPause) DoSpawn();
    }

    /// <summary>
    /// �������邽�тɐݒ肵���͈͓��Ń����_���ɁA�C���^�[�o�����Đݒ肷��
    /// �X�|�[������ʒu�����^�񒆉E�iLMR�j�̂R��
    /// LMR�̃C���f�b�N�X�ԍ��������_���ɂ���
    /// �X�|�[������A�C�e����G���m���d�Ȃ�Ȃ��悤�ɁAGM�Ńt���O�Ǘ�False�̂Ƃ��ɂ��̈ʒu�֐�������
    /// ���n�ʂ̃v���n�u�̎q�I�u�W�F�N�g�ł�����̂Ƃ́A�d�Ȃ�ւ̑Ή��͍l���Ă��Ȃ�
    /// </summary>
    void DoSpawn()
    {
        _intervalTimer += Time.deltaTime;
        int spawnIndex = Random.Range(0, _spawns.Length);
        if (_intervalTimer >= _interval)
        {
            if (GM.Instance._isSpawn[spawnIndex] == false)
            {
                var go = Launch();
                if (go) go.transform.position = _spawns[spawnIndex].transform.position;
                GM.Instance._isSpawn[spawnIndex] = true;
                _interval = Random.Range(_intervalMin, _intervalMax);
                _intervalTimer = 0;
            }
        }
    }
}
