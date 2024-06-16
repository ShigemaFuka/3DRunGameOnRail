using UnityEngine;
/// <summary>
/// プレイヤーが敵と接触したときに表示するパネル
/// </summary>
public class ShowPanel : MonoBehaviour
{
    [SerializeField] GameObject _panel = default;
    [SerializeField, Tooltip("表示時間")] float _limit = 0.1f;
    float _timer = 0f;
    void Start()
    {
        _panel.SetActive(false);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _limit)
        {
            _panel.SetActive(false);
        }
    }

}
