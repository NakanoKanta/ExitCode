using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _playerSpeed = 2f;
    private Rigidbody _rb;
    [SerializeField] public bool _isMoving = true;

    Animator _animator;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isMoving)
        {
            Move();
        }
        Anima();
    }
    
    void Move() //移動処理
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x,0,z);
        //transform.Translate(move * _playerSpeed * Time.deltaTime);
        _rb.MovePosition(_rb.position + move * _playerSpeed * Time.fixedDeltaTime);
    }

    void Anima() //アニメーションの処理
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float speed = new Vector2(x, z).magnitude;

        _animator.SetFloat("Speed", speed);
    }
}
