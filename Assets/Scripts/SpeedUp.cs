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
        if (coll.gameObject.CompareTag("Player"))
        {
            var movePlayer = coll.GetComponent<MovePlayer>();
            movePlayer._timer = 0; //最後に取得したタイミングから、カウント開始
            //if(movePlayer.Speed < 30) movePlayer.Speed = ToSpeedUp(movePlayer.Speed);
            if(movePlayer.Speed < movePlayer.MaxSpeed) movePlayer.Speed = ToSpeedUp(movePlayer.Speed);
            PlayEffectAndSE();
            SetPosition();
        }
    }

    float ToSpeedUp(float value)
    {
        value += _addSpeed;
        return value;
    }
}
