using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static EffectController Instance = default;
    [SerializeField] EffectClass[] _effectClass = default;
    [SerializeField] AudioSource _seAudio = default;
    //[SerializeField]
    //private AudioSource _bgmAudio;
    [SerializeField] SeClass[] _seClass = default;

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
        data?.ParticleSystem?.Play();
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
        _seAudio?.PlayOneShot(data?.SeClip);
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
            Ring
        }
    }

    [Serializable]
    public class SeClass
    {
        [SerializeField] AudioClip _seClip;
        [SerializeField] SE _seState;
        #region
        public AudioClip SeClip => _seClip;
        public SE SeState => _seState;
        #endregion

        public enum SE
        {
            GetItem,
            Damage,
            Recovery,
            SpeedUp
        }
    }
}