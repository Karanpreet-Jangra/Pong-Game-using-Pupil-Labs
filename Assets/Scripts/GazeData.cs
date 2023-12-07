using UnityEngine;

[System.Serializable]
public class GazeData
{
    public string topic;
    [SerializeField] double[] norm_pos = new double[] { 0, 0, 0 };
    public double timestamp;
    public double confidence;

    public double NormPosX => norm_pos[0];
    public double NormPosY => norm_pos[1];

    public override string ToString() => $"topic:{topic}\nnorm_pos:[x:{NormPosX}, y:{NormPosY}]\ntimestamp: {timestamp}\nconfidence:{confidence}";
}