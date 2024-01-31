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
        //if (GM.Instance._isPause == true)
        //{
        //    _target.SetActive(true);
        //}
        //else
        //    _target.SetActive(false);

        //_target.SetActive(GM.Instance._isPause);

        if (GM.Instance.NowState == GM.GameState.Pause)
            _target.SetActive(true);
        else
            _target.SetActive(false);
    }
}
