using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioSource audioSourceBackground;
    public AudioSource audioSourceMessage;

    [SerializeField]
    List<AudioClip> backgroundMusic = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> directionNews = new List<AudioClip>();
    [SerializeField]
    Weather weatherScript;

    private int currentBackgroundMusicIndex = 0;

    private AudioClip currentBackgroundMusic;

    private void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {
        if (!audioSourceBackground.isPlaying)
        {
            PlayBackgroundMusic();
        }
    }

    public void PlayBackgroundMusic()
    {
        currentBackgroundMusic = backgroundMusic[currentBackgroundMusicIndex];
        audioSourceBackground.PlayOneShot(currentBackgroundMusic, 1);
        currentBackgroundMusicIndex += 1;
    }

    public void PlayRadioMessage(Weather.windDirection nextWindDirection)
    {
        MakeQuietBackgroundMusic();
        AudioClip messageToPlay;
        switch (nextWindDirection)
        {
            case Weather.windDirection.North:
                messageToPlay = directionNews[0];
                break;
            case Weather.windDirection.NorthEast:
                messageToPlay = directionNews[1];
                break;
            case Weather.windDirection.East:
                messageToPlay = directionNews[2];
                break;
            case Weather.windDirection.SouthEast:
                messageToPlay = directionNews[3];
                break;
            case Weather.windDirection.South:
                messageToPlay = directionNews[4];
                break;
            case Weather.windDirection.SouthWest:
                messageToPlay = directionNews[5];
                break;
            case Weather.windDirection.West:
                messageToPlay = directionNews[6];
                break;
            case Weather.windDirection.NorthWest:
                messageToPlay = directionNews[7];
                break;
            default:
                messageToPlay = directionNews[0];
                break;
        }
        audioSourceMessage.PlayOneShot(messageToPlay, 0.8f);
        // at the end of the radio message, change next weather to current weather
        Invoke("DoAtEndOfRadioMessage", messageToPlay.length);
    }

    private void MakeQuietBackgroundMusic()
    {
        audioSourceBackground.volume = 0.1f;
    }

    private void MakeLoudBackgroundMusic()
    {
        audioSourceBackground.volume = 0.8f;
    }
    private void SetNextWeather()
    {
        Debug.Log("Finished playing radio message. Set fetched weather data to current");
        weatherScript.SetNextWeatherToCurrent();
    }

    private void DoAtEndOfRadioMessage()
    {
        SetNextWeather();
        MakeLoudBackgroundMusic();
    }
}
