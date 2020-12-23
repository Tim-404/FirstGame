using UnityEngine;

/// <summary>
/// EndTrigger: Detects when the player has succesfully reached the end of a level
/// </summary>
public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;

    /// <summary>
    /// Ends the game when the player completes the level
    /// </summary>
    private void OnTriggerEnter()
    {
        gameManager.CompleteLevel();
    }
}
