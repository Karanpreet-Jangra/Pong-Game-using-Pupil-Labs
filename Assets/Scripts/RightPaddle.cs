using UnityEditor;
using UnityEngine;

public class RightP : Paddle
{
    private Vector2 _direction;
    //Constrains for the eye position
    private float _gamebottomrightpositionx = 5.6f;
    private float _gamebottomrightpositiony = -3.5f;
    private float _gamerightxoffset = 1.5f;
    private float _gamerightyoffset = 7f;
    //Constrains for the paddle position
    private float _paddledrightpositionx = 5.86f;
    private float _paddlerightxoffset = 0.35f;
    private float _paddlerightyoffset = 1.0f;

    private Vector3 _eyepos;
    [field: SerializeField] public EyeController _eyeController;
    //[field: SerializeField] public LinearEquations _linearEquations;

    //[SerializeField] bool onArrowsEyeEnable;
    [SerializeField] bool onMouseEnable;
    [SerializeField] bool onKeyboardEnable;
    [SerializeField] bool onEyeEnable;
    [SerializeField] bool onFocus;

    private Vector2 _mouseposition;
    private Vector2 _worldposition;

    public float timeRemaining = 0.6f;
    private bool timeIsRunning;

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
    }
    private void Update() {
        if (onMouseEnable)
            MouseUpdate();
        if (onKeyboardEnable)
            KeyboardUpdate();
        if (onEyeEnable)
            MoveUsingEye();
        
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude != 0)
        {
            _rigidbody.AddForce(_direction * this.speed);
        }

    }

    private void KeyboardUpdate()
    {
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
        //Inside the shape of the paddle
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
        //Debug.Log(_eyepos);
        if (_eyepos.x < _gamebottomrightpositionx && _eyepos.x > (_gamebottomrightpositionx - _gamerightxoffset))
        {
            //Debug.Log("Ok");
            if (_eyepos.y > _gamebottomrightpositiony && _eyepos.y < (_gamebottomrightpositiony + _gamerightyoffset))
            {
                //Inside the shape of the paddle
                if (_eyepos.x > _paddledrightpositionx && _eyepos.x < (_paddledrightpositionx + _paddlerightxoffset))
                {
                    if (_eyepos.y > (_rigidbody.position.y + _paddlerightyoffset))
                    {
                        if (onFocus)
                        {
                            Moveup(true);
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
                            Movedown(true);
                        }
                        else
                        {
                            _direction = Vector2.down;
                        }
                    }
                    else {
                        timeRemaining = 0.6f;
                        _direction = Vector2.zero; 
                    }
                }
            }
        }
    }

    public void Moveup(bool timerunning)
    {
        if (timerunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0.6f;
                timeIsRunning = false;
                _direction = Vector2.up;
                _renderer.material.color = Color.black;
            }
        }
    }

    public void Movedown(bool timerunning)
    {
        if (timerunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0.6f;
                timeIsRunning = false;
                _direction = Vector2.down;
                _renderer.material.color = Color.gray;
            }
        }
    }

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
