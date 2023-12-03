using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n�ʂ̃v���n�u�̎q�ɂȂ��Ă���A�C�e����A�G�L������SetActive���Ǘ�
/// �n�ʂ̃v���n�u���l�A�g����
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField] List<Transform> _childrenTransform = null;
    bool _hasChildren;

    void Start()
    {
        GetChildren();
    }

    /// <summary>
    /// �n�ʃv���n�u��SetActive���^�ɂȂ����Ƃ��ɁA�q�I�u�W�F�N�g���^�ɂ���
    /// </summary>
    void OnEnable()
    {
        if (_hasChildren)
        {
            foreach (var childTransform in _childrenTransform)
            {
                childTransform.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// �n�ʃv���n�u�����̎q�I�u�W�F�N�g���擾
    /// �A�C�e����G�L�����ȊO�́A���f���͑ΏۊO
    /// </summary>
    void GetChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("Gimmick"))
                _childrenTransform.Add(child);
        }
        _hasChildren = true;
    }
}
