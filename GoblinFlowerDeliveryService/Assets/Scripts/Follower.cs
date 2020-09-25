using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    public bool Moving = false;
    public GameObject StartStopButton;
    public bool IsPlayerInCart = false;

    private float distanceTravelled = 90.89914f;
    private Vector3 _offset = new Vector3(-.501f, 0.086f, -0.19f);


    public void ToggleStartStop()
    {
        if(IsPlayerInCart)
            Moving = !Moving;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Player") return;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Moving) return;
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        //if (gameObject.tag == "Player") transform.position = transform.position - _offset;
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
