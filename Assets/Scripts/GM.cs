using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �X�^�[�g�E�C���Q�[���E�Q�[���I�[�o�[���̏����A
/// �����ӏ��̃t���O�Ǘ��A�|�[�Y��ʂ̕\������^�C�~���O�̃t���O�Ǘ�
/// </summary>
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
    bool _isTimer;
    public bool _inGame;
    [Tooltip("UI��\�����邩")] public bool _isHelpEvent;


    void Awake()
    {
        // ���̏����� Start() �ɏ����Ă��悢���AAwake() �ɏ������Ƃ������B
        // �Q�l: �C�x���g�֐��̎��s���� https://docs.unity3d.com/ja/2019.4/Manual/ExecutionOrder.html
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

    /// <summary>
    /// True�ɂȂ��Ă���A��莞�Ԃ�������False�ɂ���
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