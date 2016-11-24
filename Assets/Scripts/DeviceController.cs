using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeviceController : MonoBehaviour {

    SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObject.index); } }

    HashSet<Interactable> objectsHoveringOver = new HashSet<Interactable>();
    Interactable closestObject;
    Interactable interactingObject;

    void Start() {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update() {
        if (controller == null) {
            Debug.Log("Controller not initialized.");
            return;
        }

        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)) {
            float minDistance = float.MaxValue;
            float distance;
            foreach (Interactable obj in objectsHoveringOver) {
                distance = (obj.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance) {
                    minDistance = distance;
                    closestObject = obj;
                }
            }

            interactingObject = closestObject;
            closestObject = null;

            if (interactingObject) {
                interactingObject.OnTriggerPressDown(this);
            }
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) && interactingObject) {
            interactingObject.OnTriggerPressUp(this);
        }
    }

    void OnTriggerEnter(Collider collider) {
        Interactable collidedObject = collider.GetComponent<Interactable>();
        if (collidedObject) {
            objectsHoveringOver.Add(collidedObject);
        }
    }

    void OnTriggerExit(Collider collider) {
        Interactable collidedObject = collider.GetComponent<Interactable>();
        if (collidedObject) {
            objectsHoveringOver.Remove(collidedObject);
        }
    }

}
