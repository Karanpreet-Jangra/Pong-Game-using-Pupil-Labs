using UnityEngine;
using System;

public class ZmqSubscriberComponent : MonoBehaviour
{
    [field:SerializeField] public ZmqSubscriber Subscriber { get; private set; }

    private void OnDestroy()
    {
        Subscriber.Stop();
        //Debug.Log("Destroy");
    }
}