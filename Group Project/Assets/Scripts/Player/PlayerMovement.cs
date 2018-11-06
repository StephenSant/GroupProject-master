using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float jumpHeight;
    public bool isGrounded;
    public LayerMask groundLayer;

    public float runSpeed;
    public float walkSpeed;
    public float sneakSpeed;
    public MoveSpeed moveSpeed;

    Vector3 moveDir;
    Vector3 spawnPoint;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Debug.Log("Went out of bounds.");
            transform.position = spawnPoint;
        }

        Vector3 camEuler = Camera.main.transform.eulerAngles;
        moveDir = Quaternion.AngleAxis(camEuler.y, Vector3.up) * moveDir;
        Vector3 force = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
        rb.velocity = force;
        Quaternion playerRotation = Quaternion.AngleAxis(camEuler.y, Vector3.up);
        transform.rotation = playerRotation;

        //transform.rotation = playerRotation;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = MoveSpeed.run;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = MoveSpeed.sneak;
        }
        else
        {
            moveSpeed = MoveSpeed.walk;
        }

        switch (moveSpeed)
        {
            case MoveSpeed.run:
                moveDir = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * runSpeed);
                break;
            case MoveSpeed.sneak:
                moveDir = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * sneakSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * sneakSpeed);
                break;
            default:
                moveDir = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed, rb.velocity.y, Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed);
                break;

        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        }
    }
    public enum MoveSpeed
    {
        run,
        walk,
        sneak
    }
}
