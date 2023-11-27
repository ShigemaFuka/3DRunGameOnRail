using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [SerializeField, Tooltip("一時停止と、途中からリスタートする用")] UnityEvent _onHelpEvent = null;
    bool _isTimer;
    public bool _inGame;

    void Awake()
    {
        // この処理は Start() に書いてもよいが、Awake() に書くことが多い。
        // 参考: イベント関数の実行順序 https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
        if (Instance)
        {
            // インスタンスが既にある場合は、破棄する
            Debug.LogWarning($"SingletonSystem のインスタンスは既に存在するので、{gameObject.name} は破棄します。");
            Destroy(this.gameObject);
        }
        else
        {
            // このクラスのインスタンスが無かった場合は、自分を DontDestroyOnload に置く
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        Score = 0;
        _onStartEvent.Invoke();
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
            _onHelpEvent.Invoke();
            _isTimer = false;
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

    void FlagChange(int boolIndex)
    {
        if (_isSpawn[boolIndex])
        {
            if (_timers[boolIndex] >= 3f)
            {
                _isSpawn[boolIndex] = false;
                _timers[boolIndex] = 0;
            }
        }
    }
}
