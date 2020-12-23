using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// LevelsList: Handles button management specifically for the LevelsList
/// </summary>
public class LevelsList : ButtonManager
{
    private const float buttonSpacing = 10f;
    private const float maxButtonSize = 50f;
    private const float textToButtonSizeRatio = 0.5f;

    public Canvas listCanvas;
    public GameObject buttonParent;
    public GameObject title;
    public GameObject backButton;
    public GameObject buttonModel;
    public Font buttonFont;

    /// <summary>
    /// Loads buttons on start-up of the scene
    /// </summary>
    public void Start()
    {
        GenerateLevelButtons();
    }

    /// <summary>
    /// Generates all the buttons for the levels using the button prefab
    /// </summary>
    public void GenerateLevelButtons()
    {
        int firstLvlIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int numLevels = SceneManager.sceneCountInBuildSettings - firstLvlIndex - 1;
        buttons = new GameObject[numLevels];

        float titleHeight = title.GetComponent<RectTransform>().rect.height;
        float backButtonHeight = backButton.GetComponent<RectTransform>().rect.height;
        float buttonSize = Math.Min(maxButtonSize, 
            (listCanvas.GetComponent<RectTransform>().rect.height 
                    - titleHeight - backButtonHeight) 
                * listCanvas.GetComponent<RectTransform>().localScale.y
                / (float) Math.Round(Math.Sqrt(numLevels)));

        // Create the buttons and adjust their dimensions
        for (int i = 0; i < numLevels; ++i)
        {
            buttons[i] = Instantiate(buttonModel);
            buttons[i].name = "Lvl" + (i + 1);
                                        // worldPositionStays = false prevent unwanted scaling  
            buttons[i].transform.SetParent(buttonParent.transform, false);
            buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSize, buttonSize);

            Text text = buttons[i].GetComponentInChildren<Text>();
            text.text = (i + 1).ToString();
            text.font = buttonFont;
            text.fontSize = (int) (buttonSize * textToButtonSizeRatio);

            // Delegate parameters are not "saved" on declaration, cannot directly use i + firstLvlIndex
            int buildIndex = i + firstLvlIndex;
            buttons[i].GetComponent<Button>().onClick.AddListener(delegate { ToScene(buildIndex); });

            // TODO: turn off interactable for certain buttons 
            // (later, *need either PlayerPref or Serialiazable)
        }

        // Format buttons
        Vector2 center = listCanvas.GetComponent<RectTransform>().rect.center;
        center.y += (backButtonHeight - titleHeight) / 2;
        FormatSquare(center, buttonSpacing);
    }
}
