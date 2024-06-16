using UnityEngine;
/// <summary>
/// アイテムをプレイヤーの位置に収集する機能
/// </summary>
public class TestCollect : MonoBehaviour
{
    GameObject _playerObject = default;
    [SerializeField] float _speed = 5f;
    [SerializeField] bool _isMove = false;
    void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (_isMove) transform.position = Vector3.MoveTowards(transform.position, _playerObject.transform.position, _speed);
    }
}