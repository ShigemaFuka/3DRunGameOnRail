using UnityEngine;
/// <summary>
/// �v���C���[��HP�𑝌�����I�u�W�F�N�g�ɃA�^�b�`����
/// </summary>

public class ChangePlayerHp : ItemBase
{
    [SerializeField, Tooltip("���������")] int _value = 1;
    [SerializeField, Tooltip("������ԃA�j���[�V�����̃��f��")] GameObject _gameObject = default;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHp>().ChangeNowHp(_value);
            PlayEffectAndSE();
            SetPosition();
        }
        if (other.gameObject.CompareTag("Invincible"))
        {
            // HP������������
            if (_value > 0) other.GetComponent<PlayerHp>().ChangeNowHp(_value);
            if(_gameObject) Instantiate(_gameObject, transform.position, Quaternion.identity);
            SetPosition();
        }
    }
}