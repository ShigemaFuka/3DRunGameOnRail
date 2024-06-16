using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトで表示するトータルスコアを管理する
/// コインや敵を吹き飛ばした数、コンティニューの回数に応じて変動する
/// タイムアップになったらリザルトを計算
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField, Tooltip("トータルテキスト")] Text _totalScoreText = default;
    [SerializeField, Tooltip("コインテキスト")] Text _coinText = default;
    [SerializeField, Tooltip("キル数テキスト")] Text _killText = default;
    [SerializeField, Tooltip("残機テキスト")] Text _hpText = default;
    [SerializeField, Tooltip("コンティニューの回数テキスト")] Text _continueText = default;
    [SerializeField, Tooltip("ランクテキスト")] Text _rankText = default;

    [SerializeField, Tooltip("トータルスコア")] float _totalScore = 0;
    [SerializeField, Tooltip("コイン")] int _coin = 0;
    [SerializeField, Tooltip("キル数")] int _kill = 0;
    [SerializeField, Tooltip("残機")] int _hp = 0;
    [SerializeField, Tooltip("コンティニューの回数")] float _continue = 0;

    void Start()
    {
        _totalScore = 0;
        _coin = 0;
        _kill = 0;
        _hp = 0;
        _continue = 0;
    }

    //public void AddScore(int amount)
    //{

    //}

    public void Result()
    {
        _coin = GM.Instance.Coin;
        _kill = GM.Instance.KillCount * 100;
        _hp = GM.Instance.HP * 300;
        _continue = (_coin + _kill + _hp) * 0.2f * GM.Instance.ContinueCount;
        _totalScore = _coin + _kill + _hp - _continue;
        Debug.Log($"_totalScore = _coin + _kill - _continue : {_totalScore} = {_coin} + {_kill} + {_hp} - {_continue}");

        _coinText.text = GM.Instance.Coin.ToString("00000");
        _killText.text = GM.Instance.KillCount.ToString("00");
        _hpText.text = GM.Instance.HP.ToString("00");
        _continueText.text = GM.Instance.ContinueCount.ToString("00");
        _totalScoreText.text = _totalScore.ToString("00000");
        _rankText.text = Rank(_totalScore);
    }

    string Rank(float value)
    {
        string rank;
        if (value >= 8000)
        {
            EffectController.Instance.SePlay(EffectController.SeClass.SE.Applause);
            rank = "S";
        }
        else if (value >= 4000)
        {
            EffectController.Instance.SePlay(EffectController.SeClass.SE.Applause);
            rank = "A";
        }
        else if (value >= 2000)
            rank = "B";
        else if (value >= 1000)
            rank = "C";
        else if (value >= 500)
            rank = "D";
        else
            rank = "E";
        return rank;
    }
}
