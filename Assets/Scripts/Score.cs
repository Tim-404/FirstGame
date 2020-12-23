using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Score: Keeps track of the distance the player has traveled in the current level
/// </summary>
public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;

    /// <summary>
    /// Updates the score based on the player z position
    /// </summary>
    private void Update()
    {
        scoreText.text = player.position.z.ToString("0");
    }
}
