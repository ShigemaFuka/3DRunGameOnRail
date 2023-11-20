using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource = default;
    [SerializeField] GameObject _effect;

    public void PlayEffectAndSE()
    {
        if (_audioSource) AudioSource.PlayClipAtPoint(_audioSource.clip, gameObject.transform.position);
        if (_effect) Instantiate(_effect, transform.position, Quaternion.identity);
    }
}
