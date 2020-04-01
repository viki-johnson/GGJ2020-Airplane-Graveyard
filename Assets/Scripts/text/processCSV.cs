using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class processCSV : MonoBehaviour
{
    // Start is called before the first frame update

    public TextAsset txt;
    public string[] lines;
    public engineerLines engineer;
    void Start()
    {
        splitLines();

        for(int q=0; q<lines.Length; q++){
            assignLines(lines[q]);
        }
    }

    void splitLines(){
        string text = txt.text;
        lines = text.Split('\n');
    }


    void assignLines(string str){
        string[] temp = str.Split(new[] {','}, 6); 

        switch(temp[1]){
            case"engineer":
            engineer.processLines(temp);
            break;
        }
    }
}
