using UnityEditor;
using System.IO;
using System.Media;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainEditor : EditorWindow
{
    private static GameObject canvas_object;
    private static Canvas canvas;
    private static Transform canvas_transform;
    private GameObject text_object;
    private Text text;
    private TextData text_data;
    private Vector3 text_pos;
    private static string text_file_path = "";
    private static string text_file_name = "";
    private string textData;
    private string[] splitText;
    private float text_field_pos_x = 0.0f;
    private float text_field_pos_y = 0.0f;
    public static TextDataBase setting;


    [MenuItem("TyranoUnity/Main")]
    static void Open(){
        EditorWindow.GetWindow<MainEditor>("Text_Conversion");
        if(!GameObject.Find("Canvas")){
            canvas_object = new GameObject();
            canvas_object.name = "Canvas";
            canvas_object.AddComponent<Canvas>();
            canvas = canvas_object.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas_object.AddComponent<CanvasScaler>();
            canvas_object.AddComponent<GraphicRaycaster>();
            canvas_object.AddComponent<Transform>();
        }else{
            canvas_object = GameObject.Find("Canvas");
            canvas = canvas_object.GetComponent<Canvas>();
        }
    }
    private void OnGUI()
    {
        GUILayout.Space(15f);
        text_object = EditorGUILayout.ObjectField("TextField", text_object, typeof(GameObject), true) as GameObject;
        GUILayout.Space(15f);
        GUILayout.Label("テキストフィールドのx座標");
        text_field_pos_x = EditorGUILayout.DelayedFloatField(text_field_pos_x);
        GUILayout.Space(15f);
        GUILayout.Label("テキストフィールドのy座標");
        text_field_pos_y = EditorGUILayout.DelayedFloatField(text_field_pos_y);
        GUILayout.Space(15f);
        if(GUILayout.Button("適用")){
            if(text_object != null){
                text_pos.x = text_field_pos_x + 295f;
                text_pos.y = text_field_pos_y + 166f;
                text_pos.z = 0f;
                GameObject text_obj = Instantiate(text_object, text_pos, Quaternion.identity) as GameObject;
                canvas_transform = canvas_object.GetComponent<Transform>();
                text_obj.transform.SetParent(canvas_transform);
            }
        }
        GUILayout.Space(15f);
        //テキストファイル読み込み
        if(GUILayout.Button("Add File")){
            if(text_object != null){
                text_file_path =  EditorUtility.OpenFilePanel("Open txt", "", "TXT");
                if(text_file_path == null){
                    return;
                }else{
                    text_file_name = Path.GetFileNameWithoutExtension(text_file_path);
                    TextRoad();
                }
            }else{
                Debug.Log("TextFieldを設定してください");
            }
        }
    }

    //テキストファイル内部読み込み
    private void TextRoad(){
        TextAsset textasset = new TextAsset();
        textasset = Resources.Load(text_file_name, typeof(TextAsset)) as TextAsset;
        if(textasset == null){
            return;
        }else{
            textData = textasset.text;

            splitText = textData.Split(char.Parse("\n"));
            var path = "Assets/Editor/TextDataBase.asset";
            setting = AssetDatabase.LoadAssetAtPath<TextDataBase>(path);
            if(setting!=null)
            {
                File.Delete(path);
            }
            setting = ScriptableObject.CreateInstance<TextDataBase>();
            AssetDatabase.CreateAsset(setting, path);
            for(int i=0;i<splitText.Length;i++){
                setting.AddTextData(splitText[i]);
            }
            EditorUtility.SetDirty(setting);
        }
    }

    public static string GetTextFilePath(){
        return text_file_path;
    }

    public static string GetTextFileName(){
        return text_file_name;
    }
}
