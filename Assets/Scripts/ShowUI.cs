using UnityEngine;

/// <summary>
/// GM�̃t���O�ɍ��킹��SetActive�̐^�U��؂�ւ���
/// UI��\������I�u�W�F�N�g�̐e�I�u�W�F�N�g�ɃA�^�b�`����
/// </summary>
public class ShowUI : MonoBehaviour
{
    [SerializeField, Tooltip("�Ώ�")] GameObject _target;
    void Start()
    {
        _target.SetActive(false);
    }

    void Update()
    {
        if (GM.Instance._isHelpEvent == true)
        {
            _target.SetActive(true);
        }
        else
            _target.SetActive(false);
    }
}
