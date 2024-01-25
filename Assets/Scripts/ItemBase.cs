using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //[SerializeField] AudioSource _audioSource = default;
    //[SerializeField] GameObject _effect = default;
    //[SerializeField, Tooltip("������ԃA�j���[�V�����̃��f��")] protected bool _isKnockOut = default;
    [SerializeField, Tooltip("�M�~�b�N��Collect���ĂԃI�u�W�F�N�g")] GameObject _collectGimmickObject = default;
    void Start()
    {
        _collectGimmickObject = GameObject.Find("CollectGimmicks");
    }

    //public void PlayEffectAndSE()
    //{
    //    if (_audioSource) AudioSource.PlayClipAtPoint(_audioSource.clip, gameObject.transform.position);
    //    if (_effect) Instantiate(_effect, transform.position, Quaternion.identity);
    //}

    /// <summary>
    /// �n�ʂ̃v���n�u�̎q�I�u�W�F�N�g: �����ɔ�A�N�e�B�u�ɂ���
    /// �X�|�[�������I�u�W�F�N�g: 
    /// Collect�֐����ĂԃR���C�_�[���̃I�u�W�F�N�g�t�߂��A
    /// �J�����ɉf��Ȃ��ʒu�Ɉړ�������
    /// </summary>
    protected void SetPosition()
    {
        //�f�t�H���g��Gimmick�̃^�O���t���Ă���
        //���ꂪ�t���Ă��Ȃ��A�C�e����G�L�����̃I�u�W�F�N�g�ɂ��ẮA�X�|�[�����ɕʂ̃^�O�ɍX�V����Ă��邩��
        if (gameObject.CompareTag("Gimmick"))
        {
            gameObject.SetActive(false);
        }
        //�X�|�[�������I�u�W�F�N�g �J�����ɉf��Ȃ��Ƃ���Ŋi�[����
        else gameObject.transform.position = _collectGimmickObject.transform.position + new Vector3(0, 0, 10);
    }
}