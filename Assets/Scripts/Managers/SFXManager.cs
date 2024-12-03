using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance; // Singleton instance

    public GameObject[] sfxs; // Array of sound effects

    public void Awake()
    {
        instance = this;
    }

    // Play a sound effect
    public void PlaySFX(int sfxID)
    {
        sfxs[sfxID].GetComponent<AudioSource>().Play();
    }
}
