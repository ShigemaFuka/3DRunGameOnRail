using UnityEngine;

/// <summary>
/// HelpUI���\������Ă���Ƃ��̂ݗL���ɂ���
/// GM��SetActive�𐧌�
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
