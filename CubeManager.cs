using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour
{
    public float initialSpeed = 5f;  // Set an initial speed
    private float currentSpeed;
    public float horizontalSpeed = 5f;  // Adjusted horizontal speed
    public float speedIncreaseRate = 0.1f;  // Gradual speed increase over time
    public float maxSpeed = 20f;  // Maximum speed cap
    public float jumpForce = 10f;  // Adjusted jump force
    public float slideDuration = 1f;  // Duration of the slide
    private bool isGrounded = true;
    private bool isSliding = false;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        currentSpeed = initialSpeed;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Start the game directly in Running state
        animator.SetBool("isRunning", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isSliding", false);
    }

    void Update()
    {
        MoveForward();
        MoveHorizontal();
        HandleAnimations();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Hurdle"))
        {
            print("GameOver");
            SceneManager.LoadScene("SampleScene");
        }

        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Detection"))
        {
            print("Hit Detection and GameOver");
            SceneManager.LoadScene("SampleScene");
        }
        else if (col.gameObject.CompareTag("Coin"))
        {
            ScoreManager.instance.AddScore(1);
            Destroy(col.gameObject);
        }
    }

    void MoveHorizontal()
    {
        float xInput = Input.GetAxis("Horizontal");
        transform.Translate(xInput * horizontalSpeed * Time.deltaTime, 0, 0);
    }

    void MoveForward()
    {
        // Ensure character moves forward regardless of the state
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    void HandleAnimations()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && !isSliding)
        {
            isSliding = true;
            animator.SetBool("isSliding", true);
            StartCoroutine(Slide());
        }

        if (isGrounded && !isSliding)
        {
            animator.SetBool("isRunning", true);
        }
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(slideDuration);
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
