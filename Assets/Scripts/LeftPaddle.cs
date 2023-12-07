using UnityEngine;
using NetMQ;

public class LeftPaddle : Paddle
{
    private Vector2 _direction;
    //Constrains for the mouse position
    public float _gamebottomleftpositionx = -6.6f;
    public float _gamebottomleftpositiony = -3.5f;
    public float _gameleftxoffset = 1.0f;
    public float _gameleftyoffset = 7f;
    //Constrains for the paddle position
    public float _paddledleftpositionx = -6.20f;
    public float _paddleleftxoffset = 0.35f;
    public float _paddleleftyoffset = 0.82f;
    //
    public Vector2 _mouseposition;
    public Vector2 _worldposition;
    public Vector3 _eyepos;
    public Vector3 _eyepos2;
    
    [field: SerializeField] public EyeController _eyeController;

    private void OnEnable()
    {
        _eyeController.oneyePosition += MovewithEye;
    }

    private void Update()
    {
        KeyboardUpdate();
        MouseUpdate();
        //MoveusingEye(_eyepos);
        
    } 
    private void FixedUpdate()
        {
            if(_direction.sqrMagnitude != 0){
            _rigidbody.AddForce(_direction * this.speed);
            }
    
        }
    private void KeyboardUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
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
        if (_worldposition.x > _gamebottomleftpositionx && _worldposition.x < (_gamebottomleftpositionx + _gameleftxoffset))
        {
            if (_worldposition.y > _gamebottomleftpositiony && _worldposition.y < (_gamebottomleftpositiony + _gameleftyoffset))
            {
                //Inside the shape of the paddle
                if (_worldposition.x > _paddledleftpositionx && _worldposition.x < (_paddledleftpositionx + _paddleleftxoffset))
                {
                    if (_worldposition.y > (_rigidbody.position.y + _paddleleftyoffset))
                    {
                        _direction = Vector2.up;
                    }
                    else if (_worldposition.y < (_rigidbody.position.y - _paddleleftyoffset))
                    {
                        _direction = Vector2.down;
                    }
                    else { _direction = Vector2.zero; }
                }

            }

        }
    }

    private void MovewithEye(Vector3 positionofeye)
    {
        _eyepos = positionofeye;
    }
    private void MoveusingEye(Vector3 pos)
    {
        _eyepos2 = pos;
        //Debug.Log(_eyepos2);
        if (_eyepos2.x > _gamebottomleftpositionx && _eyepos2.x < (_gamebottomleftpositionx + _gameleftxoffset))
        {
            if (_eyepos2.y > _gamebottomleftpositiony && _eyepos2.y < (_gamebottomleftpositiony + _gameleftyoffset))
            {
                //Inside the shape of the paddle
                if (_eyepos2.x > _paddledleftpositionx && _eyepos2.x < (_paddledleftpositionx + _paddleleftxoffset))
                {
                    if (_eyepos2.y > (_rigidbody.position.y + _paddleleftyoffset))
                    {
                        _direction = Vector2.up;
                    }
                    else if (_eyepos.y < (_rigidbody.position.y - _paddleleftyoffset))
                    {
                        _direction = Vector2.down;
                    }
                    else { _direction = Vector2.zero; }
                }

            }

        }

    }


}
