using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スポーンで生成されたギミックのみCollectする
/// 接触したギミックのタグと、リストに登録しておいたObjectPoolItemを持つオブジェクトの名前を比較。
/// 一致したらそのオブジェクトのObjectPoolItemのCollect関数を呼ぶ
/// </summary>
public class CollectGimmicks : MonoBehaviour
{
    [SerializeField, Tooltip("ObjectPoolItemをもつオブジェクトのリスト")] List<GameObject> _objectListHavingOPI;

    void OnTriggerEnter(Collider other)
    {
        foreach (var item in _objectListHavingOPI)
        {
            //リストの要素と名前が一致したら
            if (other.gameObject.CompareTag(item.name))
            {
                var opi = item.GetComponent<ObjectPoolItem>();
                opi.Collect(other.gameObject); 
                //これで取り出したのと同じQueueに格納できる
            }
        }
    }
}