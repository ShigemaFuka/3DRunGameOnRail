using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPを管理
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("最大HP")] int _maxHp = 3;
    [Tooltip("現在のHP")] static int _nowHp = 0;
    [SerializeField] Text _hpText = null;
    [Tooltip("接触したのが敵か")] int _isEnemy = 0;

    #region"プロパティ"
    public int NowHp { get => _nowHp; set => _nowHp = value; }
    public int IsEnemy { get => _isEnemy; }
    #endregion

    void OnEnable()
    {
        NowHp = _maxHp;
        _hpText.text = _nowHp.ToString("00");
    }

    void Update()
    {
        if (_nowHp == 1)
        {
            Debug.Log("残りHP：" + _nowHp);
        }

        // 以下 早期リターンしている
        // 利点
        // １）ネストが深くならない
        // ２）結局どのような処理を行いたいのかが分かりやすい
        // 今回くらいの複雑でないコードでは大差ない
        if (_nowHp > 0)
        {
            return;
        }
        if(GM.Instance._isPause)
        {
            return;
        }
        GM.Instance.GameOver();

        //
        //if (_nowHp <= 0)
        //{
        //    if(GM.Instance._isPause)  //?要る？？
        //    {
        //        GM.Instance.GameOver();
        //    }
        //}
    }

    public void ChangeNowHp(int value)
    {
        _nowHp += value;
        _hpText.text = _nowHp.ToString("00");
        //敵キャラのとき
        //if (value < 0)
        //    IsEnemy = true;
    }
}