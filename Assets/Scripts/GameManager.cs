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
    private float firstroundtime;
    private float timeofnextround;

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
        if(_rightplayerscores == 0 && _leftplayerscores == 0)
        {
            firstroundtime = Time.time;
            Debug.Log("Time between first round: ");
            Debug.Log(firstroundtime);
        }
        else //The other rounds
        {
            var lasttime = firstroundtime;
            var currenttime = Time.time;
            //Debug.Log("Ok");
            //Debug.Log(temp1);
            timeofnextround = currenttime - lasttime;
            Debug.Log("Time between each round: ");
            Debug.Log(timeofnextround);
            //Debug.Log(Application.dataPath);
            firstroundtime = currenttime;
            //timebetweenrounds = timeofnextround;
        }
    }

    private void WriteMetrics()
    {
        if(_rightplayerscores == 0 && _leftplayerscores == 0)
        {
            filename = path + playerName + mode + ".csv";
            Debug.Log($"{filename}");
            TextWriter writer = new StreamWriter(filename, true);
            //var ballspeed = ball._rigidbody.velocity.x;
            writer.WriteLine("Bot Score:" + "," + _leftplayerscores + "," + "Player Score: " + "," + _rightplayerscores + "," + "Time Between Rounds: " + ","
                              + firstroundtime);
            writer.Close();
        }
        else
        {
            filename = path + playerName + mode + ".csv";
            Debug.Log($"{filename}");
            TextWriter writer = new StreamWriter(filename, true);
            //var ballspeed = ball._rigidbody.velocity.x;
            writer.WriteLine("Bot Score:" + "," + _leftplayerscores + "," + "Player Score: " + "," + _rightplayerscores + "," + "Time Between Rounds: " + ","
                              + timeofnextround);
            writer.Close();
        }
 

    }

    private void OnApplicationQuit()
    {
        NetMQConfig.Cleanup(false);
        //The total time since start
        Debug.Log((Time.realtimeSinceStartup));
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        WriteMetrics();
    }
}
