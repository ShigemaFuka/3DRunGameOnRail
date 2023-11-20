using UnityEngine;

public class SpeedUp : ItemBase
{
    [SerializeField] float _addSpeed;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var movePlayer = coll.GetComponent<MovePlayer>();
            movePlayer._timer = 0; //最後に取得したタイミングから、カウント開始
            movePlayer.Speed = ToSpeedUp(movePlayer.Speed);
            PlayEffectAndSE();
            gameObject.SetActive(false);
        }
    }

    float ToSpeedUp(float value)
    {
        value += _addSpeed;
        return value;
    }
}
