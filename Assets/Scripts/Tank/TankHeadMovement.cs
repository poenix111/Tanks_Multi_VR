using UnityEngine;

public class TankHeadMovement : MonoBehaviour
{
    public GameObject m_TankHead;
    public float m_Sensitivity = 50f;
    public float m_MaxXRotate = 45f;
    public float m_MaxYRotate = 45f;

    private float m_Xvalue;
    private float m_Yvalue;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Update()
    {
        m_Xvalue = Input.GetAxis("Mouse X");
        m_Yvalue = Input.GetAxis("Mouse Y");
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