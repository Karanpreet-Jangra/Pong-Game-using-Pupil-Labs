using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NetMQ;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    private int _leftplayerscores;
    private int _rightplayerscores;
    public TMPro.TMP_Text leftplayertext;
    public TMPro.TMP_Text rightplayertext;
    public Paddle leftpaddle;
    public Paddle rightpaddle;

    private float timeoffirstround;
    public float timebetweenrounds;

    //[field: SerializeField] public ArrowGenerator arrowgenerator { get; private set; }

    //public void FixedUpdate()
    //{
    //    arrowgenerator.Start();
    //}

    public void Leftplayerscores()
    {
        _leftplayerscores++;
        //Debug.Log(_leftplayerscores);
        this.leftplayertext.text = _leftplayerscores.ToString();
        ResetRound();
    }

    public void Rightplayerscores()
    {
        _rightplayerscores++;
        //Debug.Log(_rightplayerscores);
        this.rightplayertext.text = _rightplayerscores.ToString();
        ResetRound();
    }

    private void ResetRound()
    {
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
        this.leftpaddle.ResetPosition();
        this.rightpaddle.ResetPosition();

        if((_rightplayerscores == 1 && _leftplayerscores == 0) || (_rightplayerscores == 0 && _leftplayerscores == 1))
        {
            timeoffirstround = Time.time;
            Debug.Log("Time between each round: ");
            Debug.Log(timeoffirstround);
            timebetweenrounds = timeoffirstround;
        }
        else
        {
            var temp1 = Time.time;
            var temp2 = timeoffirstround;
            //Debug.Log("Ok");
            //Debug.Log(temp1);
            var timeofnextrounds = temp1 - temp2;
            Debug.Log("Time between each round: ");
            Debug.Log(timeofnextrounds);
            timeoffirstround = temp1;
            timebetweenrounds = timeofnextrounds;
        }
        
    }
   
    private void OnApplicationQuit()
    {

        NetMQConfig.Cleanup(false);
        //The total time since start
        //Debug.Log((Time.realtimeSinceStartup));
    }
}
