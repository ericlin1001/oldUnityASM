using UnityEngine;
using System.Collections;

public class ReplayButton : MonoBehaviour
{

    public GUISkin guiSkin;

    // Use this for initialization
    void Start()
    {
        GameObject runningPanel = GameObject.Find("RunningPanel");
        if (runningPanel) Destroy(runningPanel);
        
    }
    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        {
            GUILayout.FlexibleSpace();
            //
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                //
                GUILayout.BeginVertical();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Space(40);
                    //
                    if (GUILayout.Button("Play"))
                    {
                        Debug.Log("click Play");
                        Application.LoadLevel("level1");
                    }
                    GUILayout.Space(20);
                    if (GUILayout.Button("Help"))
                    {
                        Debug.Log("click Help");
                        Application.LoadLevel("help");
                    }
                    GUILayout.Space(20);

                    if (GUILayout.Button("About"))
                    {
                        Debug.Log("click About");
                        Application.LoadLevel("about");
                    }

                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Exit Game"))
                    {
                        Debug.Log("click Exit Game");
                        Application.Quit();
                    }
                    GUILayout.Space(20);

                    //
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndVertical();
                //
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
            //
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndVertical();

        GUILayout.EndArea();



    }
    // Update is called once per frame
    void Update()
    {

    }
}

