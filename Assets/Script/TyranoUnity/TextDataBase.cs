using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="TextDataBase", menuName="DataBase/text")]
public class TextDataBase : ScriptableObject {
    public List<string> textdatabase = new List<string>();

    public List<string> GetTextDataList(){
        return this.textdatabase;
    }

    public void AddTextData(string text){
        this.textdatabase.Add(text);
    }

    public int GetTextDataLength(){
        return this.textdatabase.Count;
    }
}
