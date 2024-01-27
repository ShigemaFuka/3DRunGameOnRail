using UnityEngine;

public class ChangeScore : ItemBase, IPull
{
    [SerializeField, Tooltip("加算するスコア")] int _score = 5;
    GameObject _playerObject = default;
    [SerializeField, Tooltip("到達したか")] bool _isReach = false;

    public void PullItem(float speed, bool flag)
    {
        //_isReach = false;
        _isReach = flag;
        if (!_playerObject) _playerObject = GameObject.FindGameObjectWithTag("Player");
        if (_playerObject && !_isReach)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerObject.transform.position, speed);

            float distance = Vector3.Distance(_playerObject.transform.position, transform.position);
            if (distance <= 1)
            {
                _isReach = true;
                return;
            }
        }
    }

    /// <summary>
    /// Playerタグのオブジェクトに接触したら、GMのAddScore関数で加算し、自身を非表示にする
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Invincible"))
        {
            GM.Instance.ChangeScore(_score);
            //PlayEffectAndSE();
            _isReach = true;
            SetPosition();
            EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Coin);
            EffectController.Instance.SePlay(EffectController.SeClass.SE.GetItem);
        }
    }
}