using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 2f;
    private Rigidbody _rb;
    [SerializeField] public bool _isMoving = true;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x,0,z);
        transform.Translate(move * _playerSpeed * Time.deltaTime);
    }
}
