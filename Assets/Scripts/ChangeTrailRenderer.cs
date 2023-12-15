using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 上昇速度に応じて色の変化を付ける
/// TrailRendererはプレイヤーの足元で表示している
/// </summary>
public class ChangeTrailRenderer : MonoBehaviour
{
    [SerializeField] MovePlayer _movePlayer = default;
    [SerializeField, Tooltip("走ったところの残像")] TrailRenderer _trailRenderer = default;

    void Start()
    {

    }

    void Update()
    {
        if (_movePlayer.Speed > _movePlayer.DefaultSpeed)
        {
            _trailRenderer.enabled = true; //描画
            if (_movePlayer.Speed >= _movePlayer.MaxSpeed)
            {
                Change(Color.red);
                Debug.Log("3段階目");
            }
            else if (_movePlayer.Speed >= (_movePlayer.MaxSpeed / 2))
            {
                Change(Color.blue + Color.red);
                Debug.Log("2段階目");
            }
            else if (_movePlayer.Speed >= (_movePlayer.MaxSpeed / 4))
            {
                Change(Color.blue);
                Debug.Log("1段階目");
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
