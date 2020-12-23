using UnityEngine;

/// <summary>
/// FollowPlayer: Is attached to the camera to follow the player
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    /// <summary>
    /// Updates the position of the camera
    /// </summary>
    private void Update()
    {
        this.transform.position = player.position + offset;
    }
}
