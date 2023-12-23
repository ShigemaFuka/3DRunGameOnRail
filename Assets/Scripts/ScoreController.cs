using UnityEngine;

public class ScoreController : ItemBase
{
    [SerializeField, Tooltip("���Z����X�R�A")] int _score = 5;

    /// <summary>
    /// Player�^�O�̃I�u�W�F�N�g�ɐڐG������AGM��AddScore�֐��ŉ��Z���A���g���\���ɂ���
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