using UnityEngine;

/// <summary>
/// 前転 一定時間経過したら回転終了
/// </summary>
public class RotationX : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = default;
    //[SerializeField, Tooltip("回転する")] bool _isRotate = false;
    [SerializeField, Tooltip("継続時間")] float _limit = default;
    [SerializeField] float _timer = 0;
    float _defaultRotateX = default;

    void Start()
    {
        _defaultRotateX = transform.rotation.x;
    }

    void Update()
    {
        //if (GM.Instance.JumpingStand)
        //    _isRotate = GM.Instance.JumpingStand;

        //if (_isRotate)
        if (GM.Instance.JumpingStand)
        {
            transform.Rotate(Vector3.right * _rotationSpeed * Time.deltaTime, Space.World);
            _timer += Time.deltaTime;
        }
        if (_timer >= _limit)
        {
            _timer = 0;
            //_isRotate = false;
            //GM.Instance.JumpingStand = _isRotate;
            GM.Instance.JumpingStand = false;
            transform.localRotation = default;
        }
    }
}
