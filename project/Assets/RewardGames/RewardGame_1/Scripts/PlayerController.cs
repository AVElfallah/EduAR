using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public Joystick joystick;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float gravity = -9.10f;
    [SerializeField] private float crouchSpeedModifier = 0.5f;
    [SerializeField] private float slideSpeedModifier = 2f;
    [SerializeField] private float slideDuration = 1f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string forwardAnimParam = "forward";
    [SerializeField] private string strafeAnimParam = "strafe";
    [SerializeField] private string slideAnimParam = "slide";
    [SerializeField] private string crouchAnimParam = "crouch";

    [Header("UI")]
    [SerializeField] private Text scoreText;

    [Header("Audio")]
    [SerializeField] private AudioClip collectionSound;
    [SerializeField] private AudioSource audioSource;

    [Header("Level")]
    [SerializeField] private GameObject level2;
    [SerializeField] private GameObject level3;

    private CharacterController controller;
    private Camera myCamera;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching;
    private bool isSliding;
    private float slideTimer;

    private int scoreCounter;
    public int ScoreCounter => scoreCounter;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        var hSv = joystick.Direction;
        float horizontalInput = hSv.x/* Input.GetAxis("Horizontal") */;
        float verticalInput = hSv.y /* Input.GetAxis("Vertical") */;

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
        float speedModifier = isCrouching ? crouchSpeedModifier : isSliding ? slideSpeedModifier : isRunning ? 2f : 1f;

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + myCamera.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            float targetSpeed = maxSpeed * speedModifier;
            float currentSpeed = Vector3.Dot(velocity, transform.forward);
            float speed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
            velocity = transform.forward * speed;

            animator.SetFloat(forwardAnimParam, speed / maxSpeed);
            animator.SetFloat(strafeAnimParam, horizontalInput);
        }
        else
        {
            animator.SetFloat(forwardAnimParam, 0f);
            animator.SetFloat(strafeAnimParam, 0f);

            float currentSpeed = Vector3.Dot(velocity, transform.forward);
            float speed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);
            velocity = transform.forward * speed;
        }

        if (isGrounded)
        {
            animator.SetBool(slideAnimParam, false);
            slideTimer = 0f;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = true;
                animator.SetBool(crouchAnimParam, true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                isCrouching = false;
                animator.SetBool(crouchAnimParam, false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
            {
                isSliding = true;
                animator.SetBool(slideAnimParam, true);
            }

            if (isSliding)
            {
                slideTimer += Time.deltaTime;

                if (slideTimer >= slideDuration)
                {
                    isSliding = false;
                    animator.SetBool(slideAnimParam, false);
                }
            }

            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);

        if (this.transform.position.y < -1)
        {
            FindObjectOfType<GameMaster>().EndGame();
            scoreText.text = "You lose";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            scoreCounter++;
            scoreText.text = "Score" + " " + scoreCounter.ToString();
            audioSource.PlayOneShot(collectionSound);
            if (scoreCounter == 18)
            {
                level2.gameObject.SetActive(true);
            }
            if (scoreCounter == 34)
            {
                level3.gameObject.SetActive(true);
            }
            if (scoreCounter == 2)
            {

                // Zoom in the player
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, this.transform.position, 20 * Time.deltaTime);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, 25 * Time.deltaTime);

                // Show "You Win" text
                //  scoreText.text = "You Win!";


            }
        }
    }
}