using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static SongManager instance; // Singleton instance

    public GameObject[] songs; // Array of music objects

    private void Awake()
    {
        instance = this;
    }

    // Plays the song with the given ID from the beginning
    public void PlaySong(int songID)
    {
        songs[songID].GetComponent<AudioSource>().Stop(); // Reset the song to the beginning
        songs[songID].GetComponent<AudioSource>().Play();
    }

    // Pauses the song with the given ID
    public void PauseSong(int songID)
    {
        songs[songID].GetComponent<AudioSource>().Pause();
    }

    // Resumes the song with the given ID from where it was paused
    public void ResumeSong(int songID)
    {
        songs[songID].GetComponent<AudioSource>().UnPause();
    }
}
