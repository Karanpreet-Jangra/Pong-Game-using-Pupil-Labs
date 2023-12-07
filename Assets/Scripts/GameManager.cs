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
    }

    private void OnApplicationQuit()
    {

        NetMQConfig.Cleanup(false);
    }
}
