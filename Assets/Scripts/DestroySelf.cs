using UnityEngine;
/// <summary>
/// エフェクト用
/// 自身を一定時間後に破棄
/// </summary>
public class DestroySelf : MonoBehaviour
{
    [SerializeField] float _lifeTime;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        
    }
}
