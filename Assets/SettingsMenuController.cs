using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    private bool menuVisible = false;

    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        Debug.Log("ToggleMenu called, menuVisible: " + menuVisible); // Debug message
        gameObject.SetActive(menuVisible);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    void Update() // Step 1: Override Update method
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Step 2: Check for back button press
        {
            if (menuVisible) // Step 3: If back button is pressed and panel is visible
            {
                ClosePanel();
            }
        }
    }
}
