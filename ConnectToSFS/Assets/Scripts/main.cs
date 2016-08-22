using UnityEngine;
using System.Collections;

using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Requests;
public class main : MonoBehaviour {
    public string userName = "";
    public string zoneName = "ConnectToSFS";
    public string serverIP = "127.0.0.1";
    public int serverPort = 9933;
    
    SmartFox sfs;
	// Use this for initialization
	void Start () {
        sfs = new SmartFox();
        sfs.ThreadSafeMode = true;

        
        
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);

        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
        //
        sfs.Connect(serverIP, serverPort);
        Debug.Log("Start");
	}
    void OnApplicationQuit()
    {
        if (sfs.IsConnected)
        {
            sfs.Disconnect();
            Debug.Log("disconnecting...");
        }
        Debug.Log("OnApplicationQuit");
    }
    void OnConnection(BaseEvent e)
    {
        Debug.Log("OnConnection");
        if ((bool)e.Params["success"])
        {
            Debug.Log("connect successfully!");
            sfs.Send(new LoginRequest(userName,"",zoneName));
        }
        else
        {
            Debug.Log("connection Failed!");
        }

    }
    void OnLogin(BaseEvent e)
    {
        Debug.Log("logged in :" + e.Params["user"]);
    }
    void OnLoginError(BaseEvent e)
    {
        Debug.Log("login error (" + e.Params["errorCode"]+"): "+e.Params["errorMessage"]);
    }

	// Update is called once per frame
	void Update () {
        sfs.ProcessEvents();
	}
}
