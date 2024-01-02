using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���邽�߂̃p�u���b�N�ϐ�")] public static EffectController Instance = default;
    [SerializeField] EffectClass[] _effectClass = default;

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
        data?.ParticleSystem?.Play();
    }

    [Serializable]
    public class EffectClass
    {
        //[SerializeField] AudioClip _seClip;
        //public AudioClip SeClip => _seClip;
        [SerializeField] ParticleSystem _particleSystem;
        public ParticleSystem ParticleSystem => _particleSystem;

        [SerializeField]
        Effect _effectState;
        public Effect EffectState => _effectState;

        public enum Effect
        {
            Coin,
            SeedUp,
            Heart,
            Crash,
            Fever,
            Ring
        }
    }
}