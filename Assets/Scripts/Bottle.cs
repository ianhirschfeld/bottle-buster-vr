using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        Rigidbody colRigidbody = collision.rigidbody;
        if (colRigidbody != null && collision.gameObject.CompareTag("Explodable")) {
            colRigidbody.AddExplosionForce(20f, transform.position, 10f, 1f, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Finish")) {
            Destroy(this);
        }
    }

}
