using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnterCart : MonoBehaviour
{
    public GameObject Rig;
    public GameObject Cart;

    private bool _isPlayerInside = false;

    public void GetInOrOutOfCart()
    {
        if(_isPlayerInside)
        {
            Cart.GetComponent<Follower>().Moving = false;
            Rig.GetComponent<Follower>().Moving = false;
            Cart.GetComponent<Follower>().IsPlayerInCart = false;
            Rig.GetComponent<Follower>().IsPlayerInCart = false;
            Rig.transform.position = transform.GetChild(0).transform.position;
            Rig.transform.rotation = Quaternion.Euler(0, Cart.transform.rotation.y, 0);
            Rig.GetComponent<ThumbstickLocomotion>().enabled = true;
            Rig.GetComponent<SnapTurnProvider>().enabled = true;
            Rig.GetComponent<CharacterController>().enabled = true;
        } else
        {
            Cart.GetComponent<Follower>().IsPlayerInCart = true;
            Rig.GetComponent<Follower>().IsPlayerInCart = true;
            Rig.transform.position = Cart.transform.position;
            Rig.transform.rotation = Cart.transform.rotation;
            Rig.GetComponent<ThumbstickLocomotion>().enabled = false;
            Rig.GetComponent<SnapTurnProvider>().enabled = false;
            Rig.GetComponent<CharacterController>().enabled = false;
        }

        _isPlayerInside = !_isPlayerInside;
    }
}
