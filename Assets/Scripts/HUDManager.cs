using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class HUDManager : MonoBehaviour {
    public GameObject mobileHUD;
    public GameObject pauseHUD;
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public bool inGame = false;
    public bool paused = false;
    public bool CursorLocked = false;
    public bool mobileMode = false;

    // Start is called before the first frame update
    void Start() {
        switch (Application.platform) {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                mobileMode = true;
                break;
            default:
                mobileMode = false;
                break;
        }
        CursorHidden(inGame);

        if (inGame) {
            DoPause(false);
        }
    }

    // Update is called once per frame
    void Update() {
        /*if (CursorLocked && !mobileMode && inGame && Input.GetKeyDown(KeyCode.Escape)) {
            CursorHidden(false);
        }

        if (!CursorLocked && !mobileMode && inGame && Input.GetMouseButton(0)) {
            CursorHidden(true);
        }
        
        if (mobileMode && Input.GetKeyDown("k") || !mobileMode && CursorLocked && Input.GetKeyDown("k")) {
            mobileMode = !mobileMode;
            SetupHUD();
        }*/

        if (inGame && Input.GetKeyDown(KeyCode.Escape)) {
            DoPause(!paused);
        }
    }

    void CursorHidden(bool isHidden) {
        CursorLocked = isHidden;
        Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !CursorLocked;
    }

    public void DoPause(bool isPaused) {
        paused = isPaused;
        pauseHUD.SetActive(paused);
        mobileHUD.SetActive(paused ? false : mobileMode);
        CursorHidden(paused ? false : !mobileMode);
    }

    public void DoMobile() {
        mobileMode = !mobileMode;
    }

    public void DoFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void LoadScene(string sceneName) {
        if (inGame) {
            StartCoroutine(DoSwitchScene(sceneName));
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator DoSwitchScene(string sceneNumber) {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;

        SceneManager.LoadScene(sceneNumber);
    }

    public void ExitApplication() {
        Application.Quit();
    }
}
