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
    [Tooltip("生成直後のスポーンの場所")] public bool[] _isSpawn = new bool[3]; // ギミックの生成場所とタイミングが重ならないように
    [Tooltip("フラグを偽にするまでの時間計測")] private float[] _timers = new float[3];
    [SerializeField] UnityEvent _onStartEvent = default;
    [SerializeField] UnityEvent _inGameEvent = default;
    [SerializeField] UnityEvent _onGameOverEvent = default;
    public bool _inGame = false;
    [Tooltip("ポーズ画面のUIを表示するか")] public bool _isPause = false;
    [Tooltip("プレイヤーの無敵化")] public bool _isInvincible = false;
    [Tooltip("ジャンプ台に接触したか")] bool _jumpingStand = false;
    [Tooltip("プレイヤーの速度を戻すまでの時間の経過")] float _timer = 0f;

    #region"プロパティ"
    //↑プロパティをまとめておいて、開閉することでコード全体を見やすくする
    //private ScoreManager _scoreManager;
    //public ScoreManager ScoreManager => _scoreManager; 
    //↑こうしておくと、GMを参照すればそのクラスを利用できる
    public int Score { get => _score; set => _score = value; }
    public bool JumpingStand { get => _jumpingStand; set => _jumpingStand = value; }
    public float Timer { get => _timer; set => _timer = value; }
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
    }

    void Update()
    {
        //リスタート
        if (Input.GetKeyDown(KeyCode.Return)&& !_inGame)
        {
            _inGameEvent.Invoke();
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