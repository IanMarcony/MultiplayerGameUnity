using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MenuLobby : MonoBehaviourPunCallbacks
{
  [SerializeField] private TMP_Text playerList;
  [SerializeField] private Button btnStartGame;


  [PunRPC]
  public void UpdateListPlayer()
  {
    playerList.text = ManagerNetwork.Instance.getPlayersList();

    btnStartGame.interactable = ManagerNetwork.Instance.isOwnerRoom();
  }

}
