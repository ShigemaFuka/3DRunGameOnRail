using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPを管理
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("最大HP")] int _maxHp = 3;
    [Tooltip("現在のHP")] static int _nowHp = 0;
    [SerializeField] Text _hpText = default;
    [Tooltip("接触したのが敵か")] int _isEnemy = 0;

    #region"プロパティ"
    public int NowHp { get => _nowHp; set => _nowHp = value; }
    public int IsEnemy { get => _isEnemy; }
    #endregion

    void OnEnable()
    {
        NowHp = _maxHp;
        _hpText.text = NowHp.ToString("00");
    }

    void Update()
    {

        // 以下 早期リターンしている
        // 利点
        // １）ネストが深くならない
        // ２）結局どのような処理を行いたいのかが分かりやすい
        // 今回くらいの複雑でないコードでは大差ない
        if (NowHp > 0)
        {
            return;
        }
        //if(GM.Instance._isPause)
        if (GM.Instance.NowState == GM.GameState.Pause)
        {
            return;
        }
        GM.Instance.GameOver();
    }

    public void ChangeNowHp(int value)
    {
        NowHp += value;
        _hpText.text = NowHp.ToString("00");
    }
}