using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�C�e����G�L�����Ȃǂ̑�ʐ�������
/// �i�[����֐����J�������ŌĂ�
/// �@��苗�����ꂽ��OR�R���C�_�[�ɐG�ꂽ��ĂԂȂ�
/// </summary>
public class ObjectPoolItem : MonoBehaviour
{
    [SerializeField, Tooltip("�����������v���n�u")] GameObject _prefab = default;
    //[SerializeField] List<Queue<GameObject>> Queues = new List<Queue<GameObject>>();
    //[Tooltip("���������v���n�u���i�[����Queue")] static Queue<GameObject> _prefabQueue;
    public Queue<GameObject> PrefabQueue; //{ get => _prefabQueue; set => _prefabQueue = value; }
    [SerializeField, Tooltip("�������Ă�����")] int _maxCount = 10;
    [SerializeField, Tooltip("��̐e�I�u�W�F�N�g")] GameObject _parentObject = default;


    [SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[3];
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _intervalTimer = 0;

    /// <summary>
    /// ���O�Ɉ��ʂ𐶐����A��A�N�e�B�u�������Ɗi�[���Ă���
    /// </summary>
    void Start()
    {
        PrefabQueue = new Queue<GameObject>();
        //Queues.Add(PrefabQueue);
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_prefab, _parentObject.transform);
            go.tag = "SpawnGimmick"; //���������M�~�b�N�̂�Collect�ł���悤��
            //Queue�ɒǉ� 
            PrefabQueue.Enqueue(go);
            Debug.Log("go: " + go);
            go.SetActive(false);
        }

        _interval = Random.Range(_intervalMin, _intervalMax);
        for (var i = 0; i < _spawns.Length; i++)
        {
            _spawns[i] = transform.GetChild(i).gameObject;
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


    //[SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[3];
    ////[SerializeField] GameObject _prefab;
    //[SerializeField] float _intervalMin = 0;
    //[SerializeField] float _intervalMax = 0;
    //[SerializeField] float _interval = 0;
    //[SerializeField] float _intervalTimer = 0;

    void Update()
    {
        if (GM.Instance._inGame) DoSpawn();
    }

    void DoSpawn()
    {
        _intervalTimer += Time.deltaTime;
        if (_intervalTimer >= _interval)
        {
            int spawnIndex = Random.Range(0, _spawns.Length);
            if (GM.Instance._isSpawn[spawnIndex] == false)
            {
                var go = Launch();
                go.transform.position = _spawns[spawnIndex].transform.position;
                GM.Instance._isSpawn[spawnIndex] = true;
            }
            _interval = Random.Range(_intervalMin, _intervalMax);
            _intervalTimer = 0;
        }
    }
}
