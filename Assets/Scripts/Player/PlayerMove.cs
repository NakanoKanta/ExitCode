using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 2f;
    private Rigidbody _rb;
    [SerializeField] public bool _isMoving = true;

    Animator _animator;
    private Vector3 move;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isMoving)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            move = new Vector3(x, 0, z);
            Move();
        }
        Anima();
    }
    
    void Move() //移動処理
    {
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }
        _rb.MovePosition(_rb.position + move * _playerSpeed * Time.fixedDeltaTime);
    }

    void Anima() //アニメーションの処理
    {
        float speed = move.magnitude;

        _animator.SetFloat("Speed", speed);
    }
}
