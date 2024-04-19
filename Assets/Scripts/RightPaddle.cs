using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class RightP : Paddle
{
    private Vector2 _direction;
    //Using the mouse modality
    //Constrains for mouse, pio austhres
    private float _paddledrightpositionx = 5.5f;
    private float _paddlerightxoffset = 1f;

    //Using the eye modality
    //Constrains for focus, pio austhres
    private float _gamebottomrightpositionx = 6.9f;
    private float _gamebottomrightpositiony = -3.5f;
    private float _gamerightxoffset = 1.5f;
    private float _gamerightyoffset = 7f;
    
    //Constrains for paddle on y axis for both mouse and eye modality
    private float _paddlerightyoffset = 0.95f;

    public Vector3 _eyepos;
    [field: SerializeField] public EyeController _eyeController;

    //[SerializeField] bool onArrowsEyeEnable;
    [SerializeField] bool onMouseEnable;
    [SerializeField] bool onKeyboardEnable;
    [SerializeField] bool onEyeEnable;
    [SerializeField] bool onFocus;
    //[SerializeField] bool onVoiceEnable;

    private Vector2 _mouseposition;
    private Vector2 _worldposition;

    //For voice modality
    //private KeywordRecognizer keywordRecognizer;
    //private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void OnEnable()
    {
        _eyeController.oneyePosition += MovewithEye;
    }

    private void MovewithEye(Vector3 positionofeye)
    {
        _eyepos = positionofeye;
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        //actions.Add("back", VoiceMoveup);
        //actions.Add("stop", VoiceMovestop);
        //actions.Add("down", VoiceMovedown);
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        //keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        //if (onVoiceEnable)
        //    MovewithVoice();
    }
    private void Update() {
        //var firstframetime = Time.time;
        //var everyotherframetime = Time.time;
        //Debug.Log(timeofrounds);
        if (onMouseEnable)
            MouseUpdate();
        if (onKeyboardEnable)
            KeyboardUpdate();
        if (onEyeEnable)
            MoveUsingEye();
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0 )
        {
            _rigidbody.AddForce(_direction * this.speed);
        }
    }

    private void KeyboardUpdate()
    {
        _mouseposition = Input.mousePosition;
        _worldposition = Camera.main.ScreenToWorldPoint(_mouseposition);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else
        {
            _direction = Vector2.zero;
        }
    }

    private void MouseUpdate()
    {
        _mouseposition = Input.mousePosition;
        _worldposition = Camera.main.ScreenToWorldPoint(_mouseposition);
        //The paddle
        if (_worldposition.x > _paddledrightpositionx && _worldposition.x < (_paddledrightpositionx + _paddlerightxoffset))
        {
            if (_worldposition.y > (_rigidbody.position.y + _paddlerightyoffset))
            {
                _direction = Vector2.up;
            }
            else if (_worldposition.y < (_rigidbody.position.y - _paddlerightyoffset))
            {
                _direction = Vector2.down;
            }
            else { _direction = Vector2.zero; }
        }
    }

    private void MoveUsingEye()
    {
        _mouseposition = Input.mousePosition;
        _worldposition = Camera.main.ScreenToWorldPoint(_mouseposition);
        //Debug.Log(_eyepos);
        if (_eyepos.x < _gamebottomrightpositionx && _eyepos.x > (_gamebottomrightpositionx - _gamerightxoffset))
        {
            //Debug.Log("Ok");
            if (_eyepos.y > _gamebottomrightpositiony && _eyepos.y < (_gamebottomrightpositiony + _gamerightyoffset))
            {
                //The paddle
                if (_eyepos.y > (_rigidbody.position.y + _paddlerightyoffset))
                    {
                        if (onFocus)
                        {
                            _direction = Vector2.up;
                            _renderer.material.color = Color.black;
                        }
                        else
                        {
                            _direction = Vector2.up;
                        }
                    }
                else if (_eyepos.y < (_rigidbody.position.y - _paddlerightyoffset))
                    {
                        if (onFocus)
                        {
                            _direction = Vector2.down;
                            _renderer.material.color = Color.gray;
                        }
                        else
                        {
                            _direction = Vector2.down;
                        }
                    }
                else {
                        _direction = Vector2.zero; 
                }
            }
            else
            {
                _direction = Vector2.zero;
            }
        }
        else
        {
            _direction = Vector2.zero;
        }
    }


    //public void MovewithVoice()
    //{
    //    keywordRecognizer.Start();
    //}

    //private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    //{
    //    Debug.Log(speech.text);
    //    actions[speech.text].Invoke();
    //}

    //public void VoiceMoveup()
    //{
    //    _direction = Vector2.up;
    //}
    //public void VoiceMovedown()
    //{
    //    _direction = Vector2.down;
    //}

    //public void VoiceMovestop()
    //{
    //    _direction = Vector2.zero;
    //}
    //private void OnApplicationQuit()
    //{
    //    keywordRecognizer.Stop();
    //}

    //public void MovewithArrows()
    //{
    //    _mouseposition = Input.mousePosition;
    //    _worldposition = Camera.main.ScreenToWorldPoint(_mouseposition);

    //    //Arrows
    //    if (_eyepos.x > 6.895481 && _eyepos.x < 8.9070)
    //    {
    //        if(_eyepos.y > 0.9690 && _eyepos.y < 2.526324)
    //        {
    //            if (_eyepos.x <= 7.9012)
    //            {
    //                decision = _linearEquations.firstlinearequation((double)_eyepos.x);
    //            }
    //            else if (_eyepos.x > 7.9012)
    //            {
    //                decision = _linearEquations.secondlinearequation((double)_eyepos.x);
    //            }
    //            else
    //            {
    //                decision = 0f;
    //            }
    //            if (decision >= _eyepos.y)
    //            {
    //                _direction = Vector2.up;

    //            }
    //            else if (decision <= _eyepos.y)
    //            {
    //                _direction = Vector2.zero;
    //            }
    //            else
    //            {
    //                _direction = Vector2.zero;
    //            }
    //        }
    //        else if (_eyepos.y > -2.534976 && _eyepos.y < -0.977653)
    //        {
    //            if (_eyepos.x <= 7.8904)
    //            {
    //                decision = _linearEquations.thirdlinearequation((double) _eyepos.x);
    //            }
    //            else if (_eyepos.x > 7.8904)
    //            {
    //                decision = _linearEquations.fourthlinearequation((double) _eyepos.x);
    //            }
    //            else { decision = 0f; }
    //            if (decision <= _eyepos.y)
    //            {
    //                _direction = Vector2.down;
    //            }
    //            else if (decision >= _eyepos.y)
    //            {
    //                _direction = Vector2.zero;
    //            }
    //            else { _direction = Vector2.zero; }
    //        }
    //        else { _direction = Vector2.zero; }
    //    }
    //    else _direction = Vector2.zero; 
    //}

}
