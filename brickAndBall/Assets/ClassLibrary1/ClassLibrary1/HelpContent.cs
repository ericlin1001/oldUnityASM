using UnityEngine;
using System.Collections;

public class HelpContent : MonoBehaviour {
    public TextAsset txtFile;
	// Use this for initialization
	void Start () {
        GUIText contentText = GetComponent<GUIText>();
        if (contentText)
        {
            contentText.text = txtFile.text;
            //contentText.text = "<html><head>a</head>\n\r<br/>hello\rnewline\rnnextline<body><h1>a<i>adfadf</i><b>sdf</b></h1><p>adfsd</p></body></html>";
        }
        else
        {
            Debug.Log("can't find GUIText");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
