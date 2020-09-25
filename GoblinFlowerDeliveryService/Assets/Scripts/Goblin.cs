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
    public string Name = "";

    public GameObject SurprisedGoblin;
    public GameObject ShrugGoblin;
    public GameObject ShakeGoblin;

    public GameManager GM;

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

        if (obj.tag == "Bucket" && obj.GetComponent<Bucket>().Delivered == false)
        {
            GM.ScoreArrangement();
            var goblin = gameObject;
            if(GM.GoblinName != Name)
            {
                goblin = ShakeGoblin;
            }
            else
            {
                Destroy(obj.GetComponent<XRGrabInteractable>());
                obj.transform.position = RightHandPosition.position;
                obj.transform.rotation = RightHandPosition.rotation;
                obj.transform.parent = RightHandPosition;
                obj.GetComponent<Bucket>().Delivered = true;
            }

            if (GM.Score < 2)
            goblin = ShrugGoblin;
            else goblin = SurprisedGoblin;

            transform.position = InsideLocation.position;
            transform.rotation = InsideLocation.rotation;
            GetComponent<SphereCollider>().enabled = false;
            goblin.transform.position = OutsideLocation.position;
            goblin.transform.rotation = OutsideLocation.rotation;
            StartCoroutine(ReceivedFlowers(goblin));
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

    IEnumerator ReceivedFlowers(GameObject goblin)
    {
        yield return new WaitForSeconds(15);
        goblin.transform.position = InsideLocation.position;
        goblin.transform.rotation = InsideLocation.rotation;
        GetComponent<SphereCollider>().enabled = true;
    }
}
