using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GOTGameManager : MonoBehaviour
{
    public static GOTGameManager Instance { get; private set; }

    public GameObject ClearUI;
    public GameObject Player;
    
    void Start()
    {
        Instance = this;


        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;

        ClearUI.SetActive(false);
    }


    public void OpenClearUI(bool isClear)
    {
        PlayerInput _player = Player.GetComponent<PlayerInput>();
        _player.DeactivateInput();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        string txt = isClear ? "CLEAR" : "FAIL";
        ClearUI.GetComponentInChildren<TextMeshProUGUI>().text = txt;

        ClearUI.SetActive(true);
    }
}
