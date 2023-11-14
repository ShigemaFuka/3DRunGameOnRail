using UnityEngine;

/// <summary>
/// �J�����ƑΏە�����苗�����ꂽ��(hit������)�A��\���Ƃ݂Ȃ��A����Collect�֐��Ŋi�[����
/// </summary>
public class CallCollect : MonoBehaviour
{
    [SerializeField] Vector3 _back;
    [SerializeField] ObjectPoolGround _objectPoolGround;
    [SerializeField] float _length = 7;
    [Tooltip("�i�[���ꂽ��")] int _count;

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
                //Debug.Log("�\��");
            }
        }
        Debug.DrawRay(gameObject.transform.position, _back * _length, Color.red);
    }
}
