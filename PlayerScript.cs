using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float speed = 15f;
    public float MinSpeed = 15f;
    public float MaxSpeed = 25f;

    public float JumpForce = 5f;

    private bool isWinner;

    // Camara
    public float Sensibility = 2f;
    public float LimitX = 45;
    public Transform cam;

    private float rotationX;
    private float rotationY;

    public bool IsGrounded;

    private Rigidbody rb;

    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento
        if (!isWinner && !UIManager.inst.Pause)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = MaxSpeed;
            }
            else
            {
                speed = MinSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * speed);

            // CAMARA
            rotationX += -Input.GetAxis("Mouse Y") * Sensibility;
            rotationX = Mathf.Clamp(rotationX, -LimitX, LimitX);
            cam.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Sensibility, 0);

            // Pausa
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.inst.ShowPauseScreen();
            }
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }

        if (collision.gameObject.tag == "DeathZone")
        {
            transform.position = SpawnPoint.position;
        }

        if (collision.gameObject.tag == "End" && !isWinner)
        {
            UIManager.inst.ShowWinScreen();
            isWinner = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
