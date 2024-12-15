using UnityEngine;

public class QualityToggle : MonoBehaviour
{
    private int mediumQualityIndex;
    private int lowQualityIndex;
    private bool isMediumQuality = true;

    void Start()
    {
        // Find quality indices
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            if (QualitySettings.names[i].Equals("Medium"))
                mediumQualityIndex = i;
            if (QualitySettings.names[i].Equals("Low"))
                lowQualityIndex = i;
        }

        // Set initial quality
        QualitySettings.SetQualityLevel(mediumQualityIndex, true);
    }

    public void ToggleQuality()
    {
        if (isMediumQuality)
        {
            QualitySettings.SetQualityLevel(lowQualityIndex, true);
            isMediumQuality = false;
        }
        else
        {
            QualitySettings.SetQualityLevel(mediumQualityIndex, true);
            isMediumQuality = true;
        }
    }
}
