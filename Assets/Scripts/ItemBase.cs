using UnityEngine;

public class ItemBase : MonoBehaviour
{
    //[SerializeField] AudioSource _audioSource = default;
    //[SerializeField] GameObject _effect = default;
    //[SerializeField, Tooltip("吹き飛ぶアニメーションのモデル")] protected bool _isKnockOut = default;
    [SerializeField, Tooltip("ギミックのCollectを呼ぶオブジェクト")] GameObject _collectGimmickObject = default;
    void Start()
    {
        _collectGimmickObject = GameObject.Find("CollectGimmicks");
    }

    //public void PlayEffectAndSE()
    //{
    //    if (_audioSource) AudioSource.PlayClipAtPoint(_audioSource.clip, gameObject.transform.position);
    //    if (_effect) Instantiate(_effect, transform.position, Quaternion.identity);
    //}

    /// <summary>
    /// 地面のプレハブの子オブジェクト: 即座に非アクティブにする
    /// スポーンされるオブジェクト: 
    /// Collect関数を呼ぶコライダーつきのオブジェクト付近かつ、
    /// カメラに映らない位置に移動させる
    /// </summary>
    protected void SetPosition()
    {
        //デフォルトでGimmickのタグが付いている
        //それが付いていないアイテムや敵キャラのオブジェクトについては、スポーン時に別のタグに更新されているから
        if (gameObject.CompareTag("Gimmick"))
        {
            gameObject.SetActive(false);
        }
        //スポーンされるオブジェクト カメラに映らないところで格納する
        else gameObject.transform.position = _collectGimmickObject.transform.position + new Vector3(0, 0, 10);
    }
}