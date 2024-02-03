using UnityEngine;
/// <summary>
/// IPull���p�����Ă���A�C�e���̂݁A���̋@�\���Ăяo��
/// ����i01/24�j�F�R�C�����v���C���[�Ɍ������Ĉړ�����
/// </summary>
public class CallPull : MonoBehaviour
{
    [SerializeField, Tooltip("���x")] float _speed = 5f;

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
