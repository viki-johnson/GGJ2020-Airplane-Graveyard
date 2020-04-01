using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemTracker : MonoBehaviour
{
    // Start is called before the first frame update

    public int item00;
    public int item01;
    public int item02;
    public int item03;

    public engineerLines _engineerLines;
    public inventory _inventory;

    public TextMeshProUGUI invBox;

    void Start()
    {
        item00 = (int)Random.Range(1,4);
        item01 = (int)Random.Range(1,4);
        item02 = (int)Random.Range(1,4);
        item03 = (int)Random.Range(1,4);
    }

    void Update(){
        invBox.text = item00 + "\n" + item01 + "\n" + item02 + "\n" + item03; 
    }

    // Update is called once per frame
    public bool checkIsNeeded(int num){
        switch(num){
            case 0:
                if(item00>0){
                    item00--;
                    _inventory.inv = 10;
                    _inventory.hasObject = false;
                    return true;
                }else{
                    return false;
                }
                break;
            case 1:
                if(item01>0){
                    item01--;
                    _inventory.inv = 10;
                    _inventory.hasObject = false;
                    return true;
                }else{
                    return false;
                }
                break;
            case 2:
                if(item02>0){
                    item02--;
                    _inventory.inv = 10;
                    _inventory.hasObject = false;
                    return true;
                }else{
                    return false;
                }
                break;
            case 3:
                if(item03>0){
                    item03--;
                    _inventory.inv = 10;
                    _inventory.hasObject = false;
                    return true;
                }else{
                    return false;
                }
                break;
            default:
                return false;
        }
        if(item00 == 0 && item01 == 0 && item02 == 0 && item03 == 0){
            _engineerLines.finished = true;
        }
    }
}
