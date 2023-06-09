using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    private CubePosition nowCube = new CubePosition(0,1,0);
    public float cubeChangeplaceSpeed = 0.5f;
    public Transform cubeToPlace;
    public GameObject cubeToCreate, allCubes;
    public GameObject[] canvasStartPage;
    private Rigidbody allCubesRB;
    private bool isLose, firstCube;
    
    private Coroutine showCubePlace;

    private List<Vector3> AllCubePositions = new List<Vector3>{
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(-1,0,0),
        new Vector3(0,1,0),
        new Vector3(0,0,1),
        new Vector3(0,0,-1),
        new Vector3(1,0,1),
        new Vector3(-1,0,-1),
        new Vector3(-1,0,1),
        new Vector3(1,0,-1),
    };
    private void Start() {
        allCubesRB = allCubes.GetComponent<Rigidbody>();
        showCubePlace = StartCoroutine(ShowCubePlace());
    } 

    private void Update() {
        if((Input.GetMouseButtonDown(0) || Input.touchCount > 0) 
        && cubeToPlace != null
        && !EventSystem.current.IsPointerOverGameObject()){
#if !UNITY_EDITOR
            if(Input.GetTouch(0).phase != TouchPhase.Began)
                return;
#endif
            if(!firstCube){
                firstCube = true;
                foreach(GameObject obj in canvasStartPage)
                    Destroy(obj);
            }
            GameObject newCube =  Instantiate(
                cubeToCreate,
                cubeToPlace.position,
                Quaternion.identity ) as GameObject;

            newCube.transform.SetParent(allCubes.transform);
            nowCube.setVector(cubeToPlace.position);
            AllCubePositions.Add(nowCube.GetVector3());
            
            allCubesRB.isKinematic = true;
            allCubesRB.isKinematic = false;

            SpawnPositions();
        }

        if(!isLose && allCubesRB.velocity.magnitude > 0.1f){
            Destroy(cubeToPlace.gameObject);
            isLose = true;
            StopCoroutine(showCubePlace);
        }
    }

    IEnumerator ShowCubePlace(){
        while(true){
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangeplaceSpeed);
        }
    }
    private void SpawnPositions(){
        List<Vector3> positions = new List<Vector3>();
        if(IsPositionEmpty(new Vector3(nowCube.x+1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x+1, nowCube.y, nowCube.z));
        if(IsPositionEmpty(new Vector3(nowCube.x-1, nowCube.y, nowCube.z))&& nowCube.x - 1 != cubeToPlace.position.x)
            positions.Add(new Vector3(nowCube.x-1, nowCube.y, nowCube.z));
        
        if(IsPositionEmpty(new Vector3(nowCube.x, nowCube.y+1, nowCube.z))&& nowCube.y + 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y+1, nowCube.z));
        if(IsPositionEmpty(new Vector3(nowCube.x, nowCube.y-1, nowCube.z))&& nowCube.y - 1 != cubeToPlace.position.y)
            positions.Add(new Vector3(nowCube.x, nowCube.y-1, nowCube.z));
        
        if(IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z+1))&& nowCube.z + 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z+1));
        if(IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z-1))&& nowCube.z - 1 != cubeToPlace.position.z)
            positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z-1));
        
        cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];

    }
    private bool IsPositionEmpty(Vector3 targetPos){
        if(targetPos.y == 0)
            return false;
        
        foreach(Vector3 pos in AllCubePositions){
            if(pos.x == targetPos.x 
            && pos.y == targetPos.y 
            && pos.z == targetPos.z)
                return false;
        

        }
        return true;
    }
}

struct CubePosition
{
    public int x, y, z;
    public CubePosition(int x, int y, int z){
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 GetVector3(){
        return new Vector3(x,y,z);
    }

    public void setVector(Vector3 position){
        x = Convert.ToInt32(position.x);
        y = Convert.ToInt32(position.y);
        z = Convert.ToInt32(position.z);
    }
    
}
