using AudioHelm;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlaceSoundController : MonoBehaviour
{
    public Button placeButton;
    public Button loadButton;
    public Button recordButton;
    public Button synthesizeButton;
    public GameObject orbPrefab;
    public RectTransform uiElement;
    public Canvas controlledCanvas;
    public float xOffset = 0.0f;

    private string audioFilePath;
    private AudioClip loadedAudioClip;
    private bool isRecording = false;
    private GameObject controlledCanvasGameObject;

    public float recordingDuration = 10.0f; // Duration of recording in seconds
    private Color originalRecordButtonColor; // Store original color of the record button

    void Start()
    {
        placeButton.onClick.AddListener(HandlePlaceButtonClick);
        loadButton.onClick.AddListener(HandleLoadButtonClick);
        recordButton.onClick.AddListener(HandleRecordButtonClick);
        originalRecordButtonColor = recordButton.GetComponent<Image>().color;
        synthesizeButton.onClick.AddListener(HandleSynthesizeButtonClick);


        if (controlledCanvas == null)
        {
            controlledCanvas = GetComponent<Canvas>();
            if (controlledCanvas == null)
            {
                Debug.LogError("No Canvas found in the scene.");
                return;
            }
        }

        controlledCanvasGameObject = controlledCanvas.gameObject;

        PositionPlaceButton();
    }

    void PositionPlaceButton()
    {
        Canvas parentCanvas = placeButton.GetComponentInParent<Canvas>();

        if (parentCanvas != null)
        {
            RectTransform buttonRectTransform = placeButton.GetComponent<RectTransform>();

            // Place the button on the bottom-right of the screen
            buttonRectTransform.anchorMin = new Vector2(1, 0);
            buttonRectTransform.anchorMax = new Vector2(1, 0);
            buttonRectTransform.pivot = new Vector2(1, 0);

            // Calculate and set button's height
            float buttonHeight = parentCanvas.pixelRect.height * 0.20f; // 20% of screen height
            buttonRectTransform.sizeDelta = new Vector2(buttonRectTransform.sizeDelta.x, buttonHeight);

            // Calculate and set button's position
            float offsetX = parentCanvas.pixelRect.width * 0.05f; // 5% of screen width
            float offsetY = parentCanvas.pixelRect.height * 0.05f; // 5% of screen height
            buttonRectTransform.anchoredPosition = new Vector2(-offsetX, offsetY);
        }
    }


    void HandlePlaceButtonClick()
    {
        if (controlledCanvas != null)
        {
            // Ajusta o tamanho do canvas
            AdjustCanvasSize();

            // Ajusta a posição do canvas
            AdjustCanvasPosition();

            // Ativa ou desativa o canvas
            controlledCanvas.gameObject.SetActive(!controlledCanvas.gameObject.activeInHierarchy);

            // Logs para debug
            Debug.Log("New state of the GameObject: " + (controlledCanvas.gameObject.activeInHierarchy ? "active" : "inactive"));
        }
        else
        {
            Debug.LogError("controlledCanvas is null.");
        }
    }

    void AdjustCanvasSize()
    {
        RectTransform canvasRectTransform = controlledCanvasGameObject.GetComponent<RectTransform>();
        canvasRectTransform.sizeDelta = new Vector2(Screen.width * 0.35f, Screen.height * 0.35f);
    }

    void AdjustCanvasPosition()
    {
        // Obtenha o RectTransform do canvas
        RectTransform canvasRectTransform = controlledCanvasGameObject.GetComponent<RectTransform>();

        // Configure o pivot para o centro, se ainda não estiver
        canvasRectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Posicione o canvas ligeiramente à esquerda do centro da tela
        canvasRectTransform.anchoredPosition = new Vector2(-200, 0);
    }



    void HandleLoadButtonClick()
    {
        if (NativeFilePicker.IsFilePickerBusy())
        {
            Debug.LogWarning("File picker is busy!");
            return;
        }

        string[] allowedFileTypes = new string[] { "audio/wav", "audio/mpeg" };

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((string result) =>
        {
            if (!string.IsNullOrEmpty(result))
            {
                string fileName = Path.GetFileName(result);
                string filePath = result;
                Debug.Log("Picked file: " + fileName + ", Path: " + filePath);
                StartCoroutine(LoadAudioClip(filePath));
            }
            else
            {
                Debug.LogWarning("No file was picked!");
            }
        }, allowedFileTypes);

        if (permission == NativeFilePicker.Permission.Denied)
        {
            Debug.LogError("User denied permission to access files!");
        }
    }

    void HandleRecordButtonClick()
    {
        if (isRecording)
        {
            Microphone.End(null);
            isRecording = false;
            PlaceSound();
            // Change button color back to original
            recordButton.GetComponent<Image>().color = originalRecordButtonColor;
        }
        else
        {
            loadedAudioClip = Microphone.Start(null, false, Mathf.FloorToInt(recordingDuration), 44100);
            isRecording = true;
            // Change button color to red
            recordButton.GetComponent<Image>().color = Color.red;
        }
    }

    void HandleSynthesizeButtonClick()
    {
        GameObject synthObject = new GameObject("Synthesizer");
        HelmController helmController = synthObject.AddComponent<HelmController>();

        AudioSource audioSource = synthObject.AddComponent<AudioSource>();

        helmController.GetParameterValue(AudioHelm.Param.kOsc1Waveform);
        helmController.SetParameterValue(AudioHelm.Param.kOsc1Waveform, 1.0f);
        helmController.SetParameterValue(AudioHelm.Param.kOsc1Transpose, 12.0f);

        GameObject orbInstance = Instantiate(orbPrefab, Camera.main.transform.position + Camera.main.transform.forward * 2, Quaternion.identity);

        AudioSource orbAudioSource = orbInstance.AddComponent<AudioSource>();
        orbAudioSource.clip = audioSource.clip;

        orbAudioSource.spatialBlend = 1.0f;
        orbAudioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        orbAudioSource.minDistance = 1.0f;
        orbAudioSource.maxDistance = 15.0f;

        orbAudioSource.Play();
    }

    IEnumerator LoadAudioClip(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load audio clip: " + www.error);
            }
            else
            {
                loadedAudioClip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log("Audio clip loaded successfully!");
                audioFilePath = filePath;
                PlaceSound();
            }
        }
    }

    void PlaceSound()
    {
        if (loadedAudioClip == null)
        {
            Debug.LogWarning("No audio clip has been loaded!");
            return;
        }

        GameObject orbInstance = Instantiate(orbPrefab, Camera.main.transform.position + Camera.main.transform.forward * 2, Quaternion.identity);
        AudioSource audioSource = orbInstance.GetComponent<AudioSource>();
        audioSource.clip = loadedAudioClip;

        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.minDistance = 1.0f;
        audioSource.maxDistance = 15.0f;

        audioSource.Play();
    }
}