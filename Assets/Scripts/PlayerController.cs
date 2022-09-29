using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{
    public static event Predicate<Vector3> DontMoving; 
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform footPosition;


    private CharacterController _characterController;
    private Vector3 _direction;
    private float _gravity;
    private Vector3 _fallVelocity;
    private PhotonView _photonView;
    private PlayerGraphic _playerGraphic;
    private bool _isGrounded => IsGrounded();
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _photonView = GetComponent<PhotonView>();
        _direction = Vector3.zero;
        _gravity = Physics.gravity.y;
        _playerGraphic = GetComponent<PlayerGraphic>();
    }

    private void Start()
    {
        if (_photonView.isMine == false)
        {
            Destroy(gameObject.GetComponentInChildren<Camera>().gameObject);
            _playerGraphic.ChangeModel();   
        }
    }

    private void Update()
    {
        if (_photonView.isMine)
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.z = Input.GetAxis("Vertical");
            Move();
        }
    }

    private void Move()
    {
        Fall();
        float speed = runSpeed;
        
        if (Input.GetKey(KeyCode.LeftControl))
            speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = shiftSpeed;
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
        
        DontMoving?.Invoke(_direction);
        
        _characterController.Move(_direction * speed * Time.deltaTime);
        _playerGraphic.SetDirection(_direction*speed);
    }

    private void Fall()
    {
        if (_isGrounded && _fallVelocity.y <= 0)
        {
            _fallVelocity.y = 0;
        }
        else
        {
            _fallVelocity.y += _gravity * Time.deltaTime;
        }

        _characterController.Move(_fallVelocity*Time.deltaTime);
        
    }

    private void Jump()
    {
        _fallVelocity.y = Mathf.Sqrt(jumpHeight * -1 * _gravity);
    }
    
    private bool IsGrounded() => Physics.CheckSphere(footPosition.position, groundDistance, groundMask);
    
}
