using UnityEngine;
using System.Collections.Generic;

// Sound class to store sound properties
public class Sound
{
    private AudioClip clip;
    private Vector3 position;
    private float volume;
    private float pitch;
    private GameObject orbObject;

    public Sound(AudioClip clip, Vector3 position, float volume, float pitch, GameObject orbObject)
    {
        this.clip = clip;
        this.position = position;
        this.volume = volume;
        this.pitch = pitch;
        this.orbObject = orbObject;
    }

    public AudioClip GetClip()
    {
        return clip;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetPitch()
    {
        return pitch;
    }

    public GameObject GetOrbObject()
    {
        return orbObject;
    }

    public void SetVolume(float volume)
    {
        AudioSource audioSource = orbObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}

// SoundPlayer class to play sounds and handle spatialization

    public class SoundPlayer
    {
        private List<AudioSource> audioSources;
        private List<GameObject> orbObjects;
        private List<Sound> sounds;
        private Transform listenerTransform;
        private Vector3 lastSoundPosition;

        public SoundPlayer(Transform listenerTransform)
        {
            audioSources = new List<AudioSource>();
            orbObjects = new List<GameObject>();
            sounds = new List<Sound>();
            this.listenerTransform = listenerTransform;
        }

    public void AddSound(Sound sound, GameObject orbPrefab)
    {
        if ((sound.GetPosition() - lastSoundPosition).magnitude > 0.5f)
        {
            GameObject soundObject = new GameObject("Sound");
            soundObject.transform.position = sound.GetPosition();
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = sound.GetClip();
            audioSource.volume = sound.GetVolume();
            audioSource.pitch = sound.GetPitch();
            audioSources.Add(audioSource);
            audioSource.Play();
            if (orbPrefab != null)
            {
                GameObject orbObject = GameObject.Instantiate(orbPrefab);
                orbObject.transform.position = sound.GetPosition();
                orbObjects.Add(orbObject);
                soundObject.AddComponent<OrbBehavior>().orbObject = orbObject;
                ParticleSystem.MainModule main = orbObject.GetComponent<ParticleSystem>().main;
                main.startColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                Sound newSound = new Sound(sound.GetClip(), sound.GetPosition(), sound.GetVolume(), sound.GetPitch(), orbObject);
                sounds.Add(newSound);
            }
            else
            {
                sounds.Add(sound);
            }
            lastSoundPosition = sound.GetPosition();
            GameObject.Destroy(soundObject, sound.GetClip().length);
        }
    }




    public void UpdateListenerPosition(Vector3 position)
        {
            listenerTransform.position = position;
        }

        public void UpdateSoundProperties(int index, float volume, float pitch)
        {
            AudioSource audioSource = audioSources[index];
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            GameObject orbObject = orbObjects[index];
            OrbBehavior orbBehavior = orbObject.GetComponent<OrbBehavior>();
            if (orbBehavior != null)
            {
                orbBehavior.SetVolume(volume);
                orbBehavior.SetPitch(pitch);
            }
        }

        public void PlaySounds()
        {
            foreach (AudioSource source in audioSources)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
        }
    }
