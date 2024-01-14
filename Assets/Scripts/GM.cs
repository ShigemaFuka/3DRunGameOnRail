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
    [SerializeField, Tooltip("�X�R�A��\������e�L�X�g")] Text _scoreText = default;
    [Tooltip("��������̃X�|�[���̏ꏊ")] public bool[] _isSpawn = new bool[3]; // �M�~�b�N�̐����ꏊ�ƃ^�C�~���O���d�Ȃ�Ȃ��悤��
    [Tooltip("�t���O���U�ɂ���܂ł̎��Ԍv��")] private float[] _timers = new float[3];
    [SerializeField] UnityEvent _onStartEvent = default;
    [SerializeField] UnityEvent _inGameEvent = default;
    [SerializeField] UnityEvent _onGameOverEvent = default;
    public bool _inGame = false;
    [Tooltip("�|�[�Y��ʂ�UI��\�����邩")] public bool _isPause = false;
    [Tooltip("�v���C���[�̖��G��")] public bool _isInvincible = false;
    [Tooltip("�W�����v��ɐڐG������")] bool _jumpingStand = false;
    [Tooltip("�v���C���[�̑��x��߂��܂ł̎��Ԃ̌o��")] float _timer = 0f;

    #region"�v���p�e�B"
    //���v���p�e�B���܂Ƃ߂Ă����āA�J���邱�ƂŃR�[�h�S�̂����₷������
    //private ScoreManager _scoreManager;
    //public ScoreManager ScoreManager => _scoreManager; 
    //���������Ă����ƁAGM���Q�Ƃ���΂��̃N���X�𗘗p�ł���
    public int Score { get => _score; set => _score = value; }
    public bool JumpingStand { get => _jumpingStand; set => _jumpingStand = value; }
    public float Timer { get => _timer; set => _timer = value; }
    #endregion

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
        _isPause = false;
        JumpingStand = false;
    }

    void Update()
    {
        //���X�^�[�g
        if (Input.GetKeyDown(KeyCode.Return)&& !_inGame)
        {
            _inGameEvent.Invoke();
            _inGame = true;
        }
        //�|�[�Y���
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
    /// �I�u�W�F�N�g��Player�ɐڐG������A���̊֐����Ă�ŃX�R�A�����Z
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
    /// True�ɂȂ��Ă���A��莞�Ԃ�������False�ɂ���
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