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
            var rb = other.gameObject.GetComponent<Rigidbody>();
            //rb.AddForce((Vector3.up + Vector3.forward) * _power, ForceMode.Impulse);
            Vector3 velocity = rb.velocity;
            velocity.y = _power;
            velocity.z = _power;
            rb.velocity = velocity;

            //音とアニメーションを追加したい
            EffectController.Instance.SePlay(EffectController.SeClass.SE.JumpingStand);
            GM.Instance.JumpingStand = true;
        }
    }
}
