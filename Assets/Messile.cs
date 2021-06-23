using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messile : MonoBehaviour, IPunObservable
{
    const string RPC_METHOD_NAME = "DestroyMessile";

    [SerializeField]
    float speed = 10f;

    PhotonView messilePhotonView;
    Rigidbody messileRb;

    public PhotonView PhotonView => messilePhotonView;
    public string RPCmethodName => RPC_METHOD_NAME;

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
        messilePhotonView = GetComponent<PhotonView>();
        messileRb = GetComponent<Rigidbody>();

        messileRb.isKinematic = !messilePhotonView.IsMine;
    }

    private void Start()
    {
        if (messilePhotonView.IsMine)
        {
            messileRb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (messilePhotonView.IsMine)
        {
            PhotonNetwork.Destroy(messilePhotonView);

            //var _player = _collision.gameObject.GetComponent<PlayerController>();
            //if (_player)
            //{
            //    var _photonView = _player.gameObject.GetComponent<PhotonView>();
            //    _photonView.RPC("MessileHit", RpcTarget.All);
            //    PhotonNetwork.Destroy(messilePhotonView);
            //}
        }
    }
}
