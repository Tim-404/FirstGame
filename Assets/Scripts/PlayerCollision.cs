using UnityEngine;

/// <summary>
/// PlayerCollision: Detects and acts on player collisions with certain course objects
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement player;

    /// <summary>
    /// Acts on player collisions
    /// </summary>
    /// <remarks>
    /// Ends the game if the player hits an obstacle.
    /// Sets player.grounded to true when the player hits the ground.
    /// </remarks>
    /// <param name="collisionInfo"></param>
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        if (collisionInfo.collider.tag == "Ground")
        {
            player.grounded = true;
        }
    }
}
