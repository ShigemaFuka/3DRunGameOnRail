using UnityEngine;

/// <summary>
/// カメラと対象物が一定距離離れたら(hitしたら)、非表示とみなし、順にCollect関数で格納する
/// </summary>
public class CallCollect : MonoBehaviour
{
    [SerializeField] Vector3 _back;
    [SerializeField] ObjectPoolGround _objectPoolGround;
    [SerializeField] float _length = 7;
    [Tooltip("格納された数")] int _count;

    void Start()
    {
        _count = 0;
    }

    void Update()
    {
        Ray ray = new Ray(gameObject.transform.position, _back * _length);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.DrawRay(gameObject.transform.position, _back * _length, Color.red);
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.activeSelf == true)
            {
                _objectPoolGround.Collect(hitObj);
                _count++;
            }
            if (_count >= 3)
            {
                _objectPoolGround.Launch();
                //Debug.Log("表示");
            }
        }
        Debug.DrawRay(gameObject.transform.position, _back * _length, Color.red);
    }
}
