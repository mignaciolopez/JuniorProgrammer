using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gameManager;
    float speed = 0f;
    [SerializeField] const float walkingSpeed = 3f;
    [SerializeField] const float runningSpeed = 6f;

    Vector3 move = Vector3.zero;
    CharacterController characterController;

    Vector3 velocity = Vector3.zero;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.01f;
    [SerializeField] LayerMask groundMask;
    //[SerializeField] LayerMask interactiveMask;
    bool grounded;

    [SerializeField] float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterController = GetComponent<CharacterController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (grounded && velocity.y < 0f)
                velocity.y = 0f;

            if (Input.GetButton("Jump") && grounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                speed = runningSpeed;
            else
                speed = walkingSpeed;

            float x = Input.GetAxis("Horizontal") * Time.deltaTime;
            float z = Input.GetAxis("Vertical") * Time.deltaTime;

            move = transform.right * x + transform.forward * z;
            if (move.magnitude > 1)
                move /= move.magnitude;

            characterController.Move(move * speed);
        }
    }
}
