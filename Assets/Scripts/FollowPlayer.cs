using UnityEngine;

/// <summary>
/// ‚˜²‚™²‚ğ–³‹‚µ‚ÄA’Ç]
/// </summary>

public class FollowPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("’Ç]‘ÎÛ")] GameObject _target = null;
    [SerializeField, Tooltip("‹——£")] float _offset = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        var pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _target.transform.position.z + _offset);
        this.gameObject.transform.position = pos;
    }
}
