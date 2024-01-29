using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アイテムや敵キャラなどの大量生成する
/// 格納する関数をコライダーつきのオブジェクト側で呼ぶ
/// 生成のインターバルを各アイテムや敵の種類の数だけ調整しているため、
/// その分だけこれをアタッチする必要がある
/// </summary>
public class ObjectPoolItem : MonoBehaviour
{
    [SerializeField, Tooltip("生成したいプレハブ")] GameObject _prefab = default;
    [Tooltip("生成したプレハブを格納するQueue")] public Queue<GameObject> PrefabQueue;
    [SerializeField, Tooltip("生成しておく数")] int _maxCount = 10;
    //[SerializeField, Tooltip("空の親オブジェクト")] GameObject _parentObject = default;

    [SerializeField, Tooltip("スポーン場所の親オブジェクト")] GameObject _spawnsParent;
    [SerializeField, Tooltip("スポーン場所")] GameObject[] _spawns = new GameObject[5];
    [SerializeField] float _intervalMin = 0;
    [SerializeField] float _intervalMax = 0;
    [SerializeField] float _interval = 0;
    [SerializeField] float _intervalTimer = 0;
    [SerializeField, Tooltip("タグの名前")] string _tagName;

    /// <summary>
    /// 事前に一定量を生成し、非アクティブしたあと格納しておく
    /// </summary>
    void Start()
    {
        PrefabQueue = new Queue<GameObject>();
        _tagName = gameObject.name;
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_prefab, gameObject.transform);
            go.tag = _tagName; //生成したギミックのみCollectできるように
            //Queueに追加 
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
        //InGame中かつ、ポーズ画面でないときに実行
        if (GM.Instance._inGame && !GM.Instance._isPause) DoSpawn();
    }

    /// <summary>
    /// 生成するたびに設定した範囲内でランダムに、インターバルを再設定する
    /// スポーンする位置が左真ん中右（LMR）の３つ
    /// LMRのインデックス番号もランダムにする
    /// スポーンするアイテムや敵同士が重ならないように、GMでフラグ管理Falseのときにその位置へ生成する
    /// ※地面のプレハブの子オブジェクトであるものとの、重なりへの対応は考えていない
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
