using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using UnityEngine.UI;
using System;

public class Echo : MonoBehaviour
{
    public InputField inputFeld;
    public Text text;

    byte[] readBuff= new byte[1024];
    string recvStr = "";

    Socket socket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (socket == null)
            return;
        if (socket.Poll(0, SelectMode.SelectRead)) {
            byte[] readBuff = new byte[1024];
            int count = socket.Receive(readBuff);
            string recvstr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
            text.text = recvStr;
        }
    }
    //点击连接按钮
    public void Connection()
    {
        //创建一个socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //连接IP端口
        socket.BeginConnect("127.0.0.1", 8888,ConnectCallback,socket);
    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket) ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Success");
            //socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallBack, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Scoket Connect Fail" + ex.ToString());
        }
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndReceive(ar);
            string s = System.Text.Encoding.Default.GetString(readBuff, 0, count);
            recvStr = s + "\n" + recvStr;

           // socket.BeginReceive(readBuff, 0, 1024, 0, ReceiveCallBack, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Scoket Receive Fail" + ex.ToString());
        }
    }

    public void Send()
    {
        //send
        string sendStr = inputFeld.text;
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        socket.BeginSend(sendBytes,0,sendBytes.Length,0,SendCallback,socket);

        

        //Close
        //socket.Close();

    }

    private void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket =(Socket) ar.AsyncState;
            int count = socket.EndSend(ar);
            Debug.Log("Socket Send Succ" + count);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Send fail" + ex.ToString());
        }
    }
}
