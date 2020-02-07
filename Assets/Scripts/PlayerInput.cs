using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Looking")]
    [SerializeField] Camera cam;
    public Vector2 lookSpeed;
    float vLookScalar;
    Vector2 mouseDelta;

    [Header("Movement")]
    [SerializeField] Rigidbody rBody;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] bool grounded;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] float airControlFactor; //Scalar applied to movement force when airborne
    Vector3 inputVector;

    [Header("Physics")]
    [SerializeField] float drag;
    [SerializeField] float gravity;
    Vector3 velocity;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        #region Looking

        mouseDelta.x = Input.GetAxis("Mouse X");
        mouseDelta.y = Input.GetAxis("Mouse Y");

        vLookScalar = Mathf.Clamp(vLookScalar - mouseDelta.y, -90, 90);

        transform.Rotate(Vector3.up * mouseDelta.x * lookSpeed);
        cam.transform.localEulerAngles = new Vector3(vLookScalar, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);

        #endregion

        #region Movement

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

        grounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayers);
        //Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundCheckDistance), Color.red, 0.2f);

        if(grounded && Input.GetButtonDown("Jump"))
        {
            velocity = rBody.velocity;
            velocity.y = jumpForce;
            rBody.velocity = velocity;
        }

        #endregion
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            rBody.AddRelativeForce(inputVector.normalized * walkSpeed);
            velocity = rBody.velocity;
            velocity.x *= drag;
            velocity.z *= drag;
            rBody.velocity = velocity;
        }
        else
        {
            rBody.AddRelativeForce(inputVector.normalized * walkSpeed * airControlFactor);
            rBody.AddForce(Vector3.down * gravity);
        }
    }
}