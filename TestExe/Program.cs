using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
namespace TestExe
{
    class Program
    {
        static     string PrintArray(string []arrs,string delim=","){
       string res="Array(";
       for(int i=0;i<arrs.Length;i++){
           if(i!=0)res+=delim;
           res+=arrs[i];
       }
       res += ")";
       Console.WriteLine(res);
       return res;
   }
        static string readTxt(string path)
        {
            StreamReader objReader = new StreamReader(path);
  
            return objReader.ReadToEnd();
        
        }
        static string txt;
        static void Main(string[] args)
        {
             txt = readTxt("map.txt");
             Console.WriteLine("<txt>"+txt+"</txt>");
            //
          
             Analyse(txt);
             Console.ReadKey();
        }
        static void Log(string str)
        {
            //if(str==null){

            Console.Write(str);
        }
       static  void Analyse(string txt)
        {
         Dictionary<string, string> varValueTable = new System.Collections.Generic.Dictionary<string, string>();
        string[] seps = {"\n","\r"};
        string[] lines = txt.Split(seps, System.StringSplitOptions.RemoveEmptyEntries);
        string dealTxt = "";
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i]=lines[i].TrimStart(' ');
            if (lines[i].StartsWith("//")) lines[i] += ";";
            dealTxt+=lines[i]+" ";
        }
          string[] newseps = {";"};
          lines = dealTxt.Split(newseps, System.StringSplitOptions.RemoveEmptyEntries);
          
          for (int i = 0; i < lines.Length; i++)
          {
              string line = lines[i];
              line=line.Trim(' ','\n','\r','\t');
              if (line == null || line == "") continue;
              if (!lines[i].StartsWith("//")){
                  string []tokens=line.Split('=');
                  if (tokens.Length<=1) { Log("error! can't find =<line>"+line+"</line>"); continue; }
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
    }
}
