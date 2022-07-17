using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBullet : MonoBehaviour
{
    //References
    [SerializeField] Rigidbody rb;
    private Transform _cam;
    public Transform cam
    {
        get { return _cam; }
        set
        {
            if (value.GetComponent<Camera>())
            {
                _cam = value;
            }
            else
            {
                Debug.LogError("Please assign Camera");
            }
        }
    }

    //Variables
    [SerializeField] float forceMultiplier;

    private void Start()
    {
        rb.AddForce(_cam.forward * forceMultiplier, ForceMode.Impulse);
        Destroy(gameObject, 2);
    }

}
