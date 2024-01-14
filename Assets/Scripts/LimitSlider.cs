using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���x�㏸�ێ��́A�p�����Ԃɉ����ăX���C�_�[��ϓ�������
/// ���x���f�t�H���g�̂܂ܕς���Ă��Ȃ��Ƃ��́A�X���C�_�[���\���ɂ���
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
