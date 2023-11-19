using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField, Tooltip("���Z����X�R�A")] int _score = 5;
    [SerializeField, Tooltip("�擾���ɖ炷��")] AudioSource _audioSource = default;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Player�^�O�̃I�u�W�F�N�g�ɐڐG������AGM��AddScore�֐��ŉ��Z���A���g���\���ɂ���
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GM.Instance.ChangeScore(_score);
            if (_audioSource) _audioSource.PlayOneShot(_audioSource.clip);
            var pos = new Vector3(gameObject.transform.position.x, -1, gameObject.transform.position.z);
            gameObject.transform.position = pos;
        }
    }
}