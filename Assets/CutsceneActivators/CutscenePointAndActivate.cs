using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class CutscenePointAndActivate : MonoBehaviour
{
    public bool beenAtivated = false;

    public float tolerance;

    public VideoClip clip;

    public float videoDuration;

    public GameObject activation;


    public void Update()
    {
        if (!beenAtivated)
        {
            if ((transform.position - Player.instance.transform.position).magnitude < tolerance)
            {
                beenAtivated = true;

                Invoke(nameof(setActive), videoDuration);
            }
        }
    }

    public void setActive()
    {
        activation.gameObject.SetActive(true);
    }

}

