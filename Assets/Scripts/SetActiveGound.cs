using UnityEngine;

/// <summary>
/// �n�ʂ̕\���E��\�����s��
/// </summary>
public class SetActiveGound : MonoBehaviour
{
    ObjectPoolGround _objectPoolGround;

    void Start()
    {
        _objectPoolGround = GetComponent<ObjectPoolGround>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �{�^���ŎQ��
    /// </summary>
    public void onLanch()
    {
        _objectPoolGround.Launch();
    }
}
