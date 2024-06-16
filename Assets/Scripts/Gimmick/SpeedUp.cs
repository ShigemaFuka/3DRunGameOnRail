using UnityEngine;

/// <summary>
/// 取得することによって、前方へのスピードが上がるアイテム
/// GMでタイムの計測とリセットを行う
/// 最後に取得してからの、GMのリセット時間までスピードUPの効果がある
/// </summary>
public class SpeedUp : ItemBase
{
    [SerializeField] int _addSpeed;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Invincible"))
        {
            var movePlayer = coll.GetComponent<MovePlayer>();
            GM.Instance.Timer = 0; //最後に取得したタイミングから、カウント開始
            if(movePlayer.Speed < movePlayer.MaxSpeed) movePlayer.Speed = ToSpeedUp(movePlayer.Speed);
            //PlayEffectAndSE();
            EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.SeedUp);
            EffectController.Instance.SePlay(EffectController.SeClass.SE.SpeedUp);
            SetPosition();
        }
    }

    float ToSpeedUp(float value)
    {
        value += _addSpeed;
        return value;
    }
}
