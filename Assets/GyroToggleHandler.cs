using UnityEngine;
using Lean.Gui;

public class LeanGyroToggleHandler : MonoBehaviour
{
    public LeanToggle leanGyroToggle;
    public TouchCameraControl touchCameraControl;

    void Start()
    {
        if (leanGyroToggle != null)
        {
            leanGyroToggle.On = touchCameraControl.useLerpForGyro; // Initial state of the toggle according to useLerpForGyro
            leanGyroToggle.OnOn.AddListener(OnLeanGyroToggleOn);
            leanGyroToggle.OnOff.AddListener(OnLeanGyroToggleOff);
        }
    }

    void OnLeanGyroToggleOn()
    {
        touchCameraControl.ToggleGyroInterpolation();
    }

    void OnLeanGyroToggleOff()
    {
        touchCameraControl.ToggleGyroInterpolation();
    }
}
