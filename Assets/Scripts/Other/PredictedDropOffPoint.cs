using UnityEngine;

/// <summary>
/// 落下地点を予測してそこにマーカーを表示する。
/// </summary>
public class PredictedDropOffPoint : MonoBehaviour
{
    [SerializeField, Header("落下するオブジェクト")] private GameObject _fallingObject;
    [SerializeField, Header("マーカーのプレハブ")] private GameObject _markerPrefab;
    private GameObject _markerInstance = default;
    private Vector3 _fallPoint = default;
    private MovePlayer _movePlayer = default;

    private void Start()
    {
        _markerInstance = Instantiate(_markerPrefab, _fallPoint, Quaternion.identity);
        _markerInstance.transform.position = new Vector3(0, 0, -1000); // 見えないところへ配置
    }

    private void Update()
    {
        // プレイヤーの左右移動に合わせてマーカーを移動
        var pos = _fallingObject.transform.position;
        _markerInstance.transform.position = new Vector3(pos.x, _fallPoint.y, _fallPoint.z);
    }

    /// <summary>
    /// ジャンプが行われた地点からの落下予想地点を算出する。
    /// </summary>
    /// <param name="rb"></param>
    public void SetJumpStartPoint(Rigidbody rb)
    {
        if (!_movePlayer) _movePlayer = FindObjectOfType<MovePlayer>();
        Vector3 initialVelocity = rb.velocity + new Vector3(0, 0, _movePlayer.Speed);
        Vector3 initialPosition = _fallingObject.transform.position;
        Vector3 gravity = Physics.gravity;
        _fallPoint = CalculateFallPoint(initialPosition, initialVelocity, gravity);
        _markerInstance.transform.position = _fallPoint;
    }

    /// <summary>
    /// 落下予想地点を計算
    /// </summary>
    /// <param name="initialPosition"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="gravity"></param>
    /// <returns></returns>
    private Vector3 CalculateFallPoint(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        // y軸方向の速度成分と重力加速度を用いて落下時間を計算
        float timeToHitGround =
            (-initialVelocity.y -
             Mathf.Sqrt(initialVelocity.y * initialVelocity.y - 2 * gravity.y * initialPosition.y)) / gravity.y;
        // 落下時間を用いて地面に達する位置を計算
        Vector3 fallPoint = initialPosition + initialVelocity * timeToHitGround +
                            0.5f * gravity * timeToHitGround * timeToHitGround;
        fallPoint.y = 0; // y軸方向を0に修正（地面上の地点を計算）


        return fallPoint;
    }
}