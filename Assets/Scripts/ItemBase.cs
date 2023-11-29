using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource = default;
    [SerializeField] GameObject _effect;
    [SerializeField, Tooltip("�M�~�b�N��Collect�I�u�W�F�N�g")] GameObject _collectGimmickObject;

    void Start()
    {
        _collectGimmickObject = GameObject.Find("CollectGimmicks");
    }

    public void PlayEffectAndSE()
    {
        if (_audioSource) AudioSource.PlayClipAtPoint(_audioSource.clip, gameObject.transform.position);
        if (_effect) Instantiate(_effect, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Collect�֐����ĂԃR���C�_�[���̃I�u�W�F�N�g�t�߂��A
    /// �J�����ɉf��Ȃ��ʒu�Ɉړ�������
    /// </summary>
    public void SetPosition()
    {
        if (gameObject.CompareTag("SpawnGimmick"))
        {
            gameObject.transform.position = _collectGimmickObject.transform.position + new Vector3(0, 0, 10);
        }
        else gameObject.SetActive(false);
    }
}