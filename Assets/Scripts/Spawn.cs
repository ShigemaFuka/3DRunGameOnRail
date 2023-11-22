//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

/// <summary>
/// ３箇所（LMR）から、アイテム・障害物をランダムに生成する
/// 生成するたびに生成までのインターバルを、範囲内で変更する
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("スポーン場所")] GameObject[] _spawns = new GameObject[3];
    //[SerializeField] List<GameObject> _list = null;
    [SerializeField] GameObject _prefab;
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _time = 0;
    GameObject _leftSpawn;
    GameObject _middleSpawn;
    GameObject _rightSpawn;
    //[SerializeField, Tooltip("遅延時間")] float _wfs = 0.3f;
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
    /// ギミックの生成場所とタイミングが重ならないように
    /// boolが偽のときにのみLMRそれぞれのスポーン位置から生成する
    /// </summary>
    IEnumerator FlagChange(int index)
    {
        // 直前(_wfs以内)にその位置で生成されていなければ
        if (GM.Instance._isSpawn[index] == false)
        {
            GM.Instance._isSpawn[index] = true;
            yield return _wfs;
            GM.Instance._isSpawn[index] = false;
        }
    }
}
