using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float turnSpeed = 10;
    public float sprintMultiplier = 2;
    public float jumpStrength = 50;
    public float jetpackForce = 100;
    public float groundRadius = .2f;
    public float drag = 6;
    public float animSmoothTime;
    public Animator anim;
    public Transform groundCheck;
    public LayerMask groundMask;
    public ParticleSystem particles;

    public PlanetGravity StrongestPlanet = null; //the planet with the most pull on the player
    public float strongestPlanetForce = 0f; //force of the strongest pull on the player

    private bool hasJetpack = false;

    bool isGrounded = false;
    float speedMod;
    Vector2 animationSmoothing,animVelocity;
    float horizontal, vertical;
    Vector3 moveDirect;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SetDrag();
        GetInput();
        AnimUpdate();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius,groundMask);
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDirect = transform.forward * vertical + transform.right * horizontal;

        if (Input.GetKey(KeyCode.LeftShift) && vertical > 0)
        {
            speedMod = sprintMultiplier;
        }
        else
        {
            speedMod = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        else if(Input.GetKey(KeyCode.Space) && !isGrounded && hasJetpack) //jetpack force & particles
        {
            rb.AddForce(jetpackForce * transform.up,ForceMode.Acceleration);
            particles.Play();
        } 
        else if (Input.GetKey(KeyCode.E) && !isGrounded && hasJetpack) 
        {
            rb.AddForce(jetpackForce * (-transform.up), ForceMode.Acceleration);
        }

        if (!Input.GetKey(KeyCode.Space)) //particles turn off if space is not pressed
        {
            particles.Stop();
        }

    }

    public void EquipJetpack()
    {
        hasJetpack = true;
    }

    void Jump()
    {
        anim.Play("Jump");
        rb.AddForce(jumpStrength * transform.up,ForceMode.Impulse);
    }

    void SetDrag()
    {
        rb.drag = drag;
    }

    void Move()
    {
        rb.AddForce(moveDirect.normalized * speed * speedMod * 10, ForceMode.Acceleration);
    }

    void AnimUpdate()
    {
        animationSmoothing = Vector2.SmoothDamp(animationSmoothing, new Vector2(horizontal, vertical), ref animVelocity, animSmoothTime);

        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("x", animationSmoothing.x);
        anim.SetFloat("z", animationSmoothing.y * speedMod);
    }

    private void LateUpdate()
    {
        Vector3 direction = StrongestPlanet.transform.position - transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(-transform.up, direction) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        //Reset strongest force var to 0
        strongestPlanetForce = 0f;
    }
}
