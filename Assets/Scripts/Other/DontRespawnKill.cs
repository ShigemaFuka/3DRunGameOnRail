using UnityEngine;

/// <summary>
/// 落下によるゲームオーバー後の、再開時にリスキルが起きないように、板を設置
/// </summary>
public class DontRespawnKill : MonoBehaviour
{
    [Tooltip("プレイヤー")] GameObject _gameObject;
    void Start()
    {
        _gameObject = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// プレイヤーの足元に配置
    /// GMのStart関数のタイミングのイベントに登録
    /// </summary>
    public void SetPos(bool isSetPos)
    {
        var targetPos = _gameObject.transform.position;
        targetPos.z += 3f; //少しボードを前方に配置 
        transform.position = new Vector3(targetPos.x, transform.position.y, targetPos.z);
    }
}
