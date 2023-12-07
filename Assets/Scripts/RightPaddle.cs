using UnityEngine;

public class RightP : Paddle
{
    private Vector2 _direction;
    //Constrains for the eye position
    public float _gamebottomrightpositionx = 5.6f;
    public float _gamebottomrightpositiony = -3.5f;
    public float _gamerightxoffset = 1.5f;
    public float _gamerightyoffset = 7f;
    //Constrains for the paddle position
    public float _paddledrightpositionx = 5.86f;
    public float _paddlerightxoffset = 0.35f;
    public float _paddlerightyoffset = 0.82f;

    public Vector3 _eyepos;
    [field: SerializeField] public EyeController _eyeController;

    private void OnEnable()
    {
        _eyeController.oneyePosition += MovewithEye;
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void Update(){
        KeyboardUpdate();
        MoveUsingEye();
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

    private void FixedUpdate()
    {
        if(_direction.sqrMagnitude != 0){
            _rigidbody.AddForce(_direction * this.speed);
        }
    }
    private void MovewithEye(Vector3 positionofeye)
    {
        _eyepos = positionofeye;    
    }

    private void MoveUsingEye()
    {
        //Debug.Log(_eyepos);
        if (_eyepos.x < _gamebottomrightpositionx && _eyepos.x > (_gamebottomrightpositionx - _gamerightxoffset))
        {
            //Debug.Log("Ok");
            if (_eyepos.y > _gamebottomrightpositiony && _eyepos.y < (_gamebottomrightpositiony + _gamerightyoffset))
            {
                //Debug.Log("Ok");

                //Inside the shape of the paddle
                if (_eyepos.x > _paddledrightpositionx && _eyepos.x < (_paddledrightpositionx + _paddlerightxoffset))
                {
                    if (_eyepos.y > (_rigidbody.position.y + _paddlerightyoffset))
                    {
                        _direction = Vector2.up;
                        _renderer.material.color = Color.black;
                    }
                    else if (_eyepos.y < (_rigidbody.position.y - _paddlerightyoffset))
                    {
                        _direction = Vector2.down;
                        _renderer.material.color = Color.gray;
                    }
                    else { _direction = Vector2.zero; }
                }

            }

        }
    }


}