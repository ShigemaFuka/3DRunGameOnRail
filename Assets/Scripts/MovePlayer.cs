using System.Collections;
using UnityEngine;

/// <summary>
/// Z軸方向にまっすぐ移動する
/// カーソルキーで左右に移動
/// ※３レーン上で動くように見せかける
/// ジャンプ時のZ軸移動が遅く見えるため、そのときだけ少し加速するように、関数を用意
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("前方と左右の移動制御")] bool _isMove;
    //[SerializeField] float _speed = 2;
    [SerializeField, Tooltip("デフォルトのスピード")] float _defaultSpeed = 0;
    [Tooltip("現在のスコア")] static float _speed = 0;
    public float Speed { get => _speed; set => _speed = value; }
    [SerializeField, Tooltip("X軸方向への移動範囲")] float _x = 3;
    [Tooltip("位置変更を適応させる")] Vector3 _changedPos;
    int _count;

    //[SerializeField, Tooltip("左右移動の遅延時間")] float _wfs = 0.5f;
    readonly WaitForSeconds _wfs = new WaitForSeconds(0.5f);
    [SerializeField, Tooltip("左右移動")] bool _isTransform;
    public bool _isResetSpeed;
    [SerializeField, Tooltip("速度を戻すまでの時間の経過")] public float _timer;
    [SerializeField, Tooltip("速度を戻すまでの時間")] float _resetTime = 5f;

    void Start()
    {
        _count = 0;
        Speed = _defaultSpeed;
        _isResetSpeed = false;
        _timer = 0;
    }

    /// <summary>
    /// コルーチンを抜ける前に残機がゼロになったとき、Falseのままだから
    /// </summary>
    void OnEnable()
    {
        _isTransform = true;
    }

    void FixedUpdate()
    {
        if (_isMove) transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }

    void Update()
    {
        if (_isMove)
        {
            if (_isTransform)
                Move();
        }
        //一定時間経過したら、速度を戻す
        if (Speed != _defaultSpeed)
        {
            _timer += Time.deltaTime;
            if (_timer >= _resetTime)
            {
                Speed = _defaultSpeed;
                _timer = 0;
            }
        }
    }

    /// <summary>
    /// カーソルキーで左右移動
    /// </summary>
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_count < 1)
            {
                _count++;
                StartCoroutine(MoveControll());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_count > -1)
            {
                _count--;
                StartCoroutine(MoveControll());
            }
        }
        if (_count == 1)
            _changedPos = new Vector3(_x, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == 0)
            _changedPos = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == -1)
            _changedPos = new Vector3(-_x, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = _changedPos;
    }

    /// <summary>
    /// 高速反復横跳びを防ぐ
    /// 若干の左右移動の遅延を起こす
    /// </summary>
    IEnumerator MoveControll()
    {
        _isTransform = false;
        yield return _wfs;
        _isTransform = true;
    }
}
