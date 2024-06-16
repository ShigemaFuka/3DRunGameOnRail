using System;
using UnityEngine;

/// <summary>
/// プレイヤーが接触してきたら、上前方に一瞬力を加える
/// </summary>
public class JumpingGimmick : MonoBehaviour
{
    [SerializeField, Tooltip("威力")] private float _power = 20f; 
    private PredictedDropOffPoint _predictedDropOffPoint = default;

    private void Start()
    {
        _predictedDropOffPoint = FindObjectOfType<PredictedDropOffPoint>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Invincible"))
        {
            var rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 velocity = rb.velocity;
            velocity.y = _power;
            velocity.z = _power;
            rb.velocity = velocity;

            _predictedDropOffPoint.SetJumpStartPoint(rb);

            EffectController.Instance.SePlay(EffectController.SeClass.SE.JumpingStand);
            GM.Instance.JumpingStand = true;
        }
    }
}
