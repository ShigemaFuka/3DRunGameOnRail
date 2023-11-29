using UnityEngine;

/// <summary>
/// �蓮�Ŕz�u�����I�u�W�F�N�g�̔�\����
/// ���ʒu�܂Ńv���C���[���ړ�������A��\���ɂ���
/// </summary>
public class SetActiveChange : MonoBehaviour
{
    [SerializeField, Tooltip("�Ώ�")] GameObject _target;
    void OnEnable()
    {
        //�����[�h�����畜��
        _target.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _target.SetActive(false);
        }
    }
}
