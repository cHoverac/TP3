using UnityEngine;
using UnityEngine.XR;

public class ShootManager : MonoBehaviour
{

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform controller;
    [SerializeField, Range(0f, 180f)] private float angleTolerance = 20f;
    [SerializeField] private AudioClip sound;

    private GameObject[] targets;
    private InputDevice _rightController;
    private bool previousPressed = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");

        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rightController.isValid)
        {
            _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if (!_rightController.isValid) return;
        }

        if (_rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            if (triggerPressed && !previousPressed)
            {
                foreach (GameObject target in targets)
                {
                    Vector3 directionToTarget = (target.transform.position - controller.position).normalized;
                    float angle = Vector3.Angle(controller.forward, directionToTarget);

                    if (angle <= angleTolerance)
                    {
                        
                        GameObject particles = Instantiate(projectilePrefab, target.transform.position, Quaternion.identity);
                        AudioSource audio = target.GetComponent<AudioSource>();
                        if (audio != null) audio.Play();

                        


                        _rightController.SendHapticImpulse(0u, 0.5f, 0.1f);
                        break;
                    }
                }
            }

            previousPressed = triggerPressed; 
        }
    }
}

