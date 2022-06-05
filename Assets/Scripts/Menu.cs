using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
   [SerializeField] private MenuEnter menuEnter;
   [SerializeField] private MenuLobby menuLobby;

  private void Start() {
      menuEnter.gameObject.SetActive(false);
      menuLobby.gameObject.SetActive(false);
  }

  public override void OnConnectedToMaster() {
      menuEnter.gameObject.SetActive(true);
  }

  public override void OnJoinedRoom()
  {
    changeMenu(menuLobby.gameObject);
    menuLobby.photonView.RPC("UpdateListPlayer",RpcTarget.All);
  }
  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
    menuLobby.UpdateListPlayer();
  }

  public void exitLobby()
  {
    ManagerNetwork.Instance.exitLobby();
    changeMenu(menuEnter.gameObject);
  }

  public void startGame(string sceneName)
  {
    ManagerNetwork.Instance.photonView.RPC("startGame", RpcTarget.All, sceneName);

  }


  public void changeMenu(GameObject menu)
  {
    menuEnter.gameObject.SetActive(false);
    menuLobby.gameObject.SetActive(false);

    menu.SetActive(true);



  }
}
