using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCollectables : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] _c;
    public List<GameObject> collectables00;
    public List<GameObject> collectables01;
    public List<GameObject> collectables02;
    public List<GameObject> collectables03;
    public float minY;

    public GameObject tree;

    public float maxX, maxZ;
    public int amount;

    public LandscapeMaker _lm;
    float q =0;
    void Start()
    {
        StartCoroutine(setUp(collectables00));
        StartCoroutine(setUp(collectables01));
        StartCoroutine(setUp(collectables02));
        StartCoroutine(setUp(collectables03));
    }

    IEnumerator setUp(List<GameObject> lgo){
        yield return new WaitForSeconds(0.01f);

        // collectables.Sort((a, b)=> 1 - 2 * Random.Range(0, 1));
        while(lgo.Count>amount){
            int t = (int)Random.Range(0,lgo.Count);
            Destroy(lgo[t]);
            lgo.RemoveAt(t);
        }

        // Debug.Log(collectables.Count);
    }

    public void spawn(Vector3 p){
        if(Random.Range(0,4)>2){
            int num = (int)q%4;
            // Debug.Log(num);

            GameObject go = Instantiate(_c[num], Vector3.zero, Quaternion.identity);
            // go.name = "collectable " + q;
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(p.x, p.y+0.1f, p.z);
            collectables00.Add(go);
            q++;
        }

    }
}
