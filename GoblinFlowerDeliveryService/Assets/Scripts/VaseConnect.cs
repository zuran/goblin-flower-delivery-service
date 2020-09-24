using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaseConnect : MonoBehaviour
{
    void OnTriggerStay(Collider collider)
    {
        //if (collider.gameObject.transform.parent == null) return;
        var parent = collider.gameObject; //.transform.parent.gameObject;
        if (parent.tag != "Flower") return;
        var rb = parent.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        parent.transform.SetParent(transform);
    }

    public void GrabFlower(GameObject flower) {
        var rb = flower.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        flower.transform.SetParent(null);
    }

    public void ReleaseFlower(GameObject flower) {
        var rb = flower.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        flower.transform.SetParent(null);
    }
}
