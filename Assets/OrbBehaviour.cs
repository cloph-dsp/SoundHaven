using UnityEngine;

public class OrbBehavior : MonoBehaviour
{
    public GameObject orbObject;
    private Transform targetTransform;

    public void SetVolume(float volume)
    {
        AudioSource audioSource = orbObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public void SetPitch(float pitch)
    {
        AudioSource audioSource = orbObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.pitch = pitch;
        }
    }
}
