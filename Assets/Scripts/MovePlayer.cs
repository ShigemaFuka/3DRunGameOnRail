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
    [SerializeField, Tooltip("左右移動の遅延時間")] float _wfs = 0.5f;
    [SerializeField, Tooltip("左右移動")] bool _isTransform;

    void Start()
    {
        _count = 0;
        Speed = _defaultSpeed;
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
    /// ジャンプ中や、加速アイテム側が使用する
    /// </summary>
    /// <param name="addSpeed">例）1.5など</param>
    public void ChangeMoveSpeed(float addSpeed)
    {
        Speed *= addSpeed;
    }

    /// <summary>
    /// アニメーションのイベントトリガーで使う
    /// </summary>
    public void ResetSpeed()
    {
        Speed = _defaultSpeed;
    }

    /// <summary>
    /// 高速反復横跳びを防ぐ
    /// 若干の左右移動の遅延を起こす
    /// </summary>
    IEnumerator MoveControll()
    {
        _isTransform = false;
        yield return new WaitForSeconds(_wfs);
        _isTransform = true;
    }
}
