using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 速度に応じてスライダーを変動させる
/// </summary>
public class LimitSlider : MonoBehaviour
{
    [SerializeField] Slider _slider = default;
    [SerializeField] MovePlayer _movePlayer = default;

    void Start()
    {
        _slider.maxValue = _movePlayer.ResetTime;
        _slider.minValue = 0;
    }

    void Update()
    {
        _slider.value = _movePlayer.ResetTime - GM.Instance.Timer;
    }
}
