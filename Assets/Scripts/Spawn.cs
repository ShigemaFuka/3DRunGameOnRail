//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

/// <summary>
/// �R�ӏ��iLMR�j����A�A�C�e���E��Q���������_���ɐ�������
/// �������邽�тɐ����܂ł̃C���^�[�o�����A�͈͓��ŕύX����
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("�X�|�[���ꏊ")] GameObject[] _spawns = new GameObject[3];
    //[SerializeField] List<GameObject> _list = null;
    [SerializeField] GameObject _prefab;
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _time = 0;
    GameObject _leftSpawn;
    GameObject _middleSpawn;
    GameObject _rightSpawn;
    //[SerializeField, Tooltip("�x������")] float _wfs = 0.3f;
    readonly WaitForSeconds _wfs = new WaitForSeconds(0.3f);

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
            //if(GM.Instance._isSpawn[spawnIndex]) //
            //int listIndex = Random.Range(0, _list.Count);
            //Instantiate(_list[listIndex], _spawn[spawnIndex].transform.position, Quaternion.identity);
            if (GM.Instance._isSpawn[spawnIndex] == false)
            {
                Instantiate(_prefab, _spawns[spawnIndex].transform.position, Quaternion.identity);
                GM.Instance._isSpawn[spawnIndex] = true;
            }

            _interval = Random.Range(_intervalMin, _intervalMax);
            _time = 0;
        }
    }

    /// <summary>
    /// �M�~�b�N�̐����ꏊ�ƃ^�C�~���O���d�Ȃ�Ȃ��悤��
    /// bool���U�̂Ƃ��ɂ̂�LMR���ꂼ��̃X�|�[���ʒu���琶������
    /// </summary>
    IEnumerator FlagChange(int index)
    {
        // ���O(_wfs�ȓ�)�ɂ��̈ʒu�Ő�������Ă��Ȃ����
        if (GM.Instance._isSpawn[index] == false)
        {
            GM.Instance._isSpawn[index] = true;
            yield return _wfs;
            GM.Instance._isSpawn[index] = false;
        }
    }
}
