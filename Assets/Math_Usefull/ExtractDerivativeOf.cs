using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExtractDerivativeOf
{

    public float firstValue;

    public float secondValue;

    public float lastFirstValue;

    public float lastSecondValues;


    public float timeBeforeLastValue;

    public float timeForLastValue;

    //Tolerance is used for minimalazing the rate of change
    public float tolerance;

    public ExtractDerivativeOf(float timeBeforeLastValue, float tolerance)
    {

        this.timeBeforeLastValue = timeBeforeLastValue;

        timeForLastValue = 0;

        this.tolerance = tolerance;
    }



    //You need to always update the last values and current values
     public void updateLastValues(float firstValue, float secondValue)
     {
        this.firstValue = firstValue; this.secondValue = secondValue;

         if(timeForLastValue <= 0)
         {
            timeForLastValue = timeBeforeLastValue;

             lastFirstValue = firstValue;

             lastSecondValues = secondValue;
         }


         if (timeForLastValue > 0)
            timeForLastValue -= Time.deltaTime;

     }

   

    public float getDerivativeOfTheTwo()
    {
        float derivative = (firstValue - lastFirstValue) / (secondValue - lastSecondValues);

        if (Mathf.Abs(derivative) > tolerance)
            return derivative;

        return 0;
    }

    public float getDerivativeWithTime()
    {
        float derivative = (firstValue - lastFirstValue) / Time.deltaTime;

        if (Mathf.Abs(derivative) > tolerance)
            return derivative;

        return 0;
    }
}

