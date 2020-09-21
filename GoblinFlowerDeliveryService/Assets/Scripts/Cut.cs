using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Cut : MonoBehaviour
{
    public GameObject Scissors;
    private BoxCollider m_Scissors_Collider;
    private Animator m_Animator;
    private AudioSource m_Audio;

    private List<UnityEngine.XR.InputDevice> rightHandDevices;

    // Start is called before the first frame update
    void Start()
    {
        rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        m_Animator = Scissors.GetComponent<Animator>();
        m_Audio = GetComponent<AudioSource>();
        m_Scissors_Collider = Scissors.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rightHandDevices.Count > 0) {
            var contr = rightHandDevices[0];
            bool triggerValue;
            bool isIdle = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
            if(isIdle && contr.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue) {
                m_Animator.SetTrigger("BeginCut");
                m_Audio.Play();
                m_Scissors_Collider.enabled = true;
            } else {
                m_Animator.ResetTrigger("BeginCut");
                m_Scissors_Collider.enabled = false;
            }

        }
    }
}
