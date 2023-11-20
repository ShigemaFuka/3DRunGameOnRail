using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPを管理
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("最大HP")] int _maxHp = 3;
    [Tooltip("現在のHP")] static int _nowHp = 0;
    public int NowHp { get => _nowHp; set => _nowHp = value; }
    [SerializeField] Text _hpText = null;

    void Start()
    {
        ChangeNowHp(_maxHp);
    }

    void Update()
    {
        if (_nowHp < 0)
        {
            GM.Instance.Result();
        }
    }

    public void ChangeNowHp(int value)
    {
        _nowHp += value;
        _hpText.text = _nowHp.ToString("00");
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}