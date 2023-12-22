using UnityEngine;

/// <summary>
/// �X�J�C�{�b�N�X�𓮂���
/// </summary>
public class MoveSkybox : MonoBehaviour
{
    [SerializeField, Tooltip("��]�X�s�[�h")] float rotateSpeed = 0.5f;
    [Tooltip("�X�J�C�{�b�N�X�̃}�e���A��")] Material skyboxMaterial;

    void Start()
    {
        //�@Lighting Settings�Ŏw�肵���X�J�C�{�b�N�X�̃}�e���A�����擾
        skyboxMaterial = RenderSettings.skybox;
    }

    void Update()
    {
        //�@�X�J�C�{�b�N�X�}�e���A����Rotation�𑀍삵�Ċp�x��ω�������
        skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotateSpeed * Time.deltaTime, 360f));
    }
}
