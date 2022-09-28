using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PhotonView))]
public class PlayerController : MonoBehaviour
{

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
            _playerGraphic.ChangeColor();   
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
        if (_isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftControl))
                speed = walkSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
                speed = shiftSpeed;
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        
        _characterController.Move(_direction * speed * Time.deltaTime);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(footPosition.position, groundDistance);
    }
}
