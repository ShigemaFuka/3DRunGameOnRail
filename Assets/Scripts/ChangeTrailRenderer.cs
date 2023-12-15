using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// �㏸���x�ɉ����ĐF�̕ω���t����
/// TrailRenderer�̓v���C���[�̑����ŕ\�����Ă���
/// </summary>
public class ChangeTrailRenderer : MonoBehaviour
{
    [SerializeField] MovePlayer _movePlayer = default;
    [SerializeField, Tooltip("�������Ƃ���̎c��")] TrailRenderer _trailRenderer = default;

    void Start()
    {

    }

    void Update()
    {
        if (_movePlayer.Speed > _movePlayer.DefaultSpeed)
        {
            _trailRenderer.enabled = true; //�`��
            if (_movePlayer.Speed >= _movePlayer.MaxSpeed)
            {
                Change(Color.red);
                Debug.Log("3�i�K��");
            }
            else if (_movePlayer.Speed >= (_movePlayer.MaxSpeed / 2))
            {
                Change(Color.blue + Color.red);
                Debug.Log("2�i�K��");
            }
            else if (_movePlayer.Speed >= (_movePlayer.MaxSpeed / 4))
            {
                Change(Color.blue);
                Debug.Log("1�i�K��");
            }
        }
        else
        {
            _trailRenderer.enabled = false;
        }
    }

    void Change(Color color)
    {
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f)/*, new GradientColorKey(Color.red, 1.0f)*/ },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        _trailRenderer.colorGradient = gradient;
    }
}
