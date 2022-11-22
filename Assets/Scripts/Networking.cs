using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Networking : MonoBehaviourPunCallbacks
{
    [Header("Networking")]
    public GameObject player;

    void Start()
    {
        Debug.Log("Starting!");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("Room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(-8.74f, 0, 15.3f);
        PhotonNetwork.Instantiate(player.name, pos, Quaternion.identity);

        Debug.Log("Player instantiated!");
    }
}
