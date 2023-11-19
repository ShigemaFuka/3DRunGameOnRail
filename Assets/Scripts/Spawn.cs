using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ３箇所（LMR）から、アイテム・障害物をランダムに生成する
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("スポーン場所")] GameObject[] _spawn = new GameObject[3];
    GameObject _leftSpawn;
    GameObject _middleSpawn;
    GameObject _rightSpawn;
    [SerializeField] List<GameObject> _list = null;
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _time = 0;

    void Start()
    {
        _leftSpawn = _spawn[0];
        _middleSpawn = _spawn[1];
        _rightSpawn = _spawn[2];
        _interval = Random.Range(_intervalMin, _intervalMax);
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _interval)
        {
            int spawnIndex = Random.Range(0, _spawn.Length);
            int listIndex = Random.Range(0, _list.Count);
            //Instantiate(_list[listIndex], _spawn[spawnIndex].transform);
            Instantiate(_list[listIndex], _spawn[spawnIndex].transform.position, Quaternion.identity);
            _interval = Random.Range(_intervalMin, _intervalMax);
            _time = 0;
        }
    }
}
