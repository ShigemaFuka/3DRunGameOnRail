using System;
using UnityEngine;

/// <summary>
/// エフェクト(パーティクルシステム)、BGM、SEを再生する
/// </summary>
public class EffectController : MonoBehaviour
{
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static EffectController Instance = default;
    [SerializeField] AudioSource _seAudio = default;
    [SerializeField] AudioSource _bgmAudio = default;
    [SerializeField, Tooltip("パーティクルシステムなどのエフェクト")] EffectClass[] _effectClass = default;
    [SerializeField] SeClass[] _seClass = default;
    [SerializeField] BgmClass[] _bgmClass;

    void Awake()
    {
        // この処理は Start() に書いてもよいが、Awake() に書くことが多い。
        // 参考: イベント関数の実行順序 https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
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
            if (playEffect.EffectState != effect) continue; //一致するまで以下をスキップして繰り返す
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
            Applause // 喝采
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