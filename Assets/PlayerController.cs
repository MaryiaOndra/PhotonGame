using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;   
    [SerializeField]
    float angleSpeed = 100f;

    void Update()
    {
        float _vertInput = Input.GetAxis("Vertical");
        float _horInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, _horInput * angleSpeed * Time.deltaTime );
        transform.position += transform.forward *_vertInput * speed * Time.deltaTime;
    }
}
