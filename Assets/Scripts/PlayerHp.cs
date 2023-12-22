using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP���Ǘ�
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�HP")] int _maxHp = 3;
    [Tooltip("���݂�HP")] static int _nowHp = 0;
    [SerializeField] Text _hpText = null;
    [Tooltip("�ڐG�����̂��G��")] int _isEnemy = 0;

    #region"�v���p�e�B"
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
            Debug.Log("�c��HP�F" + _nowHp);
        }

        // �ȉ� �������^�[�����Ă���
        // ���_
        // �P�j�l�X�g���[���Ȃ�Ȃ�
        // �Q�j���ǂǂ̂悤�ȏ������s�������̂���������₷��
        // ���񂭂炢�̕��G�łȂ��R�[�h�ł͑卷�Ȃ�
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
        //    if(GM.Instance._isPause)  //?�v��H�H
        //    {
        //        GM.Instance.GameOver();
        //    }
        //}
    }

    public void ChangeNowHp(int value)
    {
        _nowHp += value;
        _hpText.text = _nowHp.ToString("00");
        //�G�L�����̂Ƃ�
        //if (value < 0)
        //    IsEnemy = true;
    }
}