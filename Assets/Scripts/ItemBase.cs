using UnityEngine;

/// <summary>
/// �A�C�e���̊��N���X
/// </summary>
public class ItemBase : MonoBehaviour
{
    [SerializeField, Tooltip("�M�~�b�N��Collect���ĂԃI�u�W�F�N�g")] GameObject _collectGimmickObject = default;
    void Start()
    {
        _collectGimmickObject = GameObject.Find("CollectGimmicks");
    }

    /// <summary>
    /// Collect�֐����ĂԃR���C�_�[���̃I�u�W�F�N�g�t�߂��A
    /// �J�����ɉf��Ȃ��ʒu�Ɉړ�������
    /// �����Queue����X�|�[���������̂�����Queue�ɓ���邽��
    /// ���X�|�[������Ă��Ȃ��n�ʃv���n�u�̎q�I�u�W�F�N�g�ł���M�~�b�N��Collect����Ȃ�
    /// �i�n�ʃv���n�u��SetActiveChildren�ŊǗ����Ă���j
    /// </summary>
    protected void SetPosition()
    {
        // �M�~�b�N�i�n�ʂ̃v���n�u�̎q�I�u�W�F�N�g�E�X�|�[���������́j���J�����̌���
        gameObject.transform.position = _collectGimmickObject.transform.position + new Vector3(0, 0, 5);
    }
}