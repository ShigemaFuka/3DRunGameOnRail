using UnityEngine;

/// <summary>
/// 手動で配置したオブジェクトの非表示化
/// 一定位置までプレイヤーが移動したら、非表示にする
/// </summary>
public class SetActiveChange : MonoBehaviour
{
    [SerializeField, Tooltip("対象")] GameObject _target;
    void OnEnable()
    {
        //リロードしたら復活
        _target.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _target.SetActive(false);
        }
    }
}
