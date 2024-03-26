using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NetMQ;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    private string path = "C:/Users/karen/Documents/GithubProjectPongGame/Pong-Game-using-Pupil-Labs/Assets/Metrics/";
    public string playerName = "";
    public string mode = "";
    private string filename = "";
    public int totalNumberOfTries;

    public Ball ball;
    private int _leftplayerscores;
    private int _rightplayerscores;
    public TMPro.TMP_Text leftplayertext;
    public TMPro.TMP_Text rightplayertext;
    public Paddle leftpaddle;
    public Paddle rightpaddle;

    private float timeoffirstround;
    private float timebetweenrounds;
  

    public void Leftplayerscores()
    {
        CalculateTimeBetweenRounds();
        WriteMetrics();
        _leftplayerscores++;
        if (_leftplayerscores == totalNumberOfTries)
        {
            QuitGame();
        }
        this.leftplayertext.text = _leftplayerscores.ToString(); 
        ResetRound();
    }

    public void Rightplayerscores()
    {
        CalculateTimeBetweenRounds();
        WriteMetrics();
        _rightplayerscores++;
        if (_rightplayerscores == totalNumberOfTries)
        {
            QuitGame();
        }
        this.rightplayertext.text = _rightplayerscores.ToString(); 
        ResetRound();
    }

    private void ResetRound()
    {
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
        this.leftpaddle.ResetPosition();
        this.rightpaddle.ResetPosition();   
    }

    private void CalculateTimeBetweenRounds()
    {
        if ((_rightplayerscores == 1 && _leftplayerscores == 0) || (_rightplayerscores == 0 && _leftplayerscores == 1))
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
            Debug.Log(Application.dataPath);
            timeoffirstround = temp1;
            timebetweenrounds = timeofnextrounds;
        }
    }

    private void WriteMetrics()
    {
        filename = path + playerName + mode + ".csv";
        Debug.Log($"{filename}");
        TextWriter writer = new StreamWriter(filename, true);
        //var ballspeed = ball._rigidbody.velocity.x;
        writer.WriteLine("AI Score:" + "," + _leftplayerscores + "," + "Player Score: " + "," + _rightplayerscores + "," + "Time Between Rounds: " + ","
                          + timebetweenrounds);
        writer.Close();

    }

    private void OnApplicationQuit()
    {

        NetMQConfig.Cleanup(false);
        //The total time since start
        //Debug.Log((Time.realtimeSinceStartup));
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        WriteMetrics();
    }
}
