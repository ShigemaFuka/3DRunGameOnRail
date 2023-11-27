using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GM : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���邽�߂̃p�u���b�N�ϐ�")] public static GM Instance = default;
    [Tooltip("���݂̃X�R�A")] static int _score = 0;
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("�X�R�A��\������e�L�X�g")] Text _scoreText = default;
    [Tooltip("��������̃X�|�[���̏ꏊ")] public bool[] _isSpawn = new bool[3]; // �M�~�b�N�̐����ꏊ�ƃ^�C�~���O���d�Ȃ�Ȃ��悤��
    [Tooltip("�t���O���U�ɂ���܂ł̎��Ԍv��")] public float[] _timers = new float[3];
    [SerializeField] UnityEvent _onStartEvent = null;
    [SerializeField] UnityEvent _inGameEvent = null;
    [SerializeField] UnityEvent _onResultEvent = null;
    [SerializeField, Tooltip("�ꎞ��~�ƁA�r�����烊�X�^�[�g����p")] UnityEvent _onHelpEvent = null;
    bool _isTimer;
    public bool _inGame;

    void Awake()
    {
        // ���̏����� Start() �ɏ����Ă��悢���AAwake() �ɏ������Ƃ������B
        // �Q�l: �C�x���g�֐��̎��s���� https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
        if (Instance)
        {
            // �C���X�^���X�����ɂ���ꍇ�́A�j������
            Debug.LogWarning($"SingletonSystem �̃C���X�^���X�͊��ɑ��݂���̂ŁA{gameObject.name} �͔j�����܂��B");
            Destroy(this.gameObject);
        }
        else
        {
            // ���̃N���X�̃C���X�^���X�����������ꍇ�́A������ DontDestroyOnload �ɒu��
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
    /// �I�u�W�F�N�g��Player�ɐڐG������A���̊֐����Ă�ŃX�R�A�����Z
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
