using UnityEngine;
using UnityEngine.UI;

public class PlayPauseButton : MonoBehaviour
{
    public Sprite playIcon;
    public Sprite pauseIcon;

    void Start()
    {
        PositionAndResizeButton();
        Image buttonImage = GetComponent<Image>();
        buttonImage.sprite = playIcon;
    }

    void PositionAndResizeButton()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0); // Âncora no canto inferior esquerdo
        rectTransform.anchorMax = new Vector2(0, 0); // Âncora no canto inferior esquerdo
        rectTransform.pivot = new Vector2(0, 0); // Pivô no canto inferior esquerdo

        // Calcular posição e tamanho relativo à tela
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Definir a posição com uma distância maior das bordas
        float borderOffsetX = screenWidth * 0.05f; // 5% da largura da tela
        float borderOffsetY = screenHeight * 0.05f; // 5% da altura da tela

        // Definir o tamanho relativo à tela
        float buttonWidth = screenWidth * 0.1f; // 10% da largura da tela
        float buttonHeight = screenHeight * 0.1f; // 10% da altura da tela

        rectTransform.anchoredPosition = new Vector2(borderOffsetX, borderOffsetY);
        rectTransform.sizeDelta = new Vector2(buttonWidth, buttonHeight);
    }

    public void ToggleIcon()
    {
        Image buttonImage = GetComponent<Image>();
        if (buttonImage != null && playIcon != null && pauseIcon != null)
        {
            if (buttonImage.sprite == playIcon)
            {
                buttonImage.sprite = pauseIcon;
                // Lógica de pausa
            }
            else
            {
                buttonImage.sprite = playIcon;
                // Lógica de reprodução
            }
        }
    }

}
