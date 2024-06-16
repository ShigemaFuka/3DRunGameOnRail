﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 地面を無限生成しているように見せかける
/// 事前に一定数生成しておき、格納・取り出しを繰り返す
/// 無限生成：地面プレハブのZ軸方向の長さおきにプレハブの表示位置を変更して、地面プレハブをピッタリ揃える
/// 複数種類の地面のプレハブを事前に生成する
/// 生成のタイミングが一定であるため、これ一つで多種類の地面のプレハブを生成する
/// </summary>
public class ObjectPoolGround : MonoBehaviour
{
    [SerializeField, Tooltip("生成したい地面のプレハブ")] GameObject[] _groundPrefabs = default;
    [Tooltip("生成した地面を格納するQueue")] static Queue<GameObject> _groundPrefabQueue;
    public Queue<GameObject> GroundPrefabQueue { get => _groundPrefabQueue; set => _groundPrefabQueue = value; }
    [Header("連続で同じプレハブが出ることもある")]
    [SerializeField, Tooltip("生成しておく地面の数")] int _maxNum = 5;
    [SerializeField, Tooltip("表示しておく地面の数")] int _visibleNum = 5;
    [Tooltip("ランダムで地面リストの要素を指定するためのインデックス")] int _randomPrefabsIndex;
    [SerializeField, Tooltip("空の親オブジェクト")] GameObject _parentObject = default;
    [Tooltip("プレハブの表示位置")] Vector3 _pos;
    [SerializeField, Tooltip("表示間隔（プレハブのZ軸方向の長さに依存）")] float _addZLength = 20;
    [SerializeField, Tooltip("プレハブの表示位置のZの値")] float _setZLength = 0; // ０スタート

    /// <summary>
    /// 事前に規定数のオブジェクトを生成して、非アクティブにして用意 
    /// </summary>
    void Start()
    {
        //Queueの初期化
        GroundPrefabQueue = new Queue<GameObject>();
        for (int i = 0; i < _maxNum; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _groundPrefabs.Length);
            GameObject go = Instantiate(_groundPrefabs[_randomPrefabsIndex], _parentObject.transform);
            //Queueに追加 
            GroundPrefabQueue.Enqueue(go);
            go.SetActive(false);
        }
        for (int i = 0; i < _visibleNum; i++)
        {
            Launch();
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
        GroundPrefabQueue.Enqueue(go);
    }

    /// <summary>
    /// Nullでなければqueueから取り出す
    /// </summary>
    /// <returns>取り出したオブジェクトかNull</returns>
    public GameObject Launch()
    {
        //Queueが空ならnull
        if (GroundPrefabQueue.Count <= 0) return null;
        //Queueからオブジェクトを一つ取り出す
        GameObject go = GroundPrefabQueue.Dequeue();
        //オブジェクトを表示する
        go.gameObject.SetActive(true);
        //生成位置の調整
        _pos.z = _setZLength; //z.positonを０はじめにする
        go.transform.position = _pos;
        _setZLength += _addZLength;
        //呼び出し元に渡す
        return go;
    }
}
