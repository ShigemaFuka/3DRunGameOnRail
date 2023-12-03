using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地面のプレハブの子になっているアイテムや、敵キャラのSetActiveを管理
/// 地面のプレハブ同様、使い回す
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField] List<Transform> _childrenTransform = null;
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
            foreach (var childTransform in _childrenTransform)
            {
                childTransform.gameObject.SetActive(true);
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
                _childrenTransform.Add(child);
        }
        _hasChildren = true;
    }
}
