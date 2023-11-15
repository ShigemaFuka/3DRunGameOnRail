using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField, Tooltip("加算するスコア")] int _score = 5;
    [SerializeField, Tooltip("取得時に鳴らす音")] AudioSource _audioSource = default;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Playerタグのオブジェクトに接触したら、GMのAddScore関数で加算し、自身を非表示にする
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(_score);
            if (_audioSource) _audioSource.PlayOneShot(_audioSource.clip);
            gameObject.SetActive(false);
        }
    }
}