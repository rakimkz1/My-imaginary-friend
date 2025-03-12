using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public Vector3 gravity;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    [SerializeField] private LayerMask checkLayer;
    private CharacterController _chr;
    private Vector3 _gravityVelocity = Vector3.zero;

    private bool _isGrounded;
    void Start()
    {
        _chr = GetComponent<CharacterController>();
    }
    void Update()
    {
        Movement();
        Gravity();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal"), y = Input.GetAxis("Vertical");

        Vector3 diraction = (transform.right * x + transform.forward * y) * Speed * Time.deltaTime;
        _chr.Move(diraction);

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            float gx = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(gravity.x)), gy = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(gravity.y)), gz = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(gravity.z));
            _gravityVelocity.y = gy * gravity.normalized.y * -1f;
        }
    }

    void Gravity()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, checkLayer);
        if (_isGrounded && gravity.normalized == _gravityVelocity.normalized)
        {
            _gravityVelocity = gravity.normalized * 3f;
        }
        else
        {
            _gravityVelocity += gravity * Time.deltaTime;
        }
        
        _chr.Move(_gravityVelocity * Time.deltaTime);
    }
}
