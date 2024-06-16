using UnityEngine;

/// <summary>
/// ｘ軸ｙ軸を無視して、追従
/// </summary>

public class FollowPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("追従対象")] GameObject _target = null;
    [SerializeField, Tooltip("距離")] float _offset = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        var pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _target.transform.position.z + _offset);
        this.gameObject.transform.position = pos;
    }
}
