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
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("スコアを表示するテキスト")] Text _scoreText = default;
    [Tooltip("生成直後のスポーンの場所")] public bool[] _isSpawn = new bool[3]; // ギミックの生成場所とタイミングが重ならないように
    [Tooltip("フラグを偽にするまでの時間計測")] public float[] _timers = new float[3];
    [SerializeField] UnityEvent _onStartEvent = null;
    [SerializeField] UnityEvent _inGameEvent = null;
    [SerializeField] UnityEvent _onResultEvent = null;
    bool _isTimer;
    public bool _inGame;
    [Tooltip("UIを表示するか")] public bool _isHelpEvent;


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
        _isHelpEvent = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _inGameEvent.Invoke();
            _inGame = true;
            _isTimer = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isHelpEvent = !_isHelpEvent;
            _isTimer = !_isTimer;
        }
        if (_isTimer)
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
        ShowText();
    }

    public void ShowText()
    {
        _scoreText.text = Score.ToString("00000");
    }

    public void Result()
    {
        _onResultEvent.Invoke();
        _inGame = false;
        _isTimer = false;
        Debug.Log("Result");
    }

    /// <summary>
    /// Trueになってから、一定時間たったらFalseにする
    /// </summary>
    /// <param name="boolIndex"></param>
    void FlagChange(int boolIndex)
    {
        if (_isSpawn[boolIndex])
        {
            if (_timers[boolIndex] >= 1.5f)
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