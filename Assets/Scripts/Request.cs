using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using NetMQ;
using NetMQ.Sockets;
using System;
using System.Security.Cryptography;

public class Request : MonoBehaviour
{
    Thread client_thread_;
    //private Object thisLock_ = new Object();
    //bool stop_thread_ = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start the thread.");
        client_thread_ = new Thread(NetMQClient);
        client_thread_.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hi from Update method.");
    }
    void NetMQClient()
    {
        //Debug.Log("Start the thread.");
        AsyncIO.ForceDotNet.Force();

        string address = "tcp://localhost:50020";

        using (var res = new ResponseSocket(address))
            //res.Bind(address); NOT ALLOWED
        {

            using (var req = new RequestSocket(address))
            // The console app provides the response messages
            {
                req.Options.Linger = TimeSpan.Zero;
                req.Connect(address);

                req.SendFrame("Hi");
                //Debug.Log(req.ReceiveMultipartStrings()[0]);
            }
        }
       
        
    }
    void OnApplicationQuit()
    {
        //lock (thisLock_) stop_thread_ = true;
        client_thread_.Join();
        //Debug.Log("End.");
        NetMQConfig.Cleanup();
        Debug.Log("Quit the thread.");
    }
}
