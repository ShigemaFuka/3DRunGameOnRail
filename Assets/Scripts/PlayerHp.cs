using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HP���Ǘ�
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("�ő�HP")] int _maxHp = 3;
    [Tooltip("���݂�HP")] static int _nowHp = 0;
    public int NowHp { get => _nowHp; set => _nowHp = value; }
    [SerializeField] Text _hpText = null;

    void OnEnable()
    {
        NowHp = _maxHp;
        _hpText.text = _nowHp.ToString("00");
    }

    void Update()
    {
        // �ȉ� �������^�[�����Ă���
        // ���_
        // �P�j�l�X�g���[���Ȃ�Ȃ�
        // �Q�j���ǂǂ̂悤�ȏ������s�������̂���������₷��
        // ���񂭂炢�̕��G�łȂ��R�[�h�ł͑卷�Ȃ�
        if (_nowHp > 0)
        {
            return;
        }
        if(!GM.Instance._isPause)
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
    }
}