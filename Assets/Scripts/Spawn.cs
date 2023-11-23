using UnityEngine;

/// <summary>
/// �R�ӏ��iLMR�j���烉���_���ɐ�������
/// �������邽�тɐ����܂ł̃C���^�[�o�����A�͈͓��ŕύX����
/// �����ӏ��ɏd�Ȃ�ȃM�~�b�N�𐶐����Ȃ��悤�ɁAGM�̃t���O�Ő����𐧌�
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[3];
    [SerializeField] GameObject _prefab;
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _time = 0;

    void Start()
    {
        _interval = Random.Range(_intervalMin, _intervalMax);
        for (var i = 0; i < _spawns.Length; i++)
        {
            _spawns[i] = transform.GetChild(i).gameObject;
        }        
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _interval)
        {
            int spawnIndex = Random.Range(0, _spawns.Length);
            if (GM.Instance._isSpawn[spawnIndex] == false)
            {
                Instantiate(_prefab, _spawns[spawnIndex].transform.position, Quaternion.identity);
                GM.Instance._isSpawn[spawnIndex] = true;
            }
            _interval = Random.Range(_intervalMin, _intervalMax);
            _time = 0;
        }
    }
}
