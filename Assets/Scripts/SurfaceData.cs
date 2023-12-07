using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SurfaceData
{
    public string topic;
    public string name;
    public List<GazeData> gaze_on_surfaces = new List<GazeData>();

    public void SortGazeList()
    {
        //sort list on timestamps
        gaze_on_surfaces.Sort((g1, g2) =>
        {
            if (g1.timestamp < g2.timestamp)
                return -1;
            else if (g1.timestamp == g2.timestamp)
                return 0;
            else
                return 1;
        });
    }
    private int Compare(GazeData g1, GazeData g2)
    {
        if (g1.timestamp < g2.timestamp)
            return -1;
        else if (g1.timestamp == g2.timestamp)
            return 0;
        else
            return 1;
    }
    public override string ToString()
    {
        var result = $"topic:{topic}\nname:{name}\n";
        foreach (var gaze in gaze_on_surfaces)
        {
            result += $"{gaze}\n";
        }
        return result;
    }
}