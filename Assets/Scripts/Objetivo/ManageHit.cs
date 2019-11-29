using UnityEngine;

public class ManageHit : MonoBehaviour
{

    // Metodo que se llama cuando es golpeado el cubo
    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        GetComponentInParent<ObjetivosManager>().updateHits();
    }
}
