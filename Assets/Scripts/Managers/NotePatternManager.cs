using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePatternManager : MonoBehaviour
{
    public static NotePatternManager instance; // Singleton instance

    public GameObject[] patterns; // The note patterns to spawn
    private GameObject activePattern; // The currently active note pattern
    public bool activePatternHasEnded; // Has the active pattern ended?

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Play a note pattern
    public void PlayPattern(int patternID)
    {
        activePattern = Instantiate(patterns[patternID]); // create a new instance of the pattern and set it as the active pattern
        activePattern.SetActive(true); // Activate the note pattern
        activePatternHasEnded = false; // Reset the active pattern has ended status
    }
}
