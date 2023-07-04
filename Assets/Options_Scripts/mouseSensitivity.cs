using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseSensitivity : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        text.text = slider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            text.text = v.ToString();
            optionsValues.mouseSensitivity = v;
        });
    }
}
