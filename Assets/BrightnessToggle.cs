using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BrightnessToggle : MonoBehaviour, IPointerClickHandler
{
    private Image imageComponent;
    public Color offColor = new Color(0, 1, 1);
    public Color onColor = new Color(0.9f, 0.9f, 0.9f);
    private RectTransform rectTransform;
    private Canvas parentCanvas;
    public float xOffset = 60f;
    public float yOffset = 60f;
    public float dimAmount = 0.2f; // 20% darker
    public Slider dimSlider; // Slider UI for dimness adjustment

    public bool IsBrightnessDimmed { get; private set; } = false;

    public Image dimmingPanel; // UI Image that covers the entire screen for dimming

    void Start()
    {
        imageComponent = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();

        imageComponent.color = onColor;

        // Set the anchor and pivot points to the top-right of the parent container
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);

        // Set the position offset from the anchor point
        rectTransform.anchoredPosition = new Vector2(-xOffset, -yOffset);

        // Start with dimming panel fully transparent
        dimmingPanel.color = new Color(0, 0, 0, 0);

        // Set initial slider value
        dimSlider.value = dimAmount;

        // Add listener to dimSlider
        dimSlider.onValueChanged.AddListener(AdjustDimness);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleBrightness();
    }

    public void ToggleBrightness()
    {
        IsBrightnessDimmed = !IsBrightnessDimmed;

        // If the button is deactivated, the dimness can still be adjusted by the slider
        if (IsBrightnessDimmed)
        {
            imageComponent.color = offColor;
        }
        else
        {
            imageComponent.color = onColor;
        }

        // Call AdjustDimness() to update the dimness based on the current slider value
        AdjustDimness(dimSlider.value);
    }

    public void AdjustDimness(float newDimAmount)
    {
        // Map the linear slider value (0-1) to a sigmoid function
        dimAmount = 1 / (1 + Mathf.Exp(-12 * (newDimAmount - 0.5f)));

        // If the brightness is currently dimmed, update the dimming panel color
        if (IsBrightnessDimmed)
        {
            dimmingPanel.color = new Color(0, 0, 0, dimAmount);
        }
        else
        {
            dimmingPanel.color = new Color(0, 0, 0, 0);
        }
    }
}
