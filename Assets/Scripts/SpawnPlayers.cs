using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;

    public Vector2 minRange;
    public Vector2 maxRange;

    // Start is called before the first frame update
    void Start() {
        Vector3 random = transform.position
            + Vector3.right * Random.Range(minRange.x, maxRange.x)
            + Vector3.forward * Random.Range(minRange.y, maxRange.y);
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, random, Quaternion.identity);
        if (player.GetComponent<PhotonView>().IsMine) {
            GameObject camera = Instantiate(cameraPrefab, random, Quaternion.identity);
            camera.GetComponent<CameraController>().following = player.transform;
            player.GetComponent<CharController>().hudManager = camera.GetComponentInChildren<HUDManager>();
        }
    }
}
