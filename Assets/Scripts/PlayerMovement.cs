using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float sidewaysForce = 50f;
    public float jumpForce = 10f;
    public float deathHeight = 0f;

    public string moveRightKey = "d";
    public string moveLeftKey = "a";
    public string jumpKey = "space";

    public bool canJump = false;
    public bool grounded = true;

    // Update is called once per frame
    // Use FixedUpdate for physics, Unity works better this way
    private void FixedUpdate()
    {
        // accelerate forward
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        // Move according to which keys are pressed
        if (Input.GetKey(moveRightKey))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey(moveLeftKey))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (grounded && canJump && Input.GetKey(jumpKey))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            rb.angularVelocity = new Vector3(0, 0, 0);  // keep the ball from spinning
            grounded = false;                           // keeps the player from flying
        }

        // The player dies if they fall off the platform
        if (rb.position.y < deathHeight)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
