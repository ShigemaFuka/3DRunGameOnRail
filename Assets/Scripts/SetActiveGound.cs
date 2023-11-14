using UnityEngine;

/// <summary>
/// 地面の表示・非表示を行う
/// テストする際にボタン機能で簡易的に実行するため
/// </summary>
public class SetActiveGound : MonoBehaviour
{
    ObjectPoolGround _objectPoolGround;

    void Start()
    {
        _objectPoolGround = GetComponent<ObjectPoolGround>();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ボタンで参照
    /// </summary>
    public void onLanch()
    {
        _objectPoolGround.Launch();
    }
}
