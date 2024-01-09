using UnityEngine;

/// <summary>
/// プレイヤーが接触してきたら、上前方に一瞬力を加える
/// </summary>
public class JumpingGimmick : MonoBehaviour
{
    [SerializeField, Tooltip("威力")] float _power = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Invincible"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.up + Vector3.forward) * _power, ForceMode.Impulse);
            //音とアニメーションを追加したい
            EffectController.Instance.SePlay(EffectController.SeClass.SE.JumpingStand);
        }
    }
}
