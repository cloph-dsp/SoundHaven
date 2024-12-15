using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButtonController : MonoBehaviour
{
    public SettingsMenuController settingsMenuController;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(CloseSettings);
    }

    void CloseSettings()
    {
        Debug.Log("CloseSettings called");
        if (settingsMenuController != null)
        {
            settingsMenuController.ToggleMenu();
        }
    }
}
