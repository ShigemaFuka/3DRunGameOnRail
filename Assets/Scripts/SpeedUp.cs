using UnityEngine;

/// <summary>
/// �擾���邱�Ƃɂ���āA�O���ւ̃X�s�[�h���オ��A�C�e��
/// GM�Ń^�C���̌v���ƃ��Z�b�g���s��
/// �Ō�Ɏ擾���Ă���́AGM�̃��Z�b�g���Ԃ܂ŃX�s�[�hUP�̌��ʂ�����
/// </summary>
public class SpeedUp : ItemBase
{
    [SerializeField] float _addSpeed;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            var movePlayer = coll.GetComponent<MovePlayer>();
            movePlayer._timer = 0; //�Ō�Ɏ擾�����^�C�~���O����A�J�E���g�J�n
            if(movePlayer.Speed <= 400) movePlayer.Speed = ToSpeedUp(movePlayer.Speed);
            PlayEffectAndSE();
            SetPosition();
        }
    }

    float ToSpeedUp(float value)
    {
        value += _addSpeed;
        return value;
    }
}
