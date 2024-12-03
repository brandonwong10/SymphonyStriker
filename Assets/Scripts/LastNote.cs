using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastNote : MonoBehaviour
{
    public bool caught; // Has the the last note (this) entered the catch?

    // Start is called before the first frame update
    void Start()
    {
        caught = false; // The last note has not been caught
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        tag = other.tag;
        switch (tag)
        {
            // if the pbject is a deactivator
            case "Deactivator":
                caught = true;
                break;
        }

    }
}
