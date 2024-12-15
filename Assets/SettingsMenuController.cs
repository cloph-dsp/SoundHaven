using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    private bool menuVisible = false;

    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        Debug.Log("ToggleMenu called, menuVisible: " + menuVisible);
        gameObject.SetActive(menuVisible);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuVisible)
            {
                ClosePanel();
            }
        }
    }
}
