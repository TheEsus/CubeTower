using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    private bool _collisionSet;
    public GameObject restartBotton;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Cube" && !_collisionSet){
            for(int i = other.transform.childCount-1; i >= 0; i--){
                Transform child = other.transform.GetChild(i);
                child.gameObject.AddComponent<Rigidbody>();
                child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(70f,Vector3.up, 5f);
                child.SetParent(null);
            }
            restartBotton.SetActive(true);
            Camera.main.transform.position -= new Vector3(0,0,3f);
            Destroy(other.gameObject);
            _collisionSet = true;
        }
    }
}
