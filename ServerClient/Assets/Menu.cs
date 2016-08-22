using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
    public string IP = "127.0.0.1";
    public int Port = 25000;
    public GameObject cube;
    //
    bool isRegisterUI = false;
    bool isLoginUI = false;
    //
    string userName = "eric";
    string pass = "lin";
    //
   
    void OnGUI()
    {
        GUILayout.BeginVertical();
        {
            //
            GUILayout.BeginHorizontal();
            {
               // GUILayout.FlexibleSpace();
                //
                GUILayout.BeginVertical();
                {
                    if (Network.peerType == NetworkPeerType.Disconnected)
                    {


                        if (GUILayout.Button("Start Clien"))
                        {
                            Network.Connect(IP, Port);
                        }
                        if (GUILayout.Button("Start Server"))
                        {
                            Network.InitializeServer(10, Port, true);//question what is userNat??!

                        }

                    }
                    else
                    {

                        if (Network.peerType == NetworkPeerType.Client)
                        {
                            GUILayout.Label("Client");
                            //
                            if (isRegisterUI || isLoginUI)
                            {
                                if (isRegisterUI)
                                {
                                    GUILayout.BeginHorizontal();
                                    GUILayout.Label("user:");
                                    userName = GUILayout.TextArea(userName);
                                    GUILayout.EndHorizontal();

                                    GUILayout.BeginHorizontal();
                                    GUILayout.Label("user:");
                                    pass = GUILayout.TextArea(pass);
                                    GUILayout.EndHorizontal();
                                    if (GUILayout.Button("Register"))
                                    {
                                        networkView.RPC("RegisterUser", RPCMode.Server, userName, pass);
                                        isRegisterUI = false;
                                    }
                                }
                                 if (isLoginUI)
                                {
                                    GUILayout.BeginHorizontal();
                                    GUILayout.Label("user:");
                                    userName = GUILayout.TextArea(userName);
                                    GUILayout.EndHorizontal();

                                    GUILayout.BeginHorizontal();
                                    GUILayout.Label("user:");
                                    pass = GUILayout.TextArea(pass);
                                    GUILayout.EndHorizontal();

                                    if (GUILayout.Button("Login"))
                                    {
                                        networkView.RPC("LoginUser", RPCMode.Server, userName, pass);
                                        isRegisterUI = false;
                                    }
                                }
                            }
                            else
                            {

                                if (GUILayout.Button("Register"))
                                {
                                    isRegisterUI = true;
                                }
                                if (GUILayout.Button("Login"))
                                {
                                    isLoginUI = true;
                                }
                                if (GUILayout.Button("Logout"))
                                {
                                    Network.Disconnect(250);
                                }
                            }
                            //
                            

                        }
                        else if (Network.peerType == NetworkPeerType.Server)
                        {
                            GUILayout.Label("Server");
                            GUILayout.Label("Connection:" + Network.connections.Length);
                            if (GUILayout.Button("Stop Server"))
                            {
                                Network.Disconnect(250);
                            }
                        }


                    }
                }
                GUILayout.EndVertical();
                //
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            //

            //
            ShowLogText();
            //
        }
        GUILayout.EndVertical();

    }

    [RPC]
    void ChangeColor()
    {
        cube.renderer.material.color = Color.green;
    }

    string logText = "";
    void ShowLogText()
    {

        GUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
             GUILayout.Label(logText);
      
        }
        GUILayout.EndHorizontal();

    }
    void Log(string str)
    {
        logText += str+"\n";
        
    }

    void StartPlay()
    {
        if (Network.isClient)
        {
            Application.LoadLevel("startPlay");
        }
    }

    [RPC]
    void LoginSuccess(string user,string userID)
    {
      
        if (Network.isClient)
        {
            Log("LoginSuccess with user:" + user + " userID:" + userID);

            StartPlay();
        }
    }

    [RPC]
    void LoginUser(string user, string pass, NetworkMessageInfo info)
    {
       
        if (Network.isServer)
        {
            if (PlayerPrefs.HasKey(user))
            {
                if (PlayerPrefs.GetString(user) == pass)
                {
                
                    string userID = "123";
                    networkView.RPC("LoginSuccess", info.sender, user, userID);
                    Log("user:" + user + " pass:" + pass + " login  successfully!");
                }
                else
                {
                    Log("can't login user:" + user + " pass:" + pass + " realPass:" + PlayerPrefs.GetString(user));
                }
            }
            else
            {
                Log("not exist user:" + user);
            }
        }
    }

    [RPC]
    void RegisterRes(string user,string pass ,string mess)
    {
        if(Network.isClient){

            Log(mess);
}
    }

    [RPC]
    void RegisterUser(string user, string pass, NetworkMessageInfo info)
    {
        if (Network.isServer)
        {
            if (PlayerPrefs.HasKey(user))
            {
                networkView.RPC("RegisterRes", info.sender, user, pass, " user: " + user + " realPass:" + PlayerPrefs.GetString(user) + " has existed !");
                Log(" user: " + user + " realPass:" + PlayerPrefs.GetString(user) + " has existed !");
            }
            else
            {
                PlayerPrefs.SetString(user, pass);
                networkView.RPC("RegisterRes", info.sender, user, pass, "register user:" + user + " pass:" + pass + " successfully !");
                Log("register user:" + user + " pass:" + pass + " successfully !");
            }

            
        }
        else
        {
            Log("this should not be invoked in client");
        }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
