using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BottleSpawn : MonoBehaviour {

    public GameObject bottleObject;
    HashSet<GameObject> objectsColliding = new HashSet<GameObject>();

    void OnTriggerEnter(Collider other) {
        GameObject collidedObject = other.gameObject;
        if (collidedObject) {
            objectsColliding.Add(collidedObject);
        }
    }

    void OnTriggerExit(Collider other) {
        GameObject collidedObject = other.gameObject;
        if (collidedObject) {
            objectsColliding.Remove(collidedObject);
        }

        if (objectsColliding.Count == 0) {
            Instantiate(bottleObject, transform);
        }
    }

}
