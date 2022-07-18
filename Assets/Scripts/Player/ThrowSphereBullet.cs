using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ThrowSphereBullet : MonoBehaviour
{
    //References
    [SerializeField] GameObject handPos;
    [SerializeField] GameObject spheresPrefab;
    [SerializeField] GameObject cam;

    //Variables
    [SerializeField] float cooldown;
    float time;
    bool canThrow = true;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canThrow)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            ThrowSphere();
            canThrow = false;
        }
        SetCooldown();
    }

    void ThrowSphere()
    {
        GameObject bulletSphere = (GameObject)Instantiate(spheresPrefab, handPos.transform.position, Quaternion.identity);
        bulletSphere.GetComponent<SphereBullet>().cam = cam.transform;
    }
    void SetCooldown()
    {
        if (!canThrow)
        {
            time += Time.deltaTime;
            if (time > cooldown)
            {
                canThrow = true;
                time = 0;
            }
        }
    }
}
