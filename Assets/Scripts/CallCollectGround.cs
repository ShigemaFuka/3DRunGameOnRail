using UnityEngine;

/// <summary>
/// �J�����ƑΏە�����苗�����ꂽ��(hit������)�A��\���Ƃ݂Ȃ��A����Collect�֐��Ŋi�[����
/// �����Ȃ��Ƃ���Ŋi�[
/// </summary>
public class CallCollectGround : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] ObjectPoolGround _objectPoolGround;
    [SerializeField] float _length = 7;
    [Tooltip("�i�[���ꂽ��")] int _count;

    void Start()
    {
        _count = 0;
    }

    /// <summary>
    /// ���C�L���X�g�ł̓����蔻��̂��߂ɐ���`��
    /// ����������Collect�֐��Ŋi�[�������s��
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
            //�i�[�����O�̂Ƃ���Launch���悤�Ƃ���ƁANull���Ԃ��Ă��邱�Ƃɂ��A�����ɕs�s�������邽�ߗ]�T���������Ă���
            if (_count >= 3)
            {
                _objectPoolGround.Launch();
                _count--;
            }
        }
        Debug.DrawRay(gameObject.transform.position, _direction * _length, Color.red);
    }
}
