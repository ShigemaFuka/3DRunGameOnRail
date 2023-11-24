using UnityEngine;

/// <summary>
/// 地面のプレハブの子になっているアイテムや、敵キャラのSetActiveを管理
/// 地面のプレハブ同様、使い回す
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField] Transform[] _childrenTransform = null;
    bool _hasChildren;

    void Start()
    {
        _childrenTransform = new Transform[transform.childCount];
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
    /// 地面プレハブ直下の子オブジェクトを全て取得
    /// </summary>
    void GetChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
            _childrenTransform[i] = transform.GetChild(i);
        _hasChildren = true;
    }
}
