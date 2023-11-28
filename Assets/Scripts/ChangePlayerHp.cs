using UnityEngine;
/// <summary>
/// プレイヤーのHPを増減するオブジェクトにアタッチする
/// </summary>

public class ChangePlayerHp : ItemBase
{
    [SerializeField, Tooltip("増減する量")] int _value = 1;

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