using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]

public class LandscapeMaker : MonoBehaviour
{
    public float cellSize = 1f;
    public int width = 24;
    public int height = 24;
    public int zigzag = 10;
    public float bumpyness = 5f;
    public float bumpHeight = 5f;

    public spawnCollectables _sc;
    public spawnTrees _st;
    public spawnBottles _sb;
    public bool main;

    public float r;

    public void create(){
        // r = Random.Range(0,100000);

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();
        MeshBuilder mb = new MeshBuilder(6);

        //points for our plane
        Vector3[,]points = new Vector3[width,height];

        for(int x =0; x<width; x++){
            for(int y=0; y<height; y++){
                points[x,y] = new Vector3(
                    cellSize * x,
                    Mathf.PerlinNoise (
                        (x + r + this.transform.position.x) * bumpyness * 0.1f, 
                        (y + r + this.transform.position.z) * bumpyness * 0.1f)
                        * bumpHeight,
                    cellSize * y);
                 if(points[x,y].y > 2f && main){
                    if(Random.Range(0,4)>2){
                         _sc.spawn(points[x,y]);
                    }else if(Random.Range(0,4)>2){
                        _st.spawn(points[x,y]);
                    }
                 }
                if(points[x,y].y < 1f && main){
                    if(Random.Range(0,70)>68){
                        _sb.spawn(points[x,y]);
                    }
                }


            }
        }

        int submesh = 0;

        for (int x=0; x<width-1; x++){
            for(int y=0; y<height-1; y++){
                Vector3 br = points [x,     y];
                Vector3 bl = points [x+1,   y];
                Vector3 tr = points [x,     y+1];
                Vector3 tl = points [x+1,   y+1];

                if(br.y > 2.15 && bl.y > 2.15 && tr.y > 2.15 && tl.y > 2.15){
                    mb.BuildTriangle(bl, tr, tl, 0);
                    mb.BuildTriangle(bl, br, tr, 0);
                }else{
                    mb.BuildTriangle(bl, tr, tl, 1);
                    mb.BuildTriangle(bl, br, tr, 1);
                }
            }
            submesh ++;
        }

        meshFilter.mesh = mb.CreateMesh();
        meshCollider.sharedMesh = meshFilter.mesh;
    }
}
