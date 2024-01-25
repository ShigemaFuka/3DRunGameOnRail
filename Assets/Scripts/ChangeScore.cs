using UnityEngine;

public class ChangeScore : ItemBase, IPull
{
    [SerializeField, Tooltip("���Z����X�R�A")] int _score = 5;
    GameObject _playerObject = default;
    [Tooltip("���B������")] bool _isReach = false;
    public void PullItem(float speed)
    {
        if (!_playerObject) _playerObject = GameObject.FindGameObjectWithTag("Player");
        if (_playerObject && !_isReach)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerObject.transform.position, speed);
            float distance = Vector3.Distance(_playerObject.transform.position, transform.position);
            if (distance <= 0.5)
            {
                _isReach = true;
                Debug.Log("isReach : " + _isReach);
            }
        }
    }

    /// <summary>
    /// Player�^�O�̃I�u�W�F�N�g�ɐڐG������AGM��AddScore�֐��ŉ��Z���A���g���\���ɂ���
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Invincible"))
        {
            GM.Instance.ChangeScore(_score);
            //PlayEffectAndSE();
            SetPosition();
            EffectController.Instance.EffectPlay(EffectController.EffectClass.Effect.Coin);
            EffectController.Instance.SePlay(EffectController.SeClass.SE.GetItem);
        }
    }
}