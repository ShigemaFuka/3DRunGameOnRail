using UnityEngine;

/// <summary>
/// �v���C���[���ڐG���Ă�����A��O���Ɉ�u�͂�������
/// </summary>
public class JumpingGimmick : MonoBehaviour
{
    [SerializeField, Tooltip("�З�")] float _power = 20f;

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

            //���ƃA�j���[�V������ǉ�������
            EffectController.Instance.SePlay(EffectController.SeClass.SE.JumpingStand);
            GM.Instance.JumpingStand = true;
        }
    }
}
