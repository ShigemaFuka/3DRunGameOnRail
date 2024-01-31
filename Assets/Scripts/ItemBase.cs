using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public class ItemBase : MonoBehaviour
{
    [SerializeField, Tooltip("ギミックのCollectを呼ぶオブジェクト")] GameObject _collectGimmickObject = default;
    void Start()
    {
        _collectGimmickObject = GameObject.Find("CollectGimmicks");
    }

    /// <summary>
    /// Collect関数を呼ぶコライダーつきのオブジェクト付近かつ、
    /// カメラに映らない位置に移動させる
    /// 特定のQueueからスポーンしたものを特定のQueueに入れるため
    /// ※スポーンされていない地面プレハブの子オブジェクトであるギミックはCollectされない
    /// （地面プレハブのSetActiveChildrenで管理している）
    /// </summary>
    protected void SetPosition()
    {
        // ギミック（地面のプレハブの子オブジェクト・スポーンされるもの）をカメラの後ろへ
        gameObject.transform.position = _collectGimmickObject.transform.position + new Vector3(0, 0, 5);
    }
}