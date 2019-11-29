using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHit : MonoBehaviour
{
    // Metodo que se llama cuando es golpeado el cubo
    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
