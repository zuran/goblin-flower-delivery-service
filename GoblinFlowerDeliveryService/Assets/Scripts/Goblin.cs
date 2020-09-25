using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Goblin : MonoBehaviour
{
    public Transform InsideLocation;
    public Transform OutsideLocation;
    public Transform RightHandPosition;
    public float WaitOutsideTime = 10f;

    public GameObject SurprisedGoblin;

    public XRController LeftController;
    public XRController RightController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool _canAnswerDoor = true;
    public void AnswerDoor()
    {
        if (_canAnswerDoor)
        {
            transform.position = OutsideLocation.position;
            transform.rotation = OutsideLocation.rotation;
            _canAnswerDoor = false;
            StartCoroutine(WaitToAnswerDoor());
        }
    }

    public void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;
        if (obj.transform.parent != null) obj = obj.transform.parent.gameObject;

        if(obj.tag == "Bucket")
        {
            transform.position = InsideLocation.position;
            transform.rotation = InsideLocation.rotation;
            GetComponent<SphereCollider>().enabled = false;
            SurprisedGoblin.transform.position = OutsideLocation.position;
            SurprisedGoblin.transform.rotation = OutsideLocation.rotation;
            Destroy(obj.GetComponent<XRGrabInteractable>());
            obj.transform.position = RightHandPosition.position;
            obj.transform.rotation = RightHandPosition.rotation;
            obj.transform.parent = RightHandPosition;
            StartCoroutine(ReceivedFlowers());
        }
    }

    IEnumerator WaitToAnswerDoor()
    {
        yield return new WaitForSeconds(WaitOutsideTime);
        _canAnswerDoor = true;
        transform.position = InsideLocation.position;
        transform.rotation = InsideLocation.rotation;
        yield return null;
    }

    IEnumerator ReceivedFlowers()
    {
        yield return new WaitForSeconds(15);
        SurprisedGoblin.transform.position = InsideLocation.position;
        SurprisedGoblin.transform.rotation = InsideLocation.rotation;
    }
}
