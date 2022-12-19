using UnityEditor; // エディタ拡張関連はUnityEditor名前空間に定義されているのでusingしておく。
using System.IO;
using System.Media;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainEditor : EditorWindow
{

    private static string text_file_path = "";
    private static string text_file_name = "";
    private string textData;
    private string[] splitText;

    [MenuItem("TyranoUnity/Main")]
    static void Open(){
        EditorWindow.GetWindow<MainEditor>("Text_Conversion");
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Add File")){
            text_file_path =  EditorUtility.OpenFilePanel("Open txt", "", "TXT");
            if(text_file_path == null){
                return;
            }else{
                text_file_name = Path.GetFileNameWithoutExtension(text_file_path);
                TextRoad();
            }
        }
    }

    private void TextRoad(){
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load(text_file_name, typeof(TextAsset)) as TextAsset;
        if(textasset == null){
            return;
        }else{
            textData = textasset.text;

            splitText = textData.Split(char.Parse("\n"));
            Debug.Log(splitText[3]);
        }
    }

    public static string GetTextFilePath(){
        return text_file_path;
    }

    public static string GetTextFileName(){
        return text_file_name;
    }
}
