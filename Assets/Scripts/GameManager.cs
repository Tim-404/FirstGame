using UnityEngine;

/// <summary>
/// GameManager: handles all events where the player crashes or completes a level.
/// The player will be taken to a screen where they can choose to advance, retry or quit.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Used for "effects"
    public PlayerMovement movement;
    public Rigidbody rb;
    public Score scoreCounter;
    public FollowPlayer camera;

    public float slomoTimeScale = 0.25f;

    // These GameObjects control how the UI appears after closing the level
    public GameObject closeLevelUI;
    public GameObject completeText;
    public GameObject failedText;
    public GameObject nextLevelButton;

    // Keeps EndGame() from executing multiple times in the event of multiple collisions
    bool gameHasEnded = false;

    /// <summary>
    /// Closes the level and indicates that the level was completed successfully
    /// </summary>
    public void CompleteLevel()
    {
        if (!gameHasEnded)
        {
            CloseLevel(true);
        }
    }

    /// <summary>
    /// Closes the level and indicates that the player died
    /// </summary>
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER!");

            // slomo effect and restart game
            DilateTime(slomoTimeScale);
            CloseLevel(false);
            float animLen = closeLevelUI.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length;
            Invoke("RestoreTimeDilation", animLen * slomoTimeScale);
        }
    }

    private void RestoreTimeDilation()
    {
        DilateTime(1 / slomoTimeScale);
    }

    /// <summary>
    /// Disables player input and sets up the end screen from which the player can choose where
    /// to go next
    /// </summary>
    /// <param name="success">
    /// Indicates whether the level was completed successfully or not
    /// </param>
    private void CloseLevel(bool success)
    {
        DisablePlayerInteractions();
        closeLevelUI.SetActive(true);
        completeText.SetActive(success);
        failedText.SetActive(!success);
        nextLevelButton.SetActive(success);
    }

    /// <summary>
    /// Disables player input
    /// </summary>
    private void DisablePlayerInteractions()
    {
        movement.enabled = false;
        rb.useGravity = false;
        scoreCounter.enabled = false;
        camera.enabled = false;
    }

    /// <summary>
    /// Dilates time by a given amount
    /// </summary>
    /// <param name="timeScale">The time dilation factor</param>
    private void DilateTime(float timeScale)
    {
        Time.timeScale *= timeScale;
        Time.fixedDeltaTime *= timeScale;
    }
}
