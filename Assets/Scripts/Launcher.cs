using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks {
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public string gameSceneName = "";
    public string loadSceneName = "";

    public InputField createInput;
    public InputField joinInput;
    public Text errorText;

    public Text roomName;
    public Text playerCount;
    public GameObject playerListingPrefab;
    public Transform playerListContent;
    public Button startButton;
    public Button settingsButton;
    public GameObject readyButtonPrefab;
    public Transform readyButtonOrganizer;

    void Start() {
        errorText.text = "";
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
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
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        roomName.text = roomName.text + PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;
        playerCount.text = playerCount.text + players.Length;
        for (uint i = 0; i < players.Length; ++i) {
            Instantiate(playerListingPrefab, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(players[i]);

            startButton.interactable = (i == 0);
            settingsButton.interactable = (i == 0);
        }

        Instantiate(readyButtonPrefab, readyButtonOrganizer).GetComponent<ReadyButton>().SetCurrentPlayer(playerListContent.gameObject);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        //base.OnCreateRoomFailed(returnCode, message);
        errorText.text = "Error!";
    }

    public void OnClickStartGame() {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(gameSceneName);
    }

    public void OnClickLeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() {
        SceneManager.LoadScene(loadSceneName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        Instantiate(playerListingPrefab, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(newPlayer);
    }

    public void SwitchScene(string sceneName) {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(sceneName);
    }
}
