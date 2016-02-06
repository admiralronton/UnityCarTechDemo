using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;

public class SpeedometerController : MonoBehaviour
{

    [SerializeField] private CarController m_Car;

    private Image m_NeedleSprite;

    private const float speedoMin = 132f;
    private const float speedoMax = -132f;

	// Use this for initialization
	void Start ()
    {
        // Find the needle
        foreach(Image needle in GetComponentsInChildren<Image>())
        {
            if (needle.name == "SpeedoNeedle")
            {
                m_NeedleSprite = needle;
                break;
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // NOTE: This implementation sees reverse speed the same as forward speed.

        float currentSpeed = m_Car.CurrentSpeed;
        float maxSpeed = m_Car.MaxSpeed;
        
        // Make sure we stay in bounds
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        // Calculate the desired angle
        float targetAngle = ((speedoMax - speedoMin) * (currentSpeed / maxSpeed)) + speedoMin;

        // Set the needle rotation based on the current speed of the car
        Quaternion target = Quaternion.Euler(0, 0, targetAngle);
        m_NeedleSprite.transform.rotation = target;
	}
}
