using UnityEngine;

public class BottomArrowControl : MonoBehaviour
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
            if (_eyepos.y > -2.534976 && _eyepos.y < -0.977653)
            {
                if (_eyepos.x <= 7.8904)
                {
                    decision = _linearEquations.thirdlinearequation((double)_eyepos.x);
                }
                else if (_eyepos.x > 7.8904)
                {
                    decision = _linearEquations.fourthlinearequation((double)_eyepos.x);
                }
                else { decision = 0f; }
                if (decision <= _eyepos.y)
                {
                    _renderer.material.color = Color.gray;
                }
                else if (decision >= _eyepos.y)
                {
                    _renderer.material.color = Color.white;
                }
                else { _renderer.material.color = Color.white; }
            }
            else { _renderer.material.color = Color.white; }
        }
        else _renderer.material.color = Color.white;
    }
}
