using System.Collections;
using UnityEngine;

/// <summary>
/// Z軸方向にまっすぐ移動する
/// カーソルキーで左右に移動
/// ※３レーン上で動くように見せかける
/// 速度上昇中はTrailRendererで軌跡を表示
/// </summary>
public class MovePlayer : MonoBehaviour
{
    #region 変数
    [SerializeField, Tooltip("前方と左右の移動制御")] private bool _isMove = default;
    [SerializeField, Tooltip("デフォルトのスピード")] private float _defaultSpeed = 0f;
    [SerializeField, Tooltip("プラマイでX軸方向への移動範囲")] private float _xRange = 3f;
    [SerializeField, Tooltip("速度を戻すまでの時間")] private float _resetTime = 1f;
    [SerializeField, Tooltip("速度上昇の上限")] private float _maxSpeed = 40f;
    [Header("左右移動")]
    [SerializeField, Tooltip("左右移動")] private bool _isTransform = default;
    [SerializeField, Tooltip("左右移動のスピード")] private float _xSpeed = 7f;
    private Vector3 _changedPos = default; // 位置変更を適応させる
    private int _count = 0;
    private readonly WaitForSeconds _wfs = new WaitForSeconds(0.5f); // インターバル時間
    private Rigidbody _rb = default;
    private Vector3 _initialPos = default;
    #endregion

    #region プロパティ
    public float DefaultSpeed => _defaultSpeed;
    /// <summary> 現在のスピード </summary>
    public float Speed { get; set; } = 0f;
    public float MaxSpeed => _maxSpeed;
    /// <summary> 速度を戻すまでの時間 </summary>
    public float ResetTime => _resetTime;
    #endregion

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _initialPos = transform.position;
        _count = 0;
        Speed = _defaultSpeed;
        GM.Instance.Timer = 0;
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// コルーチンを抜ける前に残機がゼロになったとき、Falseのままだから
    /// </summary>
    private void OnEnable()
    {
        _isTransform = true;
    }

    private void FixedUpdate()
    {
        if (_isMove) transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }

    private void Update()
    {
        gameObject.tag = GM.Instance._isInvincible ? "Invincible" : "Player";
        _rb.isKinematic = GM.Instance.NowState == GM.GameState.Pause;

        //落下死
        if (transform.position.y <= -3)
        {
            GM.Instance.GameOver();
            var pos = transform.position;
            // 初期位置よりは奥に行くなら
            if (_initialPos.z - 10f < pos.z - 10f)
                pos.z -= 10f; // 少し手前に戻す
            transform.position = new Vector3(pos.x, 0, pos.z); // 位置を修正
        }

        //ポーズ中は行動停止
        if (GM.Instance.NowState == GM.GameState.InGame)
            _isMove = true;
        else
            _isMove = false;
        if (_isMove)
        {
            if (_isTransform) Move2();

            //一定時間経過したら、速度を戻す
            if (Speed == _defaultSpeed) return;
            GM.Instance.Timer += Time.deltaTime;
            
            if (!(GM.Instance.Timer >= _resetTime)) return;
            Speed = _defaultSpeed;
            GM.Instance.Timer = 0;
        }
    }

    /// <summary>
    /// カーソルキーで左右移動
    /// 座標指定の移動のため、瞬間移動のような挙動
    /// ミニオンラッシュ系
    /// </summary>
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (_count < 1)
            {
                _count++;
                StartCoroutine(MoveControl());
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (_count > -1)
            {
                _count--;
                StartCoroutine(MoveControl());
            }
        }
        if (_count == 1)
            _changedPos = new Vector3(_xRange, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == 0)
            _changedPos = new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z);
        else if (_count == -1)
            _changedPos = new Vector3(-_xRange, gameObject.transform.position.y, gameObject.transform.position.z);
        //左右の上限を超えたときの修正（調整）
        if (transform.position.x > _xRange) _changedPos.x = _xRange;
        else if (transform.position.x < -_xRange) _changedPos.x = -_xRange;
        gameObject.transform.position = _changedPos;
    }

    /// <summary>
    /// ３レーンの縛りがない方
    /// テンプルラン系
    /// </summary>
    private void Move2()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < _xRange)
            {
                transform.Translate(Vector3.right * (Time.deltaTime * _xSpeed));
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -_xRange)
            {
                transform.Translate(Vector3.left * (Time.deltaTime * _xSpeed));
            }
        }
    }

    /// <summary>
    /// 高速反復横跳びを防ぐ
    /// 若干の左右移動の遅延を起こす
    /// </summary>
    private IEnumerator MoveControl()
    {
        _isTransform = false;
        yield return _wfs;
        _isTransform = true;
    }
}