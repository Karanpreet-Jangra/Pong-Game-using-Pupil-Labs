using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10.0f;
    protected Rigidbody2D _rigidbody;
    protected Renderer _renderer;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }

    public void ResetPosition()
    {
        _rigidbody.position = new Vector2(_rigidbody.position.x, 0.0f);
        _rigidbody.velocity = Vector2.zero;
        _renderer.material.color = Color.green;
    }
}
