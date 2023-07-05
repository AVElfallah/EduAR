using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public AudioClip jumpClip;
    public AudioClip gameOverClip;
    public AudioSource muiscAudioSource;

    public Button jumpButton;
    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    private bool isJumping = false;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }
    public void Start()
    {
        jumpButton.onClick.AddListener(Jump);
    }
    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (isJumping)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                direction = Vector3.up * jumpForce;
                isJumping = false;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void Jump()
    {

        isJumping = true;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            muiscAudioSource.Stop();
            AudioSource.PlayClipAtPoint(gameOverClip, transform.position, 2f);
            //  jumpButton.onClick.RemoveListener(Jump);
            jumpButton.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}
