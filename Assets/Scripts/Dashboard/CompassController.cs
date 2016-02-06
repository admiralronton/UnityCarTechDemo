using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{

    [SerializeField]
    private GameObject m_Camera;

    private Image m_Rotator;

    private Quaternion baseRotatorRotation;

    // Use this for initialization
    void Start()
    {
        // Find the needle
        foreach (Image rotator in GetComponentsInChildren<Image>())
        {
            if (rotator.name == "CompassRotator")
            {
                m_Rotator = rotator;
                baseRotatorRotation = rotator.transform.localRotation;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = baseRotatorRotation;

        // Find the camera's world Y rotation angle.  This is the direction we're ultimately facing.
        float angle = m_Camera.transform.rotation.eulerAngles.y;

        // Turn the rotator by that angle.  Because of the signs in Unity, this turns the compass the right way.
        rotation = Quaternion.Euler(0, 0, angle);

        // Assign this to our local transform
        m_Rotator.transform.localRotation = rotation;
    }
}
