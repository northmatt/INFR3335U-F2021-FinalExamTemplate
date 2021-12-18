using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour {
    public GameObject mobileHUD;
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public GameObject menuHUD;
    public GameObject roomHUD;
    public bool inGame = false;
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
        SetupHUD();
    }

    // Update is called once per frame
    void Update() {
        if (CursorLocked && !mobileMode && inGame && Input.GetKeyDown(KeyCode.Escape)) {
            CursorHidden(false);
        }

        if (!CursorLocked && !mobileMode && inGame && Input.GetMouseButton(0)) {
            CursorHidden(true);
        }

        if (mobileMode && Input.GetKeyDown("k") || !mobileMode && CursorLocked && Input.GetKeyDown("k")) {
            mobileMode = !mobileMode;
            SetupHUD();
        }
    }

    void SetupHUD() {
        CursorHidden(false);

        if (!inGame)
            return;

        mobileHUD.SetActive(mobileMode);
        CursorLocked = mobileMode;
    }

    void CursorHidden(bool isHidden) {
        CursorLocked = isHidden;
        Cursor.lockState = CursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !CursorLocked;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
