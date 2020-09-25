using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject IngredientToSpawn;
    public GameObject LeftHandController;
    public GameObject RightHandController;
    public void SpawnIngredient()
    {
        var copy = Instantiate(IngredientToSpawn, IngredientToSpawn.transform.parent, true);
        copy.transform.position = RightHandController.transform.position;
        copy.transform.rotation = Quaternion.identity;

        copy.GetComponent<Rigidbody>().useGravity = false;
        
        //RestoreGravity(copy);
    }

    IEnumerator RestoreGravity(GameObject copy)
    {
        yield return new WaitForSeconds(3);
        copy.GetComponent<Rigidbody>().useGravity = true;
        yield return null;
    }

}
