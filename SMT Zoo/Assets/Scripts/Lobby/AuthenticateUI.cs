using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuthenticateUI : MonoBehaviour {


    [SerializeField] private Button authenticateButton;
    [SerializeField] private Transform lobbyUI;


    private void Awake() {
        lobbyUI.gameObject.SetActive(false);

        authenticateButton.onClick.AddListener(() => {
            LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
            Hide();
            lobbyUI.gameObject.SetActive(true);
        });
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}