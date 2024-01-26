using UnityEngine;
/// <summary>
/// �}�O�l�b�g���擾������AGM��IsPullItem��^�ɂ��邱�ƂŁA
/// IPull���p�����Ă���A�C�e���́A�p�������֐����Ăяo��
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
