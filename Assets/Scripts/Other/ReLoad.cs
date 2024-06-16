using UnityEngine;

/// <summary>
/// HelpUIが表示されているときのみ有効にする
/// GMでSetActiveを制御
/// </summary>
public class ReLoad : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GM.Instance.Reload();
        }
    }
}
