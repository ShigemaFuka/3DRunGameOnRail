using UnityEngine;
/// <summary>
/// �v���C���[��HP�𑝌�����I�u�W�F�N�g�ɃA�^�b�`����
/// </summary>

public class ChangePlayerHp : ItemBase
{
    [SerializeField, Tooltip("���������")] int _value = 1;
    //[SerializeField, Tooltip("������ԃA�j���[�V�����̃��f��")] GameObject _gameObject = default;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHp>().ChangeNowHp(_value);
            //PlayEffectAndSE();
            if (_value > 0)
            {
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Heart);
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Ring);
                EffectController.Instance.SePlay(EffectController.SeClass.SE.Recovery);
            }
            else
            {
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Crash);
                EffectController.Instance.SePlay(EffectController.SeClass.SE.Damage);
            }
            SetPosition();
        }
        if (other.gameObject.CompareTag("Invincible"))
        {
            // HP������������
            if (_value > 0)
            {
                other.GetComponent<PlayerHp>().ChangeNowHp(_value);
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Heart);
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Ring);
                EffectController.Instance.SePlay(EffectController.SeClass.SE.Recovery);
            }
            //if (_gameObject) Instantiate(_gameObject, transform.position, Quaternion.identity);
            SetPosition();
        }
    }
}