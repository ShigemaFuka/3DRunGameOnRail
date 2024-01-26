using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面のプレハブの子になっているアイテムや、敵キャラのSetActiveを管理
/// 地面のプレハブ同様、使い回す
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField, Tooltip("子オブジェクト内のギミック")] List<Transform> _childrenTransform = default;
    [SerializeField, Tooltip("位置の情報")] List<Vector3> _childrenVector3 = default;
    bool _hasChildren;

    void Start()
    {
        GetChildren();
    }

    /// <summary>
    /// 地面プレハブのSetActiveが真になったときに、子オブジェクトも真にする
    /// </summary>
    void OnEnable()
    {
        if (_hasChildren)
        {
            //foreach (var childTransform in _childrenTransform)
            //{
            //    childTransform.gameObject.SetActive(true);
            //}
            for (var i = 0; i < _childrenTransform.Count; i++)
            {
                _childrenTransform[i].gameObject.SetActive(true);
                _childrenTransform[i].gameObject.transform.localPosition = _childrenVector3[i];
            }
        }
    }

    /// <summary>
    /// 地面プレハブ直下の子オブジェクトを取得
    /// アイテムや敵キャラ以外の、モデルは対象外
    /// </summary>
    void GetChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("Gimmick"))
            {
                _childrenTransform.Add(child);
                _childrenVector3.Add(child.transform.localPosition);
            }
        }
        _hasChildren = true;
        //Debug.Log(_hasChildren);
    }
}
