using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//How the fuck do i make sound

public class SoundHandler : MonoBehaviour
 {

    public AudioSource src;

    public AudioSource srcAtck;

    public AudioClip[] sfx = new AudioClip[10];

    public Player player;

    //src.clip = sfx; src.Play();

    public float soundDuration;
    public float maxSoundDuration;

    public void Start()
    {
        soundDuration = maxSoundDuration;
    }

    int currentIndex, lastIndex;

    public void soundUpdate()
    {
        lastIndex = currentIndex;

        switch(player.state)
        {
            case Player.movementState.idle:
                src.clip = sfx[0];
                currentIndex = 0;
                break;
            case Player.movementState.walking:
                src.clip = sfx[1];
                currentIndex = 1;
                break;
            case Player.movementState.sprinting:
                src.clip = sfx[2];
                currentIndex = 2;
                break;
            case Player.movementState.air:
                src.clip = sfx[3];
                currentIndex = 3;
                break;
            case Player.movementState.crouching:
                src.clip = sfx[4];
                currentIndex = 4;
                break;
            case Player.movementState.dash:
                src.clip = sfx[5];
                currentIndex = 5;
                break;
            case Player.movementState.sliding:
                src.clip = sfx[6];
                currentIndex = 6;
                break;
           
        }

        //  srcAtck.Play();

        if (soundDuration > 0)
            soundDuration -= Time.deltaTime;

        if (soundDuration <= 0 || lastIndex != currentIndex)
        {
            soundDuration = maxSoundDuration;

            src.Play();
        }
        
    }

}

