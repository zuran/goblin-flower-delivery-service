using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ThumbstickLocomotion : MonoBehaviour
{
    public float Speed = 1;
    public CharacterController ControllerComponent;
    public XRController LeftController;

    private Vector2 _direction2D;
    private Vector3 _direction3D;
    // Start is called before the first frame update
    void Start()
    {
        ControllerComponent = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _direction3D = ControllerComponent.GetComponentInChildren<Camera>()
            .transform.TransformDirection(new Vector3(_direction2D.x, 0, _direction2D.y));
    }

    void FixedUpdate()
    {
        var device = LeftController.inputDevice;
        if(device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _direction2D))
        {
            if(_direction2D.magnitude > 0.2)
            {
                var direction = Vector3.ProjectOnPlane(_direction3D, Vector3.up) + 
                    new Vector3(0, -9.8f, 0) * Time.deltaTime;
                ControllerComponent.Move(Speed * Time.deltaTime * direction);
            }
        }
    }
}
