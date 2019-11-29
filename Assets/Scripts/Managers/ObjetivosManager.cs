using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetivosManager : MonoBehaviour
{

    public int m_TiempoLimite = 15;
    public Text m_TextTiempo;
    public Text m_TextHits;
    public Text m_TextoCentral;

    private int hits = 0;
    private float m_TiempoRestante;
    private int m_ObjetivosVivos = 0;

    public void Start()
    {
        m_TiempoRestante = m_TiempoLimite;
        m_ObjetivosVivos = transform.childCount;
        m_TextTiempo.text = m_ObjetivosVivos.ToString();
    }

    public void Update()
    {
        if (m_ObjetivosVivos == 0)
        {
            m_TextoCentral.text = "Has ganado";
        }
        else
        {
            m_TiempoRestante -= Time.deltaTime;
            if (m_TiempoRestante < 0)
            {
                m_TextoCentral.text = "Tiempo agotado";
                m_TextTiempo.text = "0";
            }
            else
            {
                m_TextTiempo.text = ((int)m_TiempoRestante).ToString();
            }
        }
    }

    public void updateHits()
    {
        if (m_TiempoRestante > 0)
        {
            m_ObjetivosVivos--;
            m_TextHits.text = m_ObjetivosVivos.ToString();
        }
    }


}
