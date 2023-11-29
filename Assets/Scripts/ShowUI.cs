using UnityEngine;

/// <summary>
/// GMのフラグに合わせてSetActiveの真偽を切り替える
/// UIを表示するオブジェクトの親オブジェクトにアタッチする
/// </summary>
public class ShowUI : MonoBehaviour
{
    [SerializeField, Tooltip("対象")] GameObject _target;
    void Start()
    {
        _target.SetActive(false);
    }

    void Update()
    {
        if (GM.Instance._isHelpEvent == true)
        {
            _target.SetActive(true);
        }
        else
            _target.SetActive(false);
    }
}
