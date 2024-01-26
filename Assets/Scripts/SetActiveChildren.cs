using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n�ʂ̃v���n�u�̎q�ɂȂ��Ă���A�C�e����A�G�L������SetActive���Ǘ�
/// �n�ʂ̃v���n�u���l�A�g����
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField, Tooltip("�q�I�u�W�F�N�g���̃M�~�b�N")] List<Transform> _childrenTransform = default;
    [SerializeField, Tooltip("�ʒu�̏��")] List<Vector3> _childrenVector3 = default;
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
            //foreach (var childTransform in _childrenTransform)
            //{
            //    childTransform.gameObject.SetActive(true);
            //}
            for (var i = 0; i < _childrenTransform.Count; i++)
            {
                _childrenTransform[i].gameObject.SetActive(true);
                _childrenTransform[i].gameObject.transform.localPosition = _childrenVector3[i];
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
            {
                _childrenTransform.Add(child);
                _childrenVector3.Add(child.transform.localPosition);
            }
        }
        _hasChildren = true;
        //Debug.Log(_hasChildren);
    }
}
