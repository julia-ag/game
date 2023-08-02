using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public Rigidbody myBody;
    public CharacterController myChar;
    [SerializeField] LayerMask groundLayer;
    float gravity = -50f;
    Vector3 velocity;
    [SerializeField] bool isGrounded
    float horizontalInput, jumpInput;
    float charSpeed, charJump;
    [SerializeField] float checkRadius;
    public GameObjact checkGround;
    public Animator myAnim;
    horizontalInput = Input.GetAxis("Horizontal");
    jumpInput = Input.GetAxis("Jump");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);
        isGrounded = Physics.CheckSphere(checkGround.transform.position, checkRadius, QueryTriggerInteraction.ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        charControl.Move(new Vector3(horizontalInput * charSpeed, 0, 0) * Time.deltaTime);

        if(isGrounded && jumpInput . 0)
        {
         
        }
    }

    void FixedUpdate()
    {

    }
}
