using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class TextData : MonoBehaviour
{
    private static List<string> textdata = new List<string>();

    private int count = 0;

    private TextDataBase textdatabase;

    private TextMeshProUGUI text_obj;

    void Start(){
        var path = "Assets/Editor/TextDataBase.asset";
        textdatabase = AssetDatabase.LoadAssetAtPath<TextDataBase>(path);
        if(textdatabase != null){
            Debug.Log(textdatabase.GetTextDataList()[0]);
            for(int i=0;i<textdatabase.GetTextDataLength();i++){
                textdata.Add(textdatabase.GetTextDataList()[i]);
            }
        }
        text_obj = this.GetComponent<TextMeshProUGUI>();
        if(text_obj == null){
            Debug.Log("null");
        }
    }

    public void OnClickTextField(){
        Debug.Log(this.name);
        Debug.Log(textdata[0]);
        if((textdata.Count != 0) && textdata.Count > count){
            text_obj.text = textdata[count];
            count++;
        }
    }

}
