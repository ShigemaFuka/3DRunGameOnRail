using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// スタート・インゲーム・ゲームオーバー時の処理、
/// 生成箇所のフラグ管理、ポーズ画面の表示するタイミングのフラグ管理
/// </summary>
public class GM : MonoBehaviour
{
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static GM Instance = default;
    [Tooltip("現在のスコア")] static int _score = 0;
    [SerializeField, Tooltip("スコアを表示するテキスト")] Text _scoreText = default;
    //[SerializeField, Tooltip("引き寄せ機能を有効にする範囲")] Collider _collider = default;
    [Tooltip("生成直後のスポーンの場所")] public bool[] _isSpawn = new bool[3]; // ギミックの生成場所とタイミングが重ならないように
    [Tooltip("フラグを偽にするまでの時間計測")] private float[] _timers = new float[3];
    [SerializeField] UnityEvent _onStartEvent = default;
    [SerializeField] UnityEvent _inGameEvent = default;
    [SerializeField] UnityEvent _onGameOverEvent = default;
    public bool _inGame = false;
    [Tooltip("ポーズ画面のUIを表示するか")] public bool _isPause = false;
    bool _inGameOver = false;
    [Tooltip("プレイヤーの無敵化")] public bool _isInvincible = false;
    [Tooltip("ジャンプ台に接触したか")] bool _jumpingStand = false;
    [Tooltip("プレイヤーの速度を戻すまでの時間の経過")] float _timer = 0f;
    [Tooltip("アイテムを引き寄せる")] bool _isPullItem = false;
    [SerializeField, Tooltip("マグネットの効果時間")] float _pullLimit = 10f;
    [SerializeField, Tooltip("マグネット用の時間")] float _pullTimer = 0f;

    #region"プロパティ"
    //↑プロパティをまとめておいて、開閉することでコード全体を見やすくする
    //private ScoreManager _scoreManager;
    //public ScoreManager ScoreManager => _scoreManager; 
    //↑こうしておくと、GMを参照すればそのクラスを利用できる
    public int Score { get => _score; set => _score = value; }
    public bool JumpingStand { get => _jumpingStand; set => _jumpingStand = value; }
    public float Timer { get => _timer; set => _timer = value; }
    public bool IsPullItem { get => _isPullItem; set => _isPullItem = value; }
    #endregion

    void Awake()
    {
        // この処理は Start() に書いてもよいが、Awake() に書くことが多い。
        // 参考: イベント関数の実行順序 https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Score = 0;
        _onStartEvent.Invoke();
        _isPause = false;
        JumpingStand = false;
        _pullTimer = 0f;
        EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.InGame);
    }

    void Update()
    {
        // テスト用
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsPullItem = !IsPullItem;
            //Debug.Log(IsPullItem);
        }
        //スタート・リスタート
        if (Input.GetKeyDown(KeyCode.Return) && !_inGame)
        {
            _inGameEvent.Invoke();
            // 以下はリスタート時のBGM再生
            if (_inGameOver) EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.InGame);
            _inGame = true;
        }
        //ポーズ画面
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isPause = !_isPause;
        }
        if (_inGame && !_isPause)
        {
            _timers[0] += Time.deltaTime;
            _timers[1] += Time.deltaTime;
            _timers[2] += Time.deltaTime;
            FlagChange(0);
            FlagChange(1);
            FlagChange(2);
        }
        // 時限式 マグネット機能停止
        if (IsPullItem)
        {
            _pullTimer += Time.deltaTime;
            if (_pullTimer >= _pullLimit)
            {
                IsPullItem = false;
                _pullTimer = 0;
            }
        }
    }

    /// <summary>
    /// オブジェクトがPlayerに接触したら、この関数を呼んでスコアを加算
    /// </summary>
    public void ChangeScore(int value)
    {
        Score += value;
        _scoreText.text = Score.ToString("00000");
    }

    public void GameOver()
    {
        _onGameOverEvent.Invoke();
        _inGame = false;
        _inGameOver = !_inGame;
        EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.GameOver);
        //_isInvincible = true;
        Debug.Log("GameOver");
    }

    /// <summary>
    /// Trueになってから、一定時間たったらFalseにする
    /// </summary>
    /// <param name="boolIndex"></param>
    void FlagChange(int boolIndex)
    {
        if (_isSpawn[boolIndex])
        {
            if (_timers[boolIndex] >= 0.2f)
            {
                _isSpawn[boolIndex] = false;
                _timers[boolIndex] = 0;
            }
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}