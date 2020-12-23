using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ButtonManager: Deals specifically with changing the properties of buttons
/// </summary>
public class ButtonManager : Navigation
{
    public GameObject[] buttons;

    private struct RowData          // Used to format buttons
    {
        public float thickness;
        public float length;
    }

    /// <summary>
    /// Sets all buttons to interactable
    /// </summary>
    public void EnableButtons()
    {
        foreach (GameObject b in buttons)
        {
            b.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// Organizes all buttons in a square-ish format around a certain point
    /// </summary>
    /// <param name="center">The center of the grid</param>
    public void FormatSquare(Vector2 center, float spacer)
    {
        FormatRect(buttons.Length, center, spacer, 1, 1);
    }

    /// <summary>
    /// Organizes a certain number of buttons in a square-ish format around a certain point.
    /// Sets all the other buttons to inactive
    /// </summary>
    /// <remarks>
    /// This function works for buttons of various sizes, but is restricted to the xy-plane
    /// </remarks>
    /// <param name="numButtons">
    /// The number of buttons to format, should the user want to avoid loading certain buttons
    /// </param>
    /// <param name="center">The center of the grid</param>
    /// <param name="spacer">The space between buttons</param>
    public void FormatSquare(int numButtons, Vector2 center, float spacer)
    {
        FormatRect(numButtons, center, spacer, 1, 1);
    }

    /// <summary>
    /// Organizes all buttons in a square-ish format around a certain point
    /// </summary>
    /// <param name="center">The center of the grid</param>
    /// <param name="spacer">The space between buttons</param>
    /// <param name="relHeight">The relative height of the grid</param>
    /// <param name="relWidth">The relative width of the grid</param>
    public void FormatRect(Vector2 center, float spacer, int relHeight, int relWidth)
    {
        FormatRect(buttons.Length, center, spacer, relHeight, relWidth);
    }

    /// <summary>
    /// Organizes a certain number of buttons in a specified rectangular format around a certain point.
    /// Sets all the other buttons to inactive
    /// </summary>
    /// <param name="numButtons">
    /// The number of buttons to format, should the user want to avoid loading certain buttons
    /// </param>
    /// <param name="center">The center of the grid</param>
    /// <param name="spacer">The space between buttons</param>
    /// <param name="relHeight">The relative height of the grid</param>
    /// <param name="relWidth">The relative width of the grid</param>
    public void FormatRect(int numButtons, Vector2 center, float spacer, int relHeight, int relWidth)
    {
        if (buttons.Length > 0 && relWidth > 0 && relHeight > 0)
        {
            numButtons = Math.Min(numButtons, buttons.Length);
            float scale = (float) Math.Sqrt((float) numButtons / relWidth / relHeight);
            int gridHeight = (int) Math.Round(relHeight * scale);
            int gridWidth = (int) Math.Ceiling((float) numButtons / gridHeight);

            GameObject[][] grid = new GameObject[gridHeight][];
            RowData[] rows = new RowData[gridHeight];
            float totalHeight = 0;
            int i = 0;

            // Add all buttons to a jagged 2D array, positions will be sorted out later
            for (int row = 0; row < gridHeight; ++row)
            {
                int arrLen = Math.Min(numButtons - i, gridWidth);
                GameObject[] arr = new GameObject[arrLen];

                float tallest = 0;    // Keep track of tallest button to add to rowThickness[]

                for (int col = 0; col < arrLen; ++col)
                {
                    arr[col] = buttons[i];

                    // Update button format info
                    RectTransform transform = buttons[i++].GetComponent<RectTransform>();
                    tallest = Math.Max(transform.sizeDelta.y + spacer, tallest);
                    rows[row].length += transform.sizeDelta.x + spacer;
                }

                grid[row] = arr;

                // Update button format info
                rows[row].thickness = tallest;
                totalHeight += tallest;
            }

            // Format all the buttons
            float height = center.y + totalHeight / 2;
            for (int row = 0; row < gridHeight; ++row)
            {
                height -= rows[row].thickness / 2;
                float xPos = center.x - rows[row].length / 2;

                foreach (GameObject b in grid[row])
                {
                    RectTransform transform = b.GetComponent<RectTransform>();
                    float xSpacing = (spacer + transform.sizeDelta.x) / 2;
                    xPos += xSpacing;
                    transform.anchoredPosition = new Vector2(xPos, height);
                    xPos += xSpacing;
                }
                height -= rows[row].thickness / 2;
            }

            // Set remaining buttons to inactive
            for (; i < buttons.Length; ++i)
            {
                buttons[i].SetActive(false);
            }
        }    
    }
}
