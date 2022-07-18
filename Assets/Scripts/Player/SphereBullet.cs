using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBullet : MonoBehaviour
{
    //References
    [SerializeField] Rigidbody rb;
    [SerializeField] Renderer rend;
    [SerializeField] float emissionIntensity;
    [SerializeField] ParticleSystem explosionParticle;
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
    [SerializeField] int damageAmount;

    private void Start()
    {
        RandomEmissionColor();
        rb.AddForce(_cam.forward * forceMultiplier, ForceMode.Impulse);
        Destroy(gameObject, 2);
    }

    //Abstracion
    void RandomEmissionColor()
    {
        rend = GetComponent<Renderer>();
        Color colorRandom = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        rend.material.SetColor("_EmissionColor", colorRandom * emissionIntensity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageAmount = Random.Range(30, 80);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound("DealDamage");
            Destroy(gameObject);
        }
    }
}
