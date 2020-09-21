using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutable : MonoBehaviour
{
    public GameObject Puff;

    private ParticleSystem m_particles;

    // Start is called before the first frame update
    void Start()
    {
        m_particles = Puff.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        Puff.transform.position = other.gameObject.transform.position;
        m_particles.Play();


        var meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach(var mesh in meshes) {
            mesh.enabled = false;
        }
        Debug.Log("mesh hit");
    }


}
