using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���U���g�ŕ\������g�[�^���X�R�A���Ǘ�����
/// �R�C����G�𐁂���΂������A�R���e�B�j���[�̉񐔂ɉ����ĕϓ�����
/// �^�C���A�b�v�ɂȂ����烊�U���g���v�Z
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField, Tooltip("�g�[�^���e�L�X�g")] Text _totalScoreText = default;
    [SerializeField, Tooltip("�R�C���e�L�X�g")] Text _coinText = default;
    [SerializeField, Tooltip("�L�����e�L�X�g")] Text _killText = default;
    [SerializeField, Tooltip("�c�@�e�L�X�g")] Text _hpText = default;
    [SerializeField, Tooltip("�R���e�B�j���[�̉񐔃e�L�X�g")] Text _continueText = default;
    [SerializeField, Tooltip("�����N�e�L�X�g")] Text _rankText = default;

    [SerializeField, Tooltip("�g�[�^���X�R�A")] float _totalScore = 0;
    [SerializeField, Tooltip("�R�C��")] int _coin = 0;
    [SerializeField, Tooltip("�L����")] int _kill = 0;
    [SerializeField, Tooltip("�c�@")] int _hp = 0;
    [SerializeField, Tooltip("�R���e�B�j���[�̉�")] float _continue = 0;

    void Start()
    {
        _totalScore = 0;
        _coin = 0;
        _kill = 0;
        _hp = 0;
        _continue = 0;
    }

    public void AddScore(int amount)
    {

    }

    public void Result()
    {
        _coin = GM.Instance.Coin;
        _kill = GM.Instance.KillCount * 100;
        _hp = GM.Instance.HP * 300;
        _continue = (_coin + _kill) * 0.2f * GM.Instance.ContinueCount;
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
