using UnityEngine;

/// <summary>
/// Z軸方向にまっすぐ移動する
/// カーソルキーで左右に移動
/// ※３レーン上で動くように見せかける
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("前方と左右の移動制御")] bool _isMove;
    [SerializeField] float _speed = 2;
    [SerializeField] float _speedX = 2;
    [SerializeField, Tooltip("X軸方向への移動範囲")] float _x = 3;
    Rigidbody _rb;
    [Space]
    [Header("以下のboolのどちらかを真にすると、左右移動可能になる")]
    [SerializeField, Tooltip("滑らか")] bool _isGetAxis;
    [SerializeField, Tooltip("瞬間")] bool _isTransform;
    [Tooltip("位置変更を適応させる")] Vector3 _changedPos;
    int _count;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _count = 0;
    }

    void FixedUpdate()
    {
        if (_isMove) transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (_isMove)
        {


            if (_isGetAxis)
            {
                _rb.velocity = new Vector2(h * _speedX, _rb.velocity.y);
            }
            else if (_isTransform)
            {
                //右入力
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (_count < 1)
                        _count++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (_count > -1)
                        _count--;
                }
                if (_count == 1)
                    _changedPos = new Vector3(_x, gameObject.transform.position.y, gameObject.transform.position.z);
                else if (_count == 0)
                    _changedPos = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
                else if (_count == -1)
                    _changedPos = new Vector3(-_x, gameObject.transform.position.y, gameObject.transform.position.z);
                gameObject.transform.position = _changedPos;
            }
        }
    }
}
