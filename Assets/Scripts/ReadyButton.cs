using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ReadyButton : MonoBehaviourPunCallbacks {
    public GameObject currentPlayer;
    public bool readyFlag = false;

    private GameObject unready, ready;

    public void SetCurrentPlayer(GameObject playerListContent) {
        Text[] tempPlayerInfoList = playerListContent.GetComponentsInChildren<Text>();
        foreach (Text temp in tempPlayerInfoList) {
            if (temp.text == PhotonNetwork.NickName)
                currentPlayer = temp.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        }
    }

    public void OnClickReady() {
        unready = currentPlayer.transform.GetChild(2).gameObject;
        ready = currentPlayer.transform.GetChild(3).gameObject;

        readyFlag = !readyFlag;

        unready.SetActive(!readyFlag);
        ready.SetActive(readyFlag);
    }
}