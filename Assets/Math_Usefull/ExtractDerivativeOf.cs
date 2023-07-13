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


    public float timeBeforeLastDelta;

    public float timeForDelta;

    public ExtractDerivativeOf(float firstValue, float secondValue, float timeBeforeLastDelta)
    {
        this.firstValue = firstValue;

        this.secondValue = secondValue;

        this.timeBeforeLastDelta = timeBeforeLastDelta;

        timeForDelta = 0;
    }



    //You need to always update the last values and current values
     public void updateLastValues(float firstValue, float secondValue)
     {
        this.firstValue = firstValue; this.secondValue = secondValue;

         if(timeForDelta <= 0)
         {
             timeForDelta = timeBeforeLastDelta;

             lastFirstValue = firstValue;

             lastSecondValues = secondValue;
         }


         if (timeForDelta > 0)
             timeForDelta -= Time.deltaTime;

     }

   

    public float getDerivativeOfTheTwo(bool switchAxes)
    {
        if(switchAxes)
            return  (secondValue - lastSecondValues) / (firstValue - lastFirstValue);

        return (firstValue - lastFirstValue) / (secondValue - lastSecondValues);
    }

    public float getDerivativeWithTime(bool value)
    {
        if (value)
            return (firstValue - lastFirstValue) / Time.deltaTime;

        return (secondValue - lastSecondValues) / Time.deltaTime;
    }
}

