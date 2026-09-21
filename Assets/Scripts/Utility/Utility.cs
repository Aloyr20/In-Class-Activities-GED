using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    //Increament must be -1 or 1
    public static int WrapAround(int max, int current, int increment, int min=0)
    {
        int temp = current + increment;

        if(temp >= max)
        {
            //wrap around to first value
            temp = min;
        }
        else if(temp < min)
        {
            //wrap around to last value
            temp = max - 1;
        }

        return temp;
    }
}
