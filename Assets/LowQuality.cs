using UnityEngine;

public class QualityToggle : MonoBehaviour
{
    private int mediumQualityIndex;
    private int lowQualityIndex;
    private bool isMediumQuality = true;

    void Start()
    {
        // Encontrar os índices para os níveis de qualidade
        // É importante garantir que estes índices correspondam aos níveis de qualidade definidos nas Quality Settings
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            if (QualitySettings.names[i].Equals("Medium"))
                mediumQualityIndex = i;
            if (QualitySettings.names[i].Equals("Low"))
                lowQualityIndex = i;
        }

        // Definir a qualidade inicial como "Medium"
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
