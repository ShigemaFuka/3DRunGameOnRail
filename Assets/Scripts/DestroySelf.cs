using UnityEngine;
/// <summary>
/// �G�t�F�N�g�p
/// ���g����莞�Ԍ�ɔj��
/// </summary>
public class DestroySelf : MonoBehaviour
{
    [SerializeField] float _lifeTime;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        
    }
}
