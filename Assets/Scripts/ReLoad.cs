using UnityEngine;

/// <summary>
/// HelpUI‚ª•\¦‚³‚ê‚Ä‚¢‚é‚Æ‚«‚Ì‚İ—LŒø‚É‚·‚é
/// GM‚ÅSetActive‚ğ§Œä
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
