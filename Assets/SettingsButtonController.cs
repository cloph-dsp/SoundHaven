using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonController : MonoBehaviour
{
    public SettingsMenuController settingsMenuController;

    // Declare xOffset and yOffset as public variables
    public float xOffset = 0;
    public float yOffset = 0;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(settingsMenuController.ToggleMenu);

        // Get the RectTransform
        RectTransform rectTransform = GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 1); // Top-left anchor
        rectTransform.anchorMax = new Vector2(0, 1); // Top-left anchor
        rectTransform.pivot = new Vector2(0, 1); // Pivot at top-left

        // Apply xOffset and yOffset
        rectTransform.anchoredPosition = new Vector2(xOffset, -yOffset); // Position at the top-left corner with offset
    }
}
