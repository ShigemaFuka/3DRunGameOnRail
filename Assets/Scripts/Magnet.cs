using UnityEngine;
/// <summary>
/// マグネットを取得したら、GMのIsPullItemを真にすることで、
/// IPullを継承しているアイテムの、継承した関数を呼び出す
/// </summary>
public class Magnet : ItemBase
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Invincible"))
        {
            GM.Instance.IsPullItem = true;
            SetPosition();
            EffectController.Instance.SePlay(EffectController.SeClass.SE.GetItem);
            EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Magnet);
        }
    }
}
