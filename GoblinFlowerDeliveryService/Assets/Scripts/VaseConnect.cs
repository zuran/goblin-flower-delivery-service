using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaseConnect : MonoBehaviour
{
    void OnTriggerStay(Collider collider)
    {
        var flower = collider.gameObject;
        if (flower.tag != "Flower" && flower.transform.parent != null)
            flower = flower.transform.parent.gameObject;
        if (flower.tag != "Flower") return;
        var rb = flower.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        flower.transform.SetParent(transform);
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
