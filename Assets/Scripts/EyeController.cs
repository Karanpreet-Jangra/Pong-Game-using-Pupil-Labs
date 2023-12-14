using System;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EyeController: MonoBehaviour 
{
    [field: SerializeField] public PupilSubscriber<SurfaceData> PupilSubscriber;

    public RectTransform surfRectTransform;
    public Transform controlledEyeObject;
    public event Action<Vector3> oneyePosition;

    double lastTimestamp = -1;
    double smooth_x = 0.5f;
    double smooth_y = 0.5f;
    public double smoothness = 0.35f;

    private void OnEnable()
    {
        PupilSubscriber.onData += OnDataReceived;
    }

    private void OnDataReceived(SurfaceData Surfacedatum)
    {
        TrySortData(Surfacedatum);
    }

    private void TrySortData(SurfaceData data)
    {
        data.SortGazeList();
        double sumx = 0;
        double sumy = 0;
        //Debug.Log($"GAZES ARE: {data.gaze_on_surfaces.Count}");
        var numberofgazes = data.gaze_on_surfaces.Count;
        if(numberofgazes > 0)
        {
            if (data.gaze_on_surfaces[0].timestamp >= lastTimestamp)
            {
                foreach (var gaze in data.gaze_on_surfaces)
                {
                    //ProcessGaze(gaze.NormPosX, gaze.NormPosY);
                    sumx += gaze.NormPosX;
                    sumy += gaze.NormPosY;
                    lastTimestamp = gaze.timestamp;
                    //Debug.Log($"{gaze.topic} {gaze.timestamp} {gaze.NormPosX} {gaze.NormPosY}");
                }
                sumx = sumx / numberofgazes;
                sumy = sumy / numberofgazes;

                //Using the avegage 
                //ProcessGaze(sumx, sumy);

                //Using the last gaze data
                var lastdatax = data.gaze_on_surfaces[numberofgazes - 1].NormPosX;
                var lastdatay = data.gaze_on_surfaces[numberofgazes - 1].NormPosY;
                ProcessGaze(lastdatax, lastdatay);
            }
        }
        
    }
    private void ProcessGaze(double averagex, double averagey)
    {
        //point = Normalized coordinates on the Screen Rectangle
        //var point = new Vector2((float)gaze.NormPosX, (float)gaze.NormPosY);
        var point = new Vector2((float)averagex,(float)averagey);
        //surfRect: In Local Space relative to Canvas
        var surfRect = surfRectTransform.rect;
        surfRect.x = 0;
        surfRect.y = 0;
        //Returns the real screen coordinates.
        var point2 = Rect.NormalizedToPoint(surfRect, point);
        point2 = Camera.main.ScreenToWorldPoint(point2);
        var result = new Vector3(point2.x, point2.y, 0);
        smooth_x += smoothness * ((float)result.x - smooth_x);
        smooth_y += smoothness * ((float)result.y - smooth_y);
        result = new Vector3((float)smooth_x,(float)smooth_y, 0);
        controlledEyeObject.position = result;
        //Debug.Log(result);
        oneyePosition?.Invoke(result);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        Debug.Log("BOO");
    }
}