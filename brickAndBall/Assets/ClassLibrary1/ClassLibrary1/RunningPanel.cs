using UnityEngine;
using System.Collections;

public class RunningPanel : MonoBehaviour {

    public GUISkin guiSkin;
    public bool isPause=false;
	// Use this for initialization
    public bool isShowControlBtn = false;
	void Start () {
	   DontDestroyOnLoad(gameObject);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        {
            //GUILayout.FlexibleSpace();
            //
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                //
                if (isShowControlBtn)
                {

                    if (GUILayout.Button("hideControl"))
                    {
                        isShowControlBtn = !isShowControlBtn;
                        Debug.Log("click hideControl");
                     

                    }

                }
                else
                {
                    if (GUILayout.Button("showControl"))
                    {
                        isShowControlBtn = !isShowControlBtn;
                        Debug.Log("click showControl");
                     

                    }

                }

                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    //GUILayout.FlexibleSpace();
                    //
                    
                    
                    if (!isPause)
                    {
                        if (GUILayout.Button("Pause"))
                        {
                            isPause = !isPause;
                            Debug.Log("click Pause");
                            Time.timeScale = 0;
                          
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Continue"))
                        {
                            isPause = !isPause;
                            Debug.Log("click Continue");
                            Time.timeScale = 1;
                        }
                        GUILayout.Space(20);
                        if (GUILayout.Button("Stop&Menu"))
                        {
                            Debug.Log("click Stop&Menu");
                            

                            GameObject paddle = GameObject.Find("paddle");
                            if (paddle)
                            {
                                paddleScript paddleS = paddle.GetComponent<paddleScript>();
                                if (paddleS)
                                {
                                    paddleS.exitPlay();

                                }
                            }
                            Application.LoadLevel("menu");

                        }
                    }
                    
                    GUILayout.Space(20);
                   // GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                //
                //GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            //
            GUILayout.FlexibleSpace();
              GUILayout.BeginHorizontal();
            {
                if (isShowControlBtn)
                {
                    

                   
                    //
                    GameObject paddle = GameObject.Find("paddle");

                    if (paddle)
                    {
                        paddleScript paddleS = paddle.GetComponent<paddleScript>();
                        if (paddleS)
                        {
                            if (GUILayout.Button("left"))
                            {

                                Debug.Log("click left");
                                paddleS.ClickMoveLeft();

                            }
                            GUILayout.FlexibleSpace();

                            if (GUILayout.Button("right"))
                            {

                                Debug.Log("click right");
                                paddleS.ClickMoveRight();

                            }


                        }
                    }

                }

           }
            GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
        }
        GUILayout.EndVertical();

        GUILayout.EndArea();



    }
}
