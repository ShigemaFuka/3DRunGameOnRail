using UnityEngine;

/// <summary>
/// スカイボックスを動かす
/// </summary>
public class MoveSkybox : MonoBehaviour
{
    [SerializeField, Tooltip("回転スピード")] float rotateSpeed = 0.5f;
    [Tooltip("スカイボックスのマテリアル")] Material skyboxMaterial;

    void Start()
    {
        //　Lighting Settingsで指定したスカイボックスのマテリアルを取得
        skyboxMaterial = RenderSettings.skybox;
    }

    void Update()
    {
        //　スカイボックスマテリアルのRotationを操作して角度を変化させる
        skyboxMaterial.SetFloat("_Rotation", Mathf.Repeat(skyboxMaterial.GetFloat("_Rotation") + rotateSpeed * Time.deltaTime, 360f));
    }
}
