using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 速度上昇維持の、継続時間に応じてスライダーを変動させる
/// 速度がデフォルトのまま変わっていないときは、スライダーを非表示にする
/// </summary>
public class LimitSlider : MonoBehaviour
{
    [SerializeField] GameObject _sliderObject = default;
    Slider _slider = default;
    [SerializeField] MovePlayer _movePlayer = default;

    void Start()
    {
        _slider = _sliderObject.GetComponent<Slider>();
        _slider.maxValue = _movePlayer.ResetTime;
        _slider.minValue = 0;
    }

    void Update()
    {
        if (GM.Instance.Timer <= 0) _sliderObject.SetActive(false);
        else _sliderObject.SetActive(true);
        _slider.value = _movePlayer.ResetTime - GM.Instance.Timer;
    }
}
