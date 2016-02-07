using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        private void Update()
        {
            // NOTE: We have to handle key inputs in the Update loop.  Inputs are only registered the 
            // frame after the event, and FixedUpdate() doesn't always run every frame.

            // Neutral input
            // This will ONLY fire the frame after the key is pressed
            if (Input.GetKeyDown(KeyCode.G))
            {
                // Toggle neutral gear
                m_Car.Neutral = !m_Car.Neutral;
            }

            // Honk
            if (Input.GetKeyDown(KeyCode.H))
            {
                m_Car.Honk = true;
            }
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");

            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

    }
}
