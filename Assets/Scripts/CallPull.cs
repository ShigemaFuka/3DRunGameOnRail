using UnityEngine;
/// <summary>
/// IPullを継承しているアイテムのみ、その機能を呼び出す
/// 現状（01/24）：コインがプレイヤーに向かって移動する
/// </summary>
public class CallPull : MonoBehaviour
{
    [SerializeField, Tooltip("速度")] float _speed = 5f;

    //void OnTriggerEnter(Collider other)
    //{
    //    if (GM.Instance.IsPullItem)
    //    {
    //        var iPull = other.GetComponents<IPull>();

    //        if (iPull != null)
    //        {
    //            foreach (var item in iPull)
    //            {
    //                item.PullItem(_speed, false);
    //            }
    //        }
    //    }
    //}
    void OnTriggerStay(Collider other)
    {
        if (GM.Instance.IsPullItem)
        {
            var iPull = other.GetComponents<IPull>();

            if (iPull != null)
            {
                foreach (var item in iPull)
                {
                    item.PullItem(_speed, false);
                }
            }
        }
    }
}
