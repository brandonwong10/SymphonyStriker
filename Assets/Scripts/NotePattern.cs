using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePattern : MonoBehaviour
{
    public GameObject song; // Reference to the song object
    public float tempo; // The tempo of the note pattern (BPM)
    public GameObject lastNote; // The last note in the pattern
    private Vector3 originalPosition; // The original position of the note pattern

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position; // Store the original position of the note pattern
        tempo = tempo / 60f; // Convert the tempo to seconds
    }

    // Update is called once per frame
    void Update()
    {
        // If the pattern has ended (the last note is has been caught)
        if (lastNote.GetComponent<LastNote>().caught)
        {
            NotePatternManager.instance.activePatternHasEnded = true; // signal to the NotePatternManager that the active pattern (this) has ended
            Destroy(gameObject); // Destroy the note pattern
        }
        // If the last note is active (the pattern is still playing)
        else
        {
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f); // Move the pattern down
        }
    }
}
