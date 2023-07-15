using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private bool pointerDown = false;
    public float pointerDownTimer;

    public float requiredHoldTime;

    public UnityEvent onLongClick;

    public Image fillImage;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("up");
        Reset();
    }


    public void Update()
    {
        if(pointerDown)
        {

           

            pointerDownTimer += Time.deltaTime;

            Debug.Log(pointerDownTimer += Time.deltaTime);

            if (pointerDownTimer > requiredHoldTime)
            {
                if (onLongClick != null)
                    onLongClick.Invoke();

               Reset();
            }
        }

        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }


    private void Reset()
    {
        pointerDown = false;

        pointerDownTimer = 0;

        fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
}

