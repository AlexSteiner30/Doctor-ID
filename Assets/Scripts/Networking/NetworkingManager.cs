using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class NetworkingManager : MonoBehaviour
{
    protected WebSocket ws;

    private void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        ws = new WebSocket("ws://localhost:4285");
        ws.Connect();

        ReceiveMessage();
        SendMessage("Hello World!");
    }

    private void ReceiveMessage()
    {
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message Received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
    }

    public new void SendMessage(string msg)
    {
        if (ws == null)
        {
            return;
        }

        ws.Send(msg);
    }
}