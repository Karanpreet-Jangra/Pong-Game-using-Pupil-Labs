using UnityEditor;
using UnityEngine;
public class ArrowUp : MonoBehaviour 
{
    protected Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    public void UpArrowTrigger()
    {
        _renderer.material.color = Color.black;
    }

    public void DownArrowTrigger()
    {
        _renderer.material.color = Color.gray;
    }
}
