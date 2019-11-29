using UnityEngine;
using System.Collections;
public class ManageHit : MonoBehaviour
{
    private float timer = 0f;
    public int m_SpinValue = 90;
    private Color m_Colorcito;
    // Metodo que se llama cuando es golpeado el cubo
    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        GetComponentInParent<ObjetivosManager>().updateHits();
    }
    public void Start()
    {

    }
    public void Update()
    {   
        transform.Rotate(Vector3.up * m_SpinValue * Time.deltaTime);
        timer += Time.deltaTime;
        
        if(timer > 2.0f) {
            changeColor();
            timer = 0f;
        }
    }

    void changeColor()
    {
        m_Colorcito = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        GetComponent<MeshRenderer>().material.color = m_Colorcito;
    }
}

