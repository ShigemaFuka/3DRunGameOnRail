using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("インスタンスを取得するためのパブリック変数")] public static GameManager Instance = default;
    [Tooltip("現在のスコア")] static int _score = 0;
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("スコアを表示するテキスト")] Text _scoreText = default;

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
    }

    void Update()
    {

    }

    /// <summary>
    /// オブジェクトがPlayerに接触したら、この関数を呼んでスコアを加算
    /// </summary>
    public void AddScore(int value)
    {
        Score += value;
        ShowText();
    }

    /// <summary>
    /// オブジェクトがPlayerに接触したら、この関数を呼んでスコアを減算
    /// </summary>
    public void SubtractScore(int value)
    {
        Score -= value;
        ShowText();
    }

    public void ShowText()
    {
        _scoreText.text = Score.ToString("00000");
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
