using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    public bool canBeHit; // Can the note be hit?
    public KeyCode keyToPress; // The key to press to hit the note
    private bool hasBeenHit; // Has the note been hit?

    // Start is called before the first frame update
    void Start()
    {
        hasBeenHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if the key is pressed
        if (Input.GetKeyDown(keyToPress))
        {
            // if the note can be hit
            if (canBeHit && !hasBeenHit)
            {
                hasBeenHit = true; // the note has been hit
                GetComponent<SpriteRenderer>().enabled = false; // hide the note
            }
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        tag = other.tag;
        switch (tag)
        {
            // if the object is an activator
            case "Activator":
                canBeHit = true;
                break;
        }

    }

    // OnTriggerExit2D is called when the Collider2D other exits the trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // if the object is an activator
        if (other.tag == "Activator")
        {
            canBeHit = false;
            // if the note has not been hit
            if (!hasBeenHit)
            {
                
            }
        }
    }
}
