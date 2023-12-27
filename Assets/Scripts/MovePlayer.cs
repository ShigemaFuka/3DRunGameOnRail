using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Z軸方向にまっすぐ移動する
/// カーソルキーで左右に移動
/// ※３レーン上で動くように見せかける
/// 速度上昇中はTrailRendererで軌跡を表示
/// </summary>
public class MovePlayer : MonoBehaviour
{
    [SerializeField, Tooltip("前方と左右の移動制御")] bool _isMove = default;
    [SerializeField, Tooltip("デフォルトのスピード")] float _defaultSpeed = 0f;
    [Tooltip("現在のスピード")] static float _speed = 0f;
    [SerializeField, Tooltip("X軸方向への移動範囲")] float _x = 3f;
    [Tooltip("位置変更を適応させる")] Vector3 _changedPos = default;
    [SerializeField] int _count = 0;

    readonly WaitForSeconds _wfs = new WaitForSeconds(0.5f);
    [SerializeField, Tooltip("左右移動")] bool _isTransform = default;
    [Tooltip("速度を戻すまでの時間の経過")] public float _timer = 0f;
    [SerializeField, Tooltip("速度を戻すまでの時間")] float _resetTime = 1f;
    //[SerializeField, Tooltip("走ったところの残像")] TrailRenderer _trailRenderer = default;

    [SerializeField, Header("正数で良い"), Tooltip("プラマイでｘ軸の移動範囲")] float _xRange = 2.0f;
    [SerializeField, Tooltip("左右移動のスピード")] float _xSpeed = 7f;
    [SerializeField, Tooltip("速度上昇の上限")] float _maxSpeed = 40f;
    Rigidbody _rb = default;


    #region"プロパティ"
    public float DefaultSpeed { get => _defaultSpeed; }
    public float Speed { get => _speed; set => _speed = value; }

    public float MaxSpeed { get => _maxSpeed; }
    #endregion


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _count = 0;
        Speed = _defaultSpeed;
        _timer = 0;
        _rb = GetComponent<Rigidbody>();
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
        if (GM.Instance._isInvincible)
            gameObject.tag = "Invincible";
        else
            gameObject.tag = "Player";

        if (GM.Instance._isPause)
            _rb.isKinematic = true;
        else _rb.isKinematic = false;

        //落下死
        if (transform.position.y <= -3)
        {
            GM.Instance.GameOver();
            var pos = transform.position;
            pos.z -= 10f; // 少し手前に戻す
            // 位置を修正
            transform.position = new Vector3(pos.x, 0, pos.z);
        }

        //ポーズ中は行動停止
        _isMove = !GM.Instance._isPause;
        if (_isMove)
        {
            if (_isTransform) Move2();
                //Move();
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
    /// 座標指定の移動のため、瞬間移動のような挙動
    /// ミニオンラッシュ系
    /// </summary>
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (_count < 1)
            {
                _count++;
                StartCoroutine(MoveControll());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
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
    /// ３レーンの縛りがない方
    /// テンプルラン系
    /// </summary>
    void Move2()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < _xRange)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _xSpeed);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -_xRange)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _xSpeed);
            }
        }
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
