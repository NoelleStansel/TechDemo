using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float sprintMultiplier = 2;
    public float jumpStrength = 50;
    public float groundRadius = .2f;
    public float drag = 6;
    public float animSmoothTime;
    public Animator anim;
    public Transform groundCheck;
    public LayerMask groundMask;

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
        Debug.Log("V/H" + vertical + " / " + horizontal);
        Debug.Log("A - V/H" + animationSmoothing.x + " / " + animationSmoothing.y);
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

}
