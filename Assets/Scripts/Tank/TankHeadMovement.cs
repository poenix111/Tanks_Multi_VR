using UnityEngine;

public class TankHeadMovement : MonoBehaviour
{
    public GameObject m_TankHead;
    public float m_Sensitivity = 150f;
    public float m_MaxXRotate = 45f;
    public float m_MaxYRotate = 45f;
    public string m_AxisXName = "AxisX";
    public string m_AxisYName = "AxisY";

    public int m_PlayerNumber = 1; // Es cambiado en el tankManager

    private float m_Xvalue;
    private float m_Yvalue;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start ()
    {
        // The fire axis is based on the player number.
        m_AxisXName = m_AxisXName + m_PlayerNumber;
        m_AxisYName = m_AxisYName + m_PlayerNumber;
    }

    private void Update()
    {
        m_Xvalue = Input.GetAxis(m_AxisXName);
        m_Yvalue = Input.GetAxis(m_AxisYName);
    }

    private void FixedUpdate()
    {
        yRotation += m_Xvalue * m_Sensitivity * Time.deltaTime;
        xRotation -= m_Yvalue * m_Sensitivity * Time.deltaTime;

        // xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -m_MaxXRotate, m_MaxXRotate);
        yRotation = Mathf.Clamp(yRotation, -m_MaxYRotate, m_MaxYRotate);

        m_TankHead.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

}