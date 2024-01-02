using UnityEngine;
/// <summary>
/// プレイヤーのHPを増減するオブジェクトにアタッチする
/// </summary>

public class ChangePlayerHp : ItemBase
{
    [SerializeField, Tooltip("増減する量")] int _value = 1;
    [SerializeField, Tooltip("吹き飛ぶアニメーションのモデル")] GameObject _gameObject = default;

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
            }
            SetPosition();
        }
        if (other.gameObject.CompareTag("Invincible"))
        {
            // HP減少が無効化
            if (_value > 0)
            {
                other.GetComponent<PlayerHp>().ChangeNowHp(_value);
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Heart);
                EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Ring);
            }
            if (_gameObject) Instantiate(_gameObject, transform.position, Quaternion.identity);
            SetPosition();
        }
    }
}