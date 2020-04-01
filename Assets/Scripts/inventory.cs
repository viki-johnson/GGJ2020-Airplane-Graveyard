using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    // Start is called before the first frame update

    public bool hasObject;
    public int inv = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == "collect" && !hasObject){
            hasObject = true;

            switch(c.gameObject.name){
                case"collectable_00(Clone)":
                    Debug.Log("one");
                    inv=0;
                    break;
                case"collectable_01(Clone)":
                Debug.Log("two");
                    inv=1;
                    break;
                case"collectable_02(Clone)":
                Debug.Log("three");
                    inv=2;
                    break;
                case"collectable_03(Clone)":
                Debug.Log("four");
                    inv=3;
                    break;
            }

            Destroy(c.gameObject);
        }
    }
}
