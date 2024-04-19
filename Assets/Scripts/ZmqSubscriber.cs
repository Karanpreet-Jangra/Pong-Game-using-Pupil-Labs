using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using NetMQ;
using NetMQ.Sockets;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine.Assertions;
using System.Net.Sockets;
using Unity.VisualScripting;
using System.Net;
using System.IO;
using MessagePack;
using System.Collections.Concurrent;
using UnityEngine.UIElements;
using System.Collections;

[Serializable]
public class ZmqSubscriber
{
    const string sub = "SUB_PORT", pub = "PUB_PORT";
    public string ip = "127.0.0.1";
    public int port = 50020;
    public string topic;
    private int subport = 0;
    private Thread recThread;
    private bool running = false;

    //When messages arrive from Pupil Headset, "notify" other scripts through subscription.
    public event Action<string, string> onMessageReceived;

    private RequestSocket cSocket;
    private SubscriberSocket subSocket;

    public bool Start()
    {
        if (recThread != null && running)
            return false;
        running = true;
        recThread = new Thread(ThreadReceiveData);
        recThread.Start();
        //Debug.Log("Thread started!");
        return true;
    }
    
    public bool Stop()
    {
        if (recThread == null)
            return false;

        running = false;    
        recThread.Join();
        recThread = null;
        //if (cSocket != null && !cSocket.IsDisposed)
        //    cSocket?.Dispose();
        //if (subSocket != null && !subSocket.IsDisposed)
        //    subSocket?.Dispose();
        cSocket = null;
        subSocket = null;
        return true;
    }

    private void ThreadReceiveData()
    {
        AsyncIO.ForceDotNet.Force();

        cSocket = new RequestSocket();

        cSocket.Connect($"tcp://{ip}:{port}");
        Assert.IsNotNull(cSocket);

        cSocket.SendFrame(sub);

        try { 
            subport = int.Parse(cSocket.ReceiveFrameString()); 
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            Debug.Log("Exception");
            if(cSocket != null && !cSocket.IsDisposed) //
                cSocket.Close();
            return;
        }
        
        cSocket.Close();
        cSocket = null;
        //Debug.Log("Subport:"+subport);

        subSocket = new SubscriberSocket();
        subSocket.Connect($"tcp://{ip}:{subport}");
        subSocket.Subscribe(topic);

        var msg = new NetMQMessage();

        while (running)
        {
            if (!subSocket.TryReceiveMultipartMessage(ref msg))
                continue;
            
            string topicReceived = msg[0].ConvertToString();
            //Content as an array
            byte[] dataReceived = msg[1].ToByteArray();
            string convertedToJsonData = MessagePackSerializer.ConvertToJson(dataReceived);
            onMessageReceived?.Invoke(topicReceived, convertedToJsonData);
            //Debug.Log(topicReceived);
            //Debug.Log(MessagePackSerializer.ConvertToJson(msg[1].ToByteArray()));   
        }
    }
}
