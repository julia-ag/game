using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    private float gravity = -50f;
    private CharacterController charControl;
    private Vector3 velocity;
    [SerializeField] bool isGrounded;
    private float horizontalInput, jumpInput;
    public float charSpeed, charJump;
    [SerializeField] float checkRadius;
    public GameObject checkGround;
    public Animator myAnim;
    public string currentState;
    public TMP_Text coinsText;
    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        charControl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        Animations();
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        isGrounded = Physics.CheckSphere(checkGround.transform.position, checkRadius, groundLayer, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        charControl.Move(new Vector3(horizontalInput * charSpeed, 0, 0) * Time.deltaTime);

        if (isGrounded && jumpInput > 0)
        {
            velocity.y += Mathf.Sqrt(charJump * -2 * gravity);
        }

        charControl.Move(velocity * Time.deltaTime);
    }
    private void Animations()
    {
        if (isGrounded && horizontalInput != 0)
        {
            ChangeAnimationState("Running");
        }
        if (isGrounded && horizontalInput == 0)
        {
            ChangeAnimationState("Idle");
        }
        if (!isGrounded)
        {
            ChangeAnimationState("Jumping");
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        myAnim.Play(newState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
            coinsText.text = "Moedas Coletadas: " + coins;
        }
    }
}