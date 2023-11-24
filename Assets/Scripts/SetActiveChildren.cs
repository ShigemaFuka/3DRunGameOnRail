using UnityEngine;

/// <summary>
/// �n�ʂ̃v���n�u�̎q�ɂȂ��Ă���A�C�e����A�G�L������SetActive���Ǘ�
/// �n�ʂ̃v���n�u���l�A�g����
/// </summary>
public class SetActiveChildren : MonoBehaviour
{
    [SerializeField] Transform[] _childrenTransform = null;
    bool _hasChildren;

    void Start()
    {
        _childrenTransform = new Transform[transform.childCount];
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
    /// �n�ʃv���n�u�����̎q�I�u�W�F�N�g��S�Ď擾
    /// </summary>
    void GetChildren()
    {
        for (var i = 0; i < transform.childCount; i++)
            _childrenTransform[i] = transform.GetChild(i);
        _hasChildren = true;
    }
}
