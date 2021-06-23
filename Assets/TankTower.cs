using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTower : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 50f;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.z = mousePos.z - objectPos.z;

        float angle = Mathf.Atan2(mousePos.z, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
