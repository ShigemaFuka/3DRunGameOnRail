using UnityEngine;

public class RotationY : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = default;

    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
    }
}
