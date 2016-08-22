
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

using System.Linq;
using System.Text;
using UnityEngine;

public class TextReader: MonoBehaviour {

	
    //
    int numRows = 5;
    int numCols = 5;
    int startTag=2;
int targetTag=3;
    int[] mapData;
    string mapName;

    //
    Vector3 startPos=new UnityEngine.Vector3();

    public GameObject basicMazeUnit;

    public GameObject basicMazeTargetUnit;

    public GameObject basicMazePlayer;


    public Vector3 GetStartPos(){
        return startPos;
    }
    // Use this for initialization
   void Start() {
       LoadResourceMaps();
       NextLevel();
	}
   // Update is called once per frame
   public void HitTarget()
   {
    
       NextLevel();

   }
   List<UnityEngine.GameObject> createObjs = new System.Collections.Generic.List<UnityEngine.GameObject>();

   TextAsset currMap=null;
   UnityEngine.Object[] AllMaps;
    //
   string[] externMapData;
   void LoadResourceMaps()
   {
       List<UnityEngine.Object> mapLists = new List<UnityEngine.Object>();

      
      UnityEngine.Object[]assets=Resources.LoadAll("maps");
      mapLists.AddRange(assets);
       //

      Debug.Log("in Resources/maps find:");
       foreach (UnityEngine.Object asset in assets)
       {
           Debug.Log(asset.name);
       }
       
       AllMaps = mapLists.ToArray();

   }
   void SearchExternalMap()
   {
       //
       //find in absolute path:
       string fpath = "./newMaps/";
       string filetype = "txt";
       Debug.Log("pwd:" + Directory.GetCurrentDirectory());
       List<string> exterlMapList = new List<string>();
       if (Directory.Exists(fpath))
       {

           foreach (string f in Directory.GetFiles(fpath, "*." + filetype))
           {
               string filename = Path.GetFileName(f);
               exterlMapList.Add(File.ReadAllText(f));
               Debug.Log("{0},{1}" + f + filename);
               Debug.Log("{0},{1}" + exterlMapList.Count + exterlMapList);
               //File.Copy(f, Path.Combine(tpath, filename), true);
           }
           foreach (string f in Directory.GetDirectories(fpath))
           {
               // CopyAllFiles(f, tpath, filetype);
           }
       }
       externMapData = exterlMapList.ToArray();
       //
   }
   string NextMap()
   {
       SearchExternalMap();
       string res=null;

       Debug.Log(UnityEngine.Random.Range(0, 2));
       //
       if (UnityEngine.Random.Range(0, AllMaps.Length + externMapData.Length) < AllMaps.Length)
       {
           if (AllMaps.Length <= 0)
           {
               Debug.Log("no map found!");
               return null;
           }
           if (currMap != null && AllMaps.Length <= 1)
           {
               Debug.Log("no othe map availabe!");
               return null;
           }
           //
           TextAsset newMap;
           while ((newMap = AllMaps[UnityEngine.Random.Range(0, AllMaps.Length)] as TextAsset) == currMap) ;
           if (newMap == null) { Debug.Log("can't open maps"); ; }

           Debug.Log("user new map:" + newMap.name);
           currMap = newMap;
           res=currMap.text;
       }
       else
       {
           res = externMapData[UnityEngine.Random.Range(0, externMapData.Length)];
       }
       
       return res;
   }
   void NextLevel()
   {
       Time.timeScale = 0;
       ReadMapFromStr(NextMap());
       BuildMaze();
       Time.timeScale = 1;
   }
   void BuildMaze()
   {
       
       foreach(UnityEngine.GameObject obj in createObjs){
           Destroy(obj); 
       }
       createObjs.Clear();

       for (int r = 0; r < numRows; r++)
       {
           for (int c = 0; c < numCols; c++)
           {
                int v = mapData[r * numCols + c];
                if (v == 0) continue;
               //
              
               float size = 1;
                   float planeX = (float)c * size;
                   float planeY = (float)r * size;
                   Vector3 newPos=new Vector3(planeX, 0, planeY);;

                if (v == startTag)
               {
                   startPos=newPos;
                   Debug.Log("set player to " + newPos);
                   basicMazePlayer.transform.parent = gameObject.transform;
                   basicMazePlayer.transform.localPosition = startPos;
                   

               }
               else if (v == targetTag)
               {
             
                   basicMazeTargetUnit=(GameObject)Instantiate(basicMazeTargetUnit);
                  
                          basicMazeTargetUnit.transform.parent = gameObject.transform;
                          basicMazeTargetUnit.transform.localPosition = newPos;
                          createObjs.Add(basicMazeTargetUnit);
               }
               else
               {
                   //Place a cube in ...
                   
                   
                   if (v == 1)
                   {
                       GameObject newUnit;
                       newUnit = (GameObject)Instantiate(basicMazeUnit);
                       newUnit.transform.parent = this.gameObject.transform;

                       newUnit.transform.localPosition = newPos;
                       newUnit.transform.rotation = Quaternion.identity;

                       createObjs.Add(newUnit);
                   }
                   else if (v == 2)
                   {
                   }
               }
               //
              
              // newUnit.transform.position = new Vector3(planeX, 0, -planeY);
               
           }
       }
   }
   void ReadMapFromStr(String map)
   { 
       //    Debug.Log(txtFile.text);
       if (map == null || map == "")
       {
           Debug.Log("can't build map");
           return;
       }
       Analyse(map);

       numRows = numCols = -1;
       if (GetTxtStr("mapName") != null) mapName = GetTxtStr("mapName");
       if(GetTxtStr("numRows")!=null)numRows = int.Parse(GetTxtStr("numRows"));
       if (GetTxtStr("numCols") != null) numCols = int.Parse(GetTxtStr("numCols"));
       if (GetTxtStr("startTag") != null) startTag = int.Parse(GetTxtStr("startTag"));
       if (GetTxtStr("targetTag") != null) targetTag = int.Parse(GetTxtStr("targetTag"));
       if (GetTxtStr("mapData") != null) mapData = StrToInts(GetTxtStr("mapData"));

       Debug.Log("use map[mapName:" + mapName+"]");
       //
       if (mapData == null || numRows * numCols != mapData.Length)
       {
           Debug.Log("Error:numRows , numCols or mapData set wront");

            Debug.Log("numRows=" + numRows);
            Debug.Log("numCols=" + numCols);
            Debug.Log("startTag=" + startTag);
            Debug.Log("targetTag=" + targetTag);
            Debug.Log("mapData=" + mapData);
            PrintArray(mapData);
       }
       // 
   
   }
   void Update()
   {

   }

   
   void Log(string str)
   {
       Debug.Log(str);
   }

    //for analyse the block.
   Dictionary<string, string> varValueTable = new System.Collections.Generic.Dictionary<string, string>();
   string GetTxtStr(string key)
   {
       if (varValueTable.ContainsKey(key))
       {
           return varValueTable[key];
       }
       return null;
   }
  




   float[] StrToFloats(string str)
   {

       if (str == null) return null;
       List<float> res = new List<float>();
       str = str.Replace("[", "");
       str = str.Replace("]", "");
       string[] values = str.Split(' ');
       foreach (string v in values)
       {
           string vv = v.Trim(' ', '\t', '\n', '\r');
           if (vv == null || vv == "") continue;
           res.Add(float.Parse(vv));

       }
       return res.ToArray();
   }

   int[] StrToInts(string str)
   {

       if (str == null) return null;
       List<int> res = new List<int>();
       str = str.Replace("[", "");
       str = str.Replace("]", "");
       string[] values = str.Split(' ');
       foreach (string v in values)
       {
           string vv = v.Trim(' ', '\t', '\n', '\r');
           if (vv == null || vv == "") continue;
           res.Add(int.Parse(vv));

       }
       return res.ToArray();
   }
   void Analyse(string sourceTxt)
   {
       varValueTable.Clear();
       while (true)
       {
           int blockCommentBeginIndex = sourceTxt.IndexOf("/*");
           int blockCommentEndIndex = sourceTxt.Length - 1;
           if (blockCommentBeginIndex != -1)
           {
               blockCommentEndIndex = sourceTxt.IndexOf("*/", blockCommentBeginIndex + 2);
               if (blockCommentEndIndex == -1) blockCommentEndIndex = sourceTxt.Length - 2;
               blockCommentEndIndex++;
               //
               //Debug.Log("find block comment:" + sourceTxt.Substring(blockCommentBeginIndex, blockCommentEndIndex - blockCommentBeginIndex + 1));
               sourceTxt=sourceTxt.Remove(blockCommentBeginIndex, blockCommentEndIndex - blockCommentBeginIndex + 1);
           }
           else
           {
               break;
           }
       }
       //
       string[] seps = { "\n", "\r" };
       string[] lines = sourceTxt.Split(seps, System.StringSplitOptions.RemoveEmptyEntries);
       string dealTxt = "";
       for (int i = 0; i < lines.Length; i++)
       {
           lines[i] = lines[i].TrimStart(' ');
           if (lines[i].StartsWith("//")) lines[i] += "; ";
           dealTxt += lines[i] + " ";
       }
       string[] newseps = { ";" };
       lines = dealTxt.Split(newseps, System.StringSplitOptions.RemoveEmptyEntries);

       for (int i = 0; i < lines.Length; i++)
       {
           string line = lines[i];
           line = line.Trim(' ', '\n', '\r', '\t');
           if (line == null || line == "") continue;

           if (!line.StartsWith("//"))
           {
               
               string[] tokens = line.Split('=');
               if (tokens.Length <= 1) { Log("error! can't find =<line>" + line + "</line>"); Debug.Log("lines[i].StartsWith(:" + lines[i].StartsWith("//")); continue; }
               string left = tokens[0].Trim(' ');
               string right = tokens[1].Trim(' ');
               //
               varValueTable.Add(left, right);

           }
       }
       
       //
       /* string varN;
        varN = "adfad";
        if (varValueTable.ContainsKey(varN))
        {
            Log("find:" + varValueTable[varN]);
        }*/
   }
    //end anaylse the 
   void ShowAllVarValue()
   {
       foreach (KeyValuePair<string, string> a in varValueTable)
       {
           Debug.Log(a.Key + "=" + a.Value);
       }
   }
   static string PrintArray(Array arrs, string delim = ",")
   {
       if (arrs == null) Debug.Log("null");
       string res = "Array(";
       for (int i = 0; i < arrs.Length; i++)
       {
           if (i != 0) res += delim;
           res += arrs.GetValue(i);
       }
       res += ")["+arrs.Length+"]";
       Debug.Log(res);
       return res;
   }
	
}
