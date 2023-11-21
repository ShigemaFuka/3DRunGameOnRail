using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPÇä«óù
/// </summary>
public class PlayerHp : MonoBehaviour
{
    [SerializeField, Tooltip("ç≈ëÂHP")] int _maxHp = 3;
    [Tooltip("åªç›ÇÃHP")] static int _nowHp = 0;
    public int NowHp { get => _nowHp; set => _nowHp = value; }
    [SerializeField] Text _hpText = null;

    void Start()
    {
        ChangeNowHp(_maxHp);
    }

    void Update()
    {
        if (_nowHp <= 0)
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