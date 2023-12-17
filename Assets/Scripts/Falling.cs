using UnityEngine;
/// <summary>
/// �v���C���[���ڐG�����痎��
/// </summary>
public class Falling : MonoBehaviour
{
    GameObject _player;
    Vector3 _pos;
    [SerializeField] float _speed = 4;
    [SerializeField] float _limit = -4;
    bool _isFall;
    [SerializeField] bool _useRb;
    Rigidbody _rb;
    void Start()
    {
        _player = GameObject.Find("Player");
        _pos = transform.position;
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    void OnEnable()
    {
        if(_rb) Reset();
    }

    void Update()
    {
        // y���̈ʒu�����ɖ߂�
        if (!GM.Instance._inGame)
        {
            Reset();
        }
        if (_isFall)
        {
            if (_useRb) Kinema();
            else Fall();
        }
        if (transform.position.y <= _limit)
            _rb.isKinematic = true;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            _isFall = true;
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            _isFall = false;
    }

    /// <summary>
    /// ���Ɉړ�
    /// </summary>
    void Fall()
    {
        if (transform.position.y >= _limit)
        {
            //transform.Translate(new Vector3(0, 0.1f*_speed * Time.deltaTime, 0));
            transform.Translate(_speed * Time.deltaTime * Vector3.down);
            Debug.Log("down");
        }
    }

    /// <summary>
    /// ���������𖳌�
    /// </summary>
    void Kinema()
    {
        _rb.isKinematic = false;
    }

    void Reset()
    {
        transform.position = new Vector3(transform.position.x, _pos.y, transform.position.z);
        _rb.isKinematic = true; // ���������𖳌�
    }
}
