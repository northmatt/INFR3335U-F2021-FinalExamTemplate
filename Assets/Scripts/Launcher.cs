using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks {
    public InputField createInput;
    public InputField joinInput;
    public Text errorText;
    public string roomSceneName = "";

    void Start() {
        errorText.text = "";
    }

    public void CreateRoom() {
        if (string.IsNullOrEmpty(createInput.text))
            return;
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() {
        //SceneManager.LoadScene(roomSceneName);
        PhotonNetwork.LoadLevel(roomSceneName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        //base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Error creating room");
        errorText.text = "Error!";
    }
}
