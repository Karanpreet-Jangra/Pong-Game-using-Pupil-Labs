using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine.Assertions;
using System.Timers;
using System.Threading;

public class ReqRepNetMq : MonoBehaviour
{
    //Kalo
    //[SerializeField] ZmqSubscriber zmqSub;

    //private string c_sent;
    //public int _port = 5024;

    //public RequestSocket _client;
    //public ResponseSocket _server;

    //public float _interval = 0.5f; // wait interval

    //private float _timer = 0;
    //private int _cnter = 1;

    //private System.Object thisLock_ = new System.Object();
    //Thread _thread_;

    //bool stop_thread_ = false;

    // Start is called before the first frame update
    void Start()
    {

       // Debug.Log("Start a thread.");
        //_thread_ = new Thread(NetMQThread);
        //_thread_.Start();

        //new Thread(() =>
        //{

        //});
    }

    //void NetMQThread()
    //{
    //    AsyncIO.ForceDotNet.Force();
    //    NetMQConfig.Linger = new TimeSpan(0, 0, 1);

    //    _server = new ResponseSocket();
    //    _server.Options.Linger = new TimeSpan(0, 0, 1);
    //    _server.Bind($"tcp://*:{_port}");
    //    //print($"server on {_port}");

    //    _client = new RequestSocket();
    //    _client.Options.Linger = new TimeSpan(0, 0, 1);
    //    _client.Connect($"tcp://localhost:{_port}");
    //   // print($"client connects {_port}");

    //    Assert.IsNotNull(_server);
    //    Assert.IsNotNull(_client);


    //    c_sent = $"Request {_cnter}";
    //    _client.SendFrame(c_sent);
    //    //print($"client sents: {c_sent}");

    //    var s_recv = _server.ReceiveFrameString();
    //    //print($"server receives {s_recv}");

    //    var s_sent = $"Response {_cnter}";
    //    _server.SendFrame(s_sent);
    //    //print($"Server sents: {s_sent}");

    //    var c_recv = _client.ReceiveFrameString();
    //    //print($"client receives {c_recv}");

    //    //transform.position += Vector3.one * 0.0005f;
    //    _cnter++;

    //}

    //void OnDisable()
    //{
    //    _thread_.Join();
    //    _client?.Dispose();
    //    _server?.Dispose();
    //    NetMQConfig.Cleanup(false);
    //}

    // Update is called once per frame
    void Update()
    {
       // print($"{c_sent}");
    }
}
