using UnityEngine;

/// <summary>
/// 無敵状態のときに、UIとBGM再生
/// </summary>
public class Invincible : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource = default;
    [SerializeField,Tooltip("高くするピッチの値")] float _pitch = default;
    [Tooltip("元々のピッチの値")] float _defaultPitch = default;
    [SerializeField] Animator _animator = default;
    void Start()
    {
        _defaultPitch = _audioSource.pitch;
        _animator.SetBool("Invincible", false);
    }

    void Update()
    {
        if (GM.Instance._isInvincible && GM.Instance._inGame && !GM.Instance._isPause)
        {
            _audioSource.pitch = _pitch;
            _animator.SetBool("Invincible", true);
        }
        else
        {
            _audioSource.pitch = _defaultPitch;
            _animator.SetBool("Invincible", false);
        }
    }
}
