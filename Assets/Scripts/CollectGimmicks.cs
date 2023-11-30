using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�|�[���Ő������ꂽ�M�~�b�N�̂�Collect����
/// �ڐG�����M�~�b�N�̃^�O�ƁA���X�g�ɓo�^���Ă�����ObjectPoolItem�����I�u�W�F�N�g�̖��O���r�B
/// ��v�����炻�̃I�u�W�F�N�g��ObjectPoolItem��Collect�֐����Ă�
/// </summary>
public class CollectGimmicks : MonoBehaviour
{
    [SerializeField, Tooltip("ObjectPoolItem�����I�u�W�F�N�g�̃��X�g")] List<GameObject> _objectListHavingOPI;

    void OnTriggerEnter(Collider other)
    {
        foreach (var item in _objectListHavingOPI)
        {
            //���X�g�̗v�f�Ɩ��O����v������
            if (other.gameObject.CompareTag(item.name))
            {
                var opi = item.GetComponent<ObjectPoolItem>();
                opi.Collect(other.gameObject); 
                //����Ŏ��o�����̂Ɠ���Queue�Ɋi�[�ł���
            }
        }
    }
}