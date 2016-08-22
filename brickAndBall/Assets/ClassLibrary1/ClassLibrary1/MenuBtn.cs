using UnityEngine;
using System.Collections;

public class MenuBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject runningPanel=GameObject.Find("RunningPanel");
        if (runningPanel)Destroy(runningPanel);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public GUISkin guiSkin;
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
                    //center:
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Menu"))
                    {
                        Debug.Log("click Menu");
                        Application.LoadLevel("menu");
                    }
                   // GUILayout.FlexibleSpace();
                    
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
	
    
    //
}
