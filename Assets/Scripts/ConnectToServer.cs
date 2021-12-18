using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks {
    public string lobbySceneName = "";

    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.NickName = "Player " + Random.Range(0, 100).ToString("000");
        SceneManager.LoadScene(lobbySceneName);
    }
}