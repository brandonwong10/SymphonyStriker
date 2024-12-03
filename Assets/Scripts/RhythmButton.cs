using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmButton : MonoBehaviour
{

    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public KeyCode KeyToPress; // the key to press to press the button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the key is pressed
        if (Input.GetKeyDown(KeyToPress))
        {
            theSR.sprite = pressedImage; // change the sprite to pressedImage
        }

        // if the key is released
        if (Input.GetKeyUp(KeyToPress))
        {
            theSR.sprite = defaultImage; // change the sprite to defaultImage
        }
    }
}
