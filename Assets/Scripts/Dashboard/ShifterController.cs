using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;

public class ShifterController : MonoBehaviour
{

    [SerializeField]
    private CarController m_Car;

    private Image m_Dot;

    private const float parkY = 20f;
    private const float driveY = -20f;
    private Vector3 baseDotPosition;

    // Use this for initialization
    void Start()
    {
        // Find the needle
        foreach (Image dot in GetComponentsInChildren<Image>())
        {
            if (dot.name == "ShiftDot")
            {
                m_Dot = dot;
                baseDotPosition = dot.transform.localPosition;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // NOTE: This implementation sees reverse speed the same as forward speed.
        Vector3 position = baseDotPosition;
        if (m_Car.Neutral)
        {
            position.y += parkY;
        }
        else
        {
            position.y += driveY;
        }
        m_Dot.transform.localPosition = position;
    }
}
