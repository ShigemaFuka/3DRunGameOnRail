using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField, Tooltip("���Z����X�R�A")] int _score = 5;

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
            GameManager.Instance.AddScore(_score);
        }
    }
}
