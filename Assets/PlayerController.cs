using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    const string MESSILE_YEL_PREPHAB_NAME = "Messile_Yellow";
    const string MESSILE_BLUE_PREPHAB_NAME = "Messile_Blue";

    [SerializeField]
    Transform messilePointTr;

    [SerializeField]
    float speed = 10f;   
    [SerializeField]
    float angleSpeed = 100f;

    PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            var _pos = transform.position;
            var _rot = transform.rotation;

            stream.Serialize(ref _pos);
            stream.Serialize(ref _rot);
        }
        else
        {
            var _pos = Vector3.zero;
            var _rot = Quaternion.identity;

            stream.Serialize(ref _pos);
            stream.Serialize(ref _rot);

            transform.position = _pos;
            transform.rotation = _rot;
        }
    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float _vertInput = Input.GetAxis("Vertical");
            float _horInput = Input.GetAxis("Horizontal");

            transform.Rotate(Vector3.up, _horInput * angleSpeed * Time.deltaTime);
            transform.position += transform.forward * _vertInput * speed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                string _messilePrephabName = PhotonNetwork.LocalPlayer.ActorNumber == 1 ?
                    MESSILE_YEL_PREPHAB_NAME :
                    MESSILE_BLUE_PREPHAB_NAME;

                PhotonNetwork.Instantiate(_messilePrephabName, messilePointTr.position, messilePointTr.rotation);
            }
        }
    }


    [PunRPC]
    public void MessileHit() 
    {
        if (photonView.IsMine)
        {
            Debug.Log("HIT THE TANK");
        }    
    }
}
