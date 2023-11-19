using UnityEngine;

/// <summary>
/// ���������𖳎����āA�Ǐ]
/// </summary>

public class FollowPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("�Ǐ]�Ώ�")] GameObject _target = null;
    [SerializeField, Tooltip("����")] float _offset = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        var pos = new Vector3(0, 0, _target.transform.position.z + _offset);
        this.gameObject.transform.position = pos;
    }
}
