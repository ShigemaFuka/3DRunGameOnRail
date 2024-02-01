using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP���Ǘ�
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�HP")] int _maxHp = 3;
    [Tooltip("���݂�HP")] static int _nowHp = 0;
    [SerializeField] Text _hpText = default;
    [Tooltip("�ڐG�����̂��G��")] int _isEnemy = 0;

    #region"�v���p�e�B"
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

        // �ȉ� �������^�[�����Ă���
        // ���_
        // �P�j�l�X�g���[���Ȃ�Ȃ�
        // �Q�j���ǂǂ̂悤�ȏ������s�������̂���������₷��
        // ���񂭂炢�̕��G�łȂ��R�[�h�ł͑卷�Ȃ�
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