using UnityEngine;

public class ScoreController : ItemBase
{
    [SerializeField, Tooltip("加算するスコア")] int _score = 5;

    /// <summary>
    /// Playerタグのオブジェクトに接触したら、GMのAddScore関数で加算し、自身を非表示にする
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Invincible"))
        {
            GM.Instance.ChangeScore(_score);
            PlayEffectAndSE();
            SetPosition();
        }
    }
}