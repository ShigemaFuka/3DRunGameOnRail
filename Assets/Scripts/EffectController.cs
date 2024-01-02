using System;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static EffectController Instance = default;
    [SerializeField] EffectClass[] _effectClass = default;

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