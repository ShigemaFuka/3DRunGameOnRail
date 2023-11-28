using UnityEngine;
/// <summary>
/// �v���C���[��HP�𑝌�����I�u�W�F�N�g�ɃA�^�b�`����
/// </summary>

public class ChangePlayerHp : ItemBase
{
    [SerializeField, Tooltip("���������")] int _value = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHp>().ChangeNowHp(_value);
            PlayEffectAndSE();
            //gameObject.SetActive(false);
            SetPosition();
        }
    }
}