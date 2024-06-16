using UnityEngine;

/// <summary>
/// カメラと対象物が一定距離離れたら(hitしたら)、非表示とみなし、順にCollect関数で格納する
/// 見えないところで格納
/// </summary>
public class CallCollectGround : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] ObjectPoolGround _objectPoolGround;
    [SerializeField] float _length = 7;
    [Tooltip("格納された数")] int _count;

    void Start()
    {
        _count = 0;
    }

    /// <summary>
    /// レイキャストでの当たり判定のために線を描画
    /// 当たったらCollect関数で格納処理を行う
    /// </summary>
    void Update()
    {
        Ray ray = new Ray(gameObject.transform.position, _direction * _length);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.DrawRay(gameObject.transform.position, _direction * _length, Color.red);
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.activeSelf == true && hitObj.CompareTag("Ground"))
            {
                _objectPoolGround.Collect(hitObj);
                _count++;
            }
            //格納数が０のときにLaunchしようとすると、Nullが返ってくることにより、処理に不都合があるため余裕を持たせている
            if (_count >= 3)
            {
                _objectPoolGround.Launch();
                _count--;
            }
        }
        Debug.DrawRay(gameObject.transform.position, _direction * _length, Color.red);
    }
}
