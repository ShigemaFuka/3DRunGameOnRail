using UnityEngine;

/// <summary>
/// Z軸方向にまっすぐ移動する
/// カーソルキーで左右に移動
/// ※３レーン上で動くように見せかける
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField] float _speed = 2;
    [SerializeField] float _speedX = 2;
    [SerializeField] float _x;
    Rigidbody _rb;
    [Space]
    [Header("以下のboolのどちらかを真にすると、左右移動可能になる")]
    [SerializeField, Tooltip("GetAxisで左右移動か")] bool _isGetAxis;
    [SerializeField, Tooltip("Transform.xで左右移動か")] bool _isTransform;
    [Tooltip("位置変更を適応させる")] Vector3 _changedPos;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (_isGetAxis)
        {
            _rb.velocity = new Vector2(h * _speedX, _rb.velocity.y);
        }
        else if (_isTransform)
        {
            //右入力
            if (h > 0)
            {
                _changedPos = new Vector3(_x, gameObject.transform.position.y);
                //_changedPos.x = _x;
                gameObject.transform.position = _changedPos;
            }
            else if (h < 0)
            {
                _changedPos = new Vector3(-_x, gameObject.transform.position.y);
                //_changedPos.x = - _x;
                gameObject.transform.position = _changedPos;
            }
        }
    }
}
