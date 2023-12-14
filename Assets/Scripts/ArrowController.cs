using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowController : MonoBehaviour
{
    public EventTrigger.TriggerEvent arrowTrigger;
    [field: SerializeField] public EyeController _eyeController;
    private void OnCollisionStay2D(Collision2D collision)
    {
        _eyeController = collision.gameObject.GetComponent<EyeController>();
        if (_eyeController != null)
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            this.arrowTrigger.Invoke(eventData);
        }
    }

}
