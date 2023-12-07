using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

//Generic PupilSubscriber
public class PupilSubscriber<T> : MonoBehaviour
{
    [field: SerializeField] public ZmqSubscriberComponent SubscriberComponent { get; private set; }

    //Method: "=>" == ":{}"
    public ZmqSubscriber Subscriber => SubscriberComponent.Subscriber;
    [SerializeField] bool onStart;

    //FIFO queue to store gaze data from network API
    ConcurrentQueue<T> dataQueue;
    //Event to notify the EyeController that data has arrived
    public event Action<T> onData;

    private void Awake()
    {
        dataQueue = new ConcurrentQueue<T>();
    }
    private void OnEnable()
    {
        // += => subscribe to an event. When an event occurs, subscribe a method to that event.
        Subscriber.onMessageReceived += OnMessageReceived;
    }
    private void OnDisable()
    {
        Subscriber.onMessageReceived -= OnMessageReceived;
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Subscriber.onMessageReceived -= OnMessageReceived;
        else
            Subscriber.onMessageReceived += OnMessageReceived;
    }

    private void Start()
    {
        if (onStart)
            Subscriber.Start();
       
    }

    private void Update()
    {
        while (!dataQueue.IsEmpty)
        {
            dataQueue.TryDequeue(out var data);
            //Debug.Log(data);
            onData?.Invoke(data);
        }
    }

    private void OnMessageReceived(string topic, string message)
    {
        //Decode message to Gaze Data
        //Debug.Log($"Message received:\ntopic:{topic}\nmessage: {message}");
        var data = JsonUtility.FromJson<T>(message);
        dataQueue.Enqueue(data);
    }

}
