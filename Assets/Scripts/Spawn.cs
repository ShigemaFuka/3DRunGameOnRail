using UnityEngine;

/// <summary>
/// �R�ӏ��iLMR�j���烉���_���ɐ�������
/// �������邽�тɐ����܂ł̃C���^�[�o�����A�͈͓��ŕύX����
/// �����ӏ��ɏd�Ȃ�ȃM�~�b�N�𐶐����Ȃ��悤�ɁAGM�̃t���O�Ő����𐧌�
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[3];
    //[SerializeField] GameObject _prefab;
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _intervalTimer = 0;

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
                //var go = Launch();
                //go.transform.position = _spawns[spawnIndex].transform.position;
                GM.Instance._isSpawn[spawnIndex] = true;
            }
            _interval = Random.Range(_intervalMin, _intervalMax);
            _intervalTimer = 0;
        }
    }

    //public override void ActionOnStart()
    //{
    //    _interval = Random.Range(_intervalMin, _intervalMax);
    //    for (var i = 0; i < _spawns.Length; i++)
    //    {
    //        _spawns[i] = transform.GetChild(i).gameObject;
    //    }
    //}
}
