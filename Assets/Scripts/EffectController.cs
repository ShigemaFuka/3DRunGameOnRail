using System;
using UnityEngine;

/// <summary>
/// �G�t�F�N�g(�p�[�e�B�N���V�X�e��)�ABGM�ASE���Đ�����
/// </summary>
public class EffectController : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���邽�߂̃p�u���b�N�ϐ�")] public static EffectController Instance = default;
    [SerializeField] AudioSource _seAudio = default;
    [SerializeField] AudioSource _bgmAudio = default;
    [SerializeField, Tooltip("�p�[�e�B�N���V�X�e���Ȃǂ̃G�t�F�N�g")] EffectClass[] _effectClass = default;
    [SerializeField] SeClass[] _seClass = default;
    [SerializeField] BgmClass[] _bgmClass;

    void Awake()
    {
        // ���̏����� Start() �ɏ����Ă��悢���AAwake() �ɏ������Ƃ������B
        // �Q�l: �C�x���g�֐��̎��s���� https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void EffectPlay(EffectClass.Effect effect)
    {
        EffectClass data = null;
        foreach (var playEffect in _effectClass)
        {
            if (playEffect.EffectState != effect) continue; //��v����܂ňȉ����X�L�b�v���ČJ��Ԃ�
            data = playEffect;
            break;
        }
        //_seAudio?.PlayOneShot(data?.SeClip);
        data?.ParticleSystem.Play();
    }

    public void SePlay(SeClass.SE se)
    {
        SeClass data = null;
        foreach (var playSe in _seClass)
        {
            if (playSe.SeState != se) continue;
            data = playSe;
            break;
        }
        _seAudio?.PlayOneShot(data?.SeClip, data.Volume);
    }

    public void BgmPlay(BgmClass.BGM bgm)
    {
        BgmClass data = null;
        foreach (var playSe in _bgmClass)
        {
            if (playSe.BgmState != bgm) continue;
            data = playSe;
            break;
        }
        _bgmAudio.clip = data?.BgmClip;
        _bgmAudio.volume = data.Volume;
        _bgmAudio.Play();
    }

    [Serializable]
    public class EffectClass
    {
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] Effect _effectState;
        #region
        public ParticleSystem ParticleSystem => _particleSystem;
        public Effect EffectState => _effectState;
        #endregion

        public enum Effect
        {
            Coin,
            SeedUp,
            Heart,
            Crash,
            Fever,
            Ring,
            Magnet
        }
    }

    [Serializable]
    public class SeClass
    {
        [SerializeField] AudioClip _seClip;
        [SerializeField] SE _seState;
        [SerializeField] float _volume;
        #region
        public AudioClip SeClip => _seClip;
        public SE SeState => _seState;
        public float Volume => _volume;
        #endregion

        public enum SE
        {
            GetItem,
            Damage,
            Recovery,
            SpeedUp,
            JumpingStand,
            Jump,
            Applause // ����
        }
    }

    [Serializable]
    public class BgmClass
    {
        [SerializeField] AudioClip _bgmClip;
        [SerializeField] BGM _bgmState;
        [SerializeField] float _volume;

        #region
        public AudioClip BgmClip => _bgmClip;
        public BGM BgmState => _bgmState;
        public float Volume => _volume;
        #endregion

        public enum BGM
        {
            InGame,
            GameOver,
            Result
        }
    }
}