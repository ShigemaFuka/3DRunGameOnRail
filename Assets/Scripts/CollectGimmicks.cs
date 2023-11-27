using UnityEngine;

/// <summary>
/// スポーンで生成されたギミックのみCollectする
/// </summary>
public class CollectGimmicks : MonoBehaviour
{
    [SerializeField] ObjectPoolItem _objectPoolItem;

    void Start()
    {
        _objectPoolItem = FindObjectOfType<ObjectPoolItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnGimmick"))
        {
            _objectPoolItem.Collect(other.gameObject);
        }
    }
}
