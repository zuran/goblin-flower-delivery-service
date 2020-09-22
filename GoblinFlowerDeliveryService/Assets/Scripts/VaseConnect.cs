using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VaseConnect : MonoBehaviour
{
    private CapsuleCollider m_Capsule;
    private GameObject m_CollidingFlower;
    // Start is called before the first frame update
    void Start()
    {
        m_Capsule = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider) {
        if(collider.gameObject.transform.parent == null) return;
        var parent = collider.gameObject.transform.parent.gameObject;
        if(parent.tag != "Flower" ||
            m_CollidingFlower == null) return;
        var rb = m_CollidingFlower.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        m_CollidingFlower.transform.SetParent(transform);
    }

    // void OnTriggerExit(Collider collider) {
    //     Debug.Log("Collision Exit");
    //     if(collider.gameObject.transform.parent == null) return;
    //     var parent = collider.gameObject.transform.parent.gameObject;
    //     if(parent.tag != "Flower" ||
    //         m_CollidingFlower == null) return;
    //     var rb = m_CollidingFlower.GetComponent<Rigidbody>();
    //     rb.isKinematic = false;
    //     rb.useGravity = true;
    //     m_CollidingFlower.transform.SetParent(null);
    // }

    public void GrabFlower(GameObject flower) {
        var rb = flower.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        flower.transform.SetParent(null);
    }


    public void ReleaseFlower(GameObject flower) {
        Debug.Log("In Release");
        var top = flower.transform.Find("Anchors/Top");
        var bottom = flower.transform.Find("Anchors/Bottom");
        //Debug.Log($"{top.transform.position} : {bottom.transform.position}");

        var overlaps = Physics.OverlapCapsule(top.transform.position, bottom.transform.position, 0.01f);
        if(overlaps.ToList().Contains(m_Capsule)) {
            Debug.Log("Found capsule");
            m_CollidingFlower = flower;

        } else {
            var rb = flower.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            flower.transform.SetParent(null);
        }
    }
}
