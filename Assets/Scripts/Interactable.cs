using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

    protected Rigidbody rigidbody;
    protected bool isInteracting;
    protected DeviceController attachedDevice;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }
 
    void TossObject(DeviceController device) {
        rigidbody.velocity = device.controller.velocity;
        rigidbody.angularVelocity = device.controller.angularVelocity;
    }

    public bool IsInteracting() {
        return isInteracting;
    }

    public void OnTriggerPressDown(DeviceController device) {
        attachedDevice = device;
        transform.SetParent(device.transform);
        rigidbody.isKinematic = true;
        isInteracting = true;
    }

    public virtual void OnTriggerPressUp(DeviceController device) {
        if (attachedDevice == device) {
            transform.SetParent(null);
            rigidbody.isKinematic = false;
            attachedDevice = null;
            isInteracting = false;
            TossObject(device);
        }
    }

}
