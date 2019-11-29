using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivosManager : MonoBehaviour
{

    public GameObject m_ObjetivoPrefab;
    public Transform[] m_SpawnPoints;

    private List<GameObject> m_Objetivos;

    // Start is called before the first frame update
    void Start()
    {
        m_Objetivos = new List<GameObject>();
        foreach (Transform trans in m_SpawnPoints)
        {
            Instantiate(m_ObjetivoPrefab, trans.position, trans.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
