using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;

public class AirGunFight : MonoBehaviour
{

    WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:5500/");

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
        //ws.Send(20.ToString("D"));
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

    public void TurnServo(int state)
    {
        /*ws = new WebSocket("ws://localhost:5500");

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

        ws.Connect();*/
         //ws.Send(state.ToString("D"));
    }
}
