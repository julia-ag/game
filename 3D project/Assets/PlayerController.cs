using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody myBody;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    private float horizontalInput, jumpInput;

    public float charSpeed, charJump, gravity, scale;

    [SerializeField] float checkRadius;
    public GameObject checkGround;

    public Animator myAnim;
    private string currentState;

    public TMP_Text coinsText;
    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");

        Animations();

        isGrounded = Physics.CheckSphere(checkGround.transform.position, checkRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }
    private void FixedUpdate()
    {
        if (jumpInput > 0 && isGrounded)
        {
            myBody.AddForce(transform.up * charJump, ForceMode.Impulse);
        }

        if (!isGrounded)
        {
            myBody.velocity = new Vector3(charSpeed * horizontalInput, myBody.velocity.y - gravity, myBody.velocity.z);
        }
        else
        {
            myBody.velocity = new Vector3(charSpeed * horizontalInput, myBody.velocity.y, myBody.velocity.z);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(checkGround.transform.position, checkRadius);
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
        if (horizontalInput == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if (horizontalInput == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        myAnim.Play(newState);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
