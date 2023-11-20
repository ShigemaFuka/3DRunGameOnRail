using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.World);
    }
}
