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
    #region
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static GM Instance = default;
    [SerializeField, Tooltip("現在のステート")] GameState _nowState = GameState.InGame;
    [SerializeField, Tooltip("前のステート")] GameState _oldState = GameState.Start;
    [SerializeField, Tooltip("制限時間")] float _limitTime = 15f;
    [SerializeField, Tooltip("制限時間の計測")] static float _limitTimer = 0.0f;
    [Tooltip("現在のコイン数")] static int _coin = 0;
    [Tooltip("(評価時に参照する)プレイヤーの残機")] static int _hp = 0;
    [Tooltip("敵のキル数")] static int _killCount = 0;
    [Tooltip("コンティニューの回数")] static int _continueCount = 0;
    [SerializeField, Tooltip("スコアを表示するテキスト")] Text _coinText = default;
    [SerializeField, Tooltip("制限時間を表示するテキスト")] Text _timeLimitText = default;
    [SerializeField, Tooltip("マグネットの効果時間")] float _pullLimit = 10f;
    [SerializeField, Tooltip("マグネット用の時間")] float _pullTimer = 0f;
    [SerializeField] PlayerHp _playerHp = default;
    [SerializeField, Tooltip("残り時間が少なくなった時にアニメーション再生")] Animator _limitTimerAnimator = default;
    [SerializeField, Tooltip("UIのAnim")] Animator _hpUiAnimator = default;


    //[SerializeField, Tooltip("引き寄せ機能を有効にする範囲")] Collider _collider = default;
    [Tooltip("生成直後のスポーンの場所")] public bool[] _isSpawn = new bool[5]; // ギミックの生成場所とタイミングが重ならないように
    [Tooltip("フラグを偽にするまでの時間計測")] private float[] _timers = new float[5];
    [SerializeField] UnityEvent _onStartEvent = default;
    [SerializeField] UnityEvent _inGameEvent = default;
    [SerializeField] UnityEvent _onGameOverEvent = default;
    [SerializeField] UnityEvent _onResultEvent = default;

    //public bool _inGame = false;
    //[SerializeField] bool _isResult = false;
    //[Tooltip("ポーズ画面のUIを表示するか")] public bool _isPause = false;
    //bool _inGameOver = false;
    [Tooltip("プレイヤーの無敵化")] public bool _isInvincible = false;
    [Tooltip("ジャンプ台に接触したか")] bool _jumpingStand = false;
    [Tooltip("プレイヤーの速度を戻すまでの時間の経過")] float _timer = 0f;
    [Tooltip("アイテムを引き寄せる")] bool _isPullItem = false;
    //[SerializeField, Tooltip("前フレームのステート")] GameState _oldState = GameState.InGame;
    //ScoreManager _scoreManager = default;
    #endregion

    #region"プロパティ"
    //↑プロパティをまとめておいて、開閉することでコード全体を見やすくする
    private ScoreManager _scoreManager;
    public ScoreManager ScoreManager => _scoreManager;
    //↑こうしておくと、GMを参照すればそのクラスを利用できる
    public int Coin { get => _coin; set => _coin = value; }
    public int HP { get => _hp; set => _hp = value; }
    public int KillCount { get => _killCount; set => _killCount = value; }
    public int ContinueCount { get => _continueCount; set => _continueCount = value; }
    public bool JumpingStand { get => _jumpingStand; set => _jumpingStand = value; }
    public float Timer { get => _timer; set => _timer = value; }
    public float LimitTimer { get => _limitTimer; set => _limitTimer = value; }
    public bool IsPullItem { get => _isPullItem; set => _isPullItem = value; }
    public GameState NowState { get => _nowState; set => _nowState = value; }
    public GameState OldState { get => _oldState; set => _oldState = value; }

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

    /// <summary> ゲームの状態を管理する列挙型 </summary>
    public enum GameState
    {
        Start,
        InGame,
        Pause,
        GameOver,
        Result,
    }

    void Start()
    {
        _onStartEvent.Invoke();
        Initialize();
    }

    void Initialize()
    {
        NowState = GameState.Start;
        Coin = 0;
        KillCount = 0;
        ContinueCount = 0;
        HP = 0;
        LimitTimer = _limitTime;
        //_isPause = false;
        //_isResult = false;
        JumpingStand = false;
        _pullTimer = 0f;
        _scoreManager = FindObjectOfType<ScoreManager>();
        EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.InGame);
    }

    void Update()
    {
        // テスト用
        if (Input.GetKeyDown(KeyCode.K))
        {
            Result();
        }

        // 制限時間を超えたら
        if (LimitTimer <= 0)
        {
            //if (!_isResult) 
            if (NowState != GameState.Result)
                Result();
        }

        //スタート・リスタート
        //if (Input.GetKeyDown(KeyCode.Return) && !_inGame)
        if (Input.GetKeyDown(KeyCode.Return) && NowState != GameState.InGame && NowState != GameState.Pause)
        {
            _inGameEvent.Invoke();
            // 以下はリスタート時のBGM再生
            //if (_inGameOver)
            if (NowState == GameState.GameOver)
            {
                EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.InGame);
                ContinueCount++;
            }
            //if (_isResult)
            if (NowState == GameState.Result)
            {
                Reload();
            }
            LimitTimer = _limitTime;
            //_inGame = true;
            NowState = GameState.InGame;
        }
        //ポーズ画面
        else if (Input.GetKeyDown(KeyCode.Tab) && NowState != GameState.Result)
        {
            //_isPause = !_isPause;

            //NowState = (NowState == GameState.Pause) ? GameState.InGame : GameState.Pause;

            if (NowState != GameState.Pause)
            {
                OldState = NowState;
                NowState = GameState.Pause;
            }
            else
                NowState = OldState; // ポーズする前のステートに戻すため
        }

        //if (_inGame && !_isPause)
        if (NowState == GameState.InGame) // inGameのときだけ計算
        {
            _timers[0] += Time.deltaTime;
            _timers[1] += Time.deltaTime;
            _timers[2] += Time.deltaTime;
            _timers[3] += Time.deltaTime;
            _timers[4] += Time.deltaTime;
            FlagChange(0);
            FlagChange(1);
            FlagChange(2);
            FlagChange(3);
            FlagChange(4);
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
            LimitTimer -= Time.deltaTime;
        }

        _timeLimitText.text = LimitTimer.ToString("000");

        // アニメーション再生 
        if (LimitTimer <= 4)
            _limitTimerAnimator.SetBool("Limit", true);
        if (NowState != GameState.InGame)
            _limitTimerAnimator.SetBool("Limit", false);

        if (_playerHp.NowHp == 1)
            _hpUiAnimator.SetBool("Hp0", true);
        if (_playerHp.NowHp != 1 || NowState != GameState.InGame)
            _hpUiAnimator.SetBool("Hp0", false);
    }

    /// <summary>
    /// オブジェクトがPlayerに接触したら、この関数を呼んでスコアを加算
    /// </summary>
    public void ChangeScore(int value)
    {
        Coin += value;
        _coinText.text = Coin.ToString("00000");
    }

    public void AddKillCount(int value)
    {
        KillCount += value;
    }

    public void GameOver()
    {
        _onGameOverEvent.Invoke();
        //_inGame = false;
        //_inGameOver = !_inGame;
        NowState = GameState.GameOver;
        EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.GameOver);
        //_isInvincible = true;
    }

    void Result()
    {
        EffectController.Instance.BgmPlay(EffectController.BgmClass.BGM.Result);
        HP = _playerHp.NowHp;
        _onResultEvent.Invoke();
        _scoreManager.Result();
        //_isResult = true;
        //_inGame = false;
        NowState = GameState.Result;
        //Initialize();
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