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
    public Text m_TextoRecord;

    private int hits = 0;
    private float m_TiempoRestante;
    private int m_ObjetivosVivos = 0;
    private bool m_Gano = false;
    private int m_Record = int.MaxValue;

    public void Start()
    {
        Inicializar();
    }

    public void Update()
    {
        if (!m_Gano)
        {
            if (m_ObjetivosVivos == 0)
            {
                StartCoroutine(Gano());
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
    }

    public void updateHits()
    {
        if (m_TiempoRestante > 0)
        {
            m_ObjetivosVivos--;
            m_TextHits.text = m_ObjetivosVivos.ToString();
        }
    }

    public void Inicializar()
    {
        m_TiempoRestante = m_TiempoLimite;
        m_ObjetivosVivos = transform.childCount;

        m_TextTiempo.text = m_TiempoLimite.ToString();
        m_TextHits.text = m_ObjetivosVivos.ToString();
        m_TextoCentral.text = "";
        m_TextoRecord.text = (m_Record != int.MaxValue) ? m_Record.ToString() : "";

        m_Gano = false;
    }

    private IEnumerator Gano()
    {
        m_TextoCentral.text = "Has ganado";
        m_Gano = true;
        if(m_TiempoLimite - (int) m_TiempoRestante < m_Record)
            m_Record = m_TiempoLimite - (int) m_TiempoRestante;

        yield return new WaitForSeconds(3);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        Inicializar();

    }

}
