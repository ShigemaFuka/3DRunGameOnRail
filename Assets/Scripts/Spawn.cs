using UnityEngine;

/// <summary>
/// ３箇所（LMR）からランダムに生成する
/// 生成するたびに生成までのインターバルを、範囲内で変更する
/// 同じ箇所に重なるなギミックを生成しないように、GMのフラグで生成を制限
/// </summary>
public class Spawn : MonoBehaviour
{
    [SerializeField, Tooltip("スポーン場所")] GameObject[] _spawns = new GameObject[3];
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
