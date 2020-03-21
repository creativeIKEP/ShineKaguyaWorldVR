using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;

public class WebSocketControl: MonoBehaviour
{

    WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:3000/");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };



        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws.Connect();

    }

    void Update()
    {

           // ws.Send("Test Message");


    }

    void OnDestroy()
    {
       // ws.Close();
        //ws = null;
    }

    public void LigthOn(int state)
    {
        //ws.Send(state.ToString("D"));
    }
}
