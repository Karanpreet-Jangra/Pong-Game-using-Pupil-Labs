using UnityEngine;

public class TopArrowControl : MonoBehaviour
{
    [field: SerializeField] public EyeController _eyeController;
    [field: SerializeField] public LinearEquations _linearEquations;
    public Vector3 _eyepos;
    protected Renderer _renderer;
    private double decision;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        _eyeController.oneyePosition += MovewithEye;
    }
    private void MovewithEye(Vector3 positionofeye)
    {
        _eyepos = positionofeye;
    }
    private void Update()
    {
        MovewithArrows();
    }
    public void MovewithArrows()
    {
        //Arrows
        if (_eyepos.x > 6.895481 && _eyepos.x < 8.9070)
        {
            if (_eyepos.y > 0.9690 && _eyepos.y < 2.526324)
            {
                if (_eyepos.x <= 7.9012)
                {
                    decision = _linearEquations.firstlinearequation((double)_eyepos.x);
                }
                else if (_eyepos.x > 7.9012)
                {
                    decision = _linearEquations.secondlinearequation((double)_eyepos.x);
                }
                else
                {
                    decision = 0f;
                }
                if (decision >= _eyepos.y)
                {
                    _renderer.material.color = Color.green;
                }
                else if (decision <= _eyepos.y)
                {
                    _renderer.material.color = Color.white;
                }
                else
                {
                    _renderer.material.color = Color.white;
                }
            }
            else { _renderer.material.color = Color.white; }
        }
        else _renderer.material.color = Color.white;
    }
}
