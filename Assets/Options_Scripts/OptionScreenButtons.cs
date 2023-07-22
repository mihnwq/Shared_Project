using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionScreenButtons : MonoBehaviour
{

    public TextMeshProUGUI time;

    public TextMeshProUGUI kills;

    public TextMeshProUGUI kd;

    public Canvas optionCanvas;

    public void optionUpdate()
    {
        time.text = Mathf.Round(Time.fixedTime).ToString();
    }

    public void onScreenButtonClick()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void onOptionButtonClick()
    {
        transform.gameObject.SetActive(false);

        optionCanvas.gameObject.SetActive(true);

        
    }


}

