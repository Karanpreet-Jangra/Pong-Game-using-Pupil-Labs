using System;
using UnityEngine;

[Serializable]
public class LinearEquations
{
    //k
    public double firstlinearequation(double x)
    {
        var sum = (1.6 * x) - 10.0637;
        return sum;
    }
    //l
    public double secondlinearequation(double x)
    {
        var sum = (-1.5 * x) + 14.329533;
        return sum;
    }
    //m
    public double thirdlinearequation(double x)
    {
        var sum = (-1.6 * x) + 10.055;
        return sum;
    }
    //n
    public double fourthlinearequation(double x)
    {
        var sum = (1.531913 * x) - 14.5893;
        return sum;
    }
}
