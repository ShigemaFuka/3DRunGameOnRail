using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("�C���X�^���X���擾���邽�߂̃p�u���b�N�ϐ�")] public static GameManager Instance = default;
    [Tooltip("���݂̃X�R�A")] static int _score = 0;
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("�X�R�A��\������e�L�X�g")] Text _scoreText = default;

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
    }

    void Update()
    {

    }

    /// <summary>
    /// �I�u�W�F�N�g��Player�ɐڐG������A���̊֐����Ă�ŃX�R�A�����Z
    /// </summary>
    public void AddScore(int value)
    {
        Score += value;
        ShowText();
    }

    /// <summary>
    /// �I�u�W�F�N�g��Player�ɐڐG������A���̊֐����Ă�ŃX�R�A�����Z
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
