using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandLocalAnimator : MonoBehaviour
{

    public InputDeviceCharacteristics controllerCharacteristics;
    public Animator handAnimator;
    //public VRGrabber grabber;
    InputDevice targetDevice;

    const float MIN_BUTTON_INPUT = 0.01f;
    const float INPUT_RATE_CHANGE = 20.0f;
    bool canPoint, canThumbsUp;
    float pointBlend, thumbsUpBlend;
    bool canPinch = false;

    /* void OnTriggerEnter(Collider other)
    {
        if (grabber.isGrabbing) return;
        if (other.CompareTag("GamePiece") || other.CompareTag("Card"))
        {
            canPinch = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (grabber.isGrabbing) return;
        if (other.CompareTag("GamePiece") || other.CompareTag("Card"))
        {
            canPinch = false;
        }
    } */
    void Start()
    {
        TryGetTargetDevice();
    }

    void TryGetTargetDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
    public void Update()
    {
        if (targetDevice != null && handAnimator != null)
        {
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripAmount);
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerAmount);


            bool usePinch = canPinch;
            if (usePinch /*&&  grabber.isGrabbing */)
            {
                triggerAmount = gripAmount;
                gripAmount = 0;
            }
            else
            {
                if (gripAmount <= 0.1f)
                {

                    triggerAmount = triggerAmount <= 0.01f ? 0.02f : triggerAmount;
                }

            }
            UpdateHandAnimation(gripAmount, triggerAmount, true /* , grabber.isGrabbing */);
        }
    }

    public void UpdateHandAnimation(float flex, float pinch, bool isThumbRested, bool isHolding = false)
    {

        handAnimator.SetFloat("Flex", flex);
        handAnimator.SetFloat("Pinch", pinch);

        canPoint = pinch <= MIN_BUTTON_INPUT && !isHolding;
        canThumbsUp = !isThumbRested && !isHolding;

        pointBlend = InputValueRateChange(canPoint, pointBlend);
        float point = canPoint ? pointBlend : 0.0f;
        int pointLayerIndex = handAnimator.GetLayerIndex("Point Layer");
        handAnimator.SetLayerWeight(pointLayerIndex, point);


        thumbsUpBlend = InputValueRateChange(canThumbsUp, thumbsUpBlend);
        float thumbsUp = canThumbsUp ? thumbsUpBlend : 0.0f;
        int thumbsUpLayerIndex = handAnimator.GetLayerIndex("Thumb Layer");
        handAnimator.SetLayerWeight(thumbsUpLayerIndex, thumbsUp);
    }
    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }
}
