using UnityEngine;

/// <summary>
/// �����ɂ��Q�[���I�[�o�[��́A�ĊJ���Ƀ��X�L�����N���Ȃ��悤�ɁA��ݒu
/// </summary>
public class DontResKill : MonoBehaviour
{
    [Tooltip("�v���C���[")] GameObject _gameObject;
    void Start()
    {
        _gameObject = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// �v���C���[�̑����ɔz�u
    /// GM��Start�֐��̃^�C�~���O�̃C�x���g�ɓo�^
    /// </summary>
    public void SetPos(bool isSetPos)
    {
        var targetPos = _gameObject.transform.position;
        targetPos.z += 3f; //�����O���ɔz�u 
        transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
    }
}
