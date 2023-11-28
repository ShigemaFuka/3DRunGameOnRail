using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アイテムや敵キャラなどの大量生成する
/// 格納する関数をコライダーつきのオブジェクト側で呼ぶ
/// </summary>
public class ObjectPoolItem : MonoBehaviour
{
    [SerializeField, Tooltip("生成したいプレハブ")] GameObject _prefab = default;
    [Tooltip("生成したプレハブを格納するQueue")] public Queue<GameObject> PrefabQueue;
    [SerializeField, Tooltip("生成しておく数")] int _maxCount = 10;
    [SerializeField, Tooltip("空の親オブジェクト")] GameObject _parentObject = default;

    [SerializeField, Tooltip("スポーン場所")] GameObject[] _spawns = new GameObject[3];
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _intervalTimer = 0;

    /// <summary>
    /// 事前に一定量を生成し、非アクティブしたあと格納しておく
    /// </summary>
    void Start()
    {
        PrefabQueue = new Queue<GameObject>();
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_prefab, _parentObject.transform);
            go.tag = "SpawnGimmick"; //生成したギミックのみCollectできるように
            //Queueに追加 
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
    /// queueへ格納する
    /// 格納時に非アクティブにする
    /// </summary>
    /// <param name="go">対象のオブジェクト</param>
    public void Collect(GameObject go)
    {
        go.gameObject.SetActive(false);
        //Queueに格納
        PrefabQueue.Enqueue(go);
    }

    /// <summary>
    /// Nullでなければqueueから取り出す
    /// </summary>
    /// <returns>取り出したオブジェクトかNull</returns>
    public GameObject Launch()
    {
        //Queueが空ならnull
        if (PrefabQueue.Count <= 0) return null;
        //Queueからオブジェクトを一つ取り出す
        GameObject go = PrefabQueue.Dequeue();
        //オブジェクトを表示する
        go.gameObject.SetActive(true);
        //呼び出し元に渡す
        return go;
    }

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
                if(go) go.transform.position = _spawns[spawnIndex].transform.position;
                GM.Instance._isSpawn[spawnIndex] = true;
            }
            _interval = Random.Range(_intervalMin, _intervalMax);
            _intervalTimer = 0;
        }
    }
}
