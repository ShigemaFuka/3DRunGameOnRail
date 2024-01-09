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
            other.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.up + Vector3.forward) * _power, ForceMode.Impulse);
            //���ƃA�j���[�V������ǉ�������
            EffectController.Instance.SePlay(EffectController.SeClass.SE.JumpingStand);
        }
    }
}
