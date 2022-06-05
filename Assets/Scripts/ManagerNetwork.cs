using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNetwork : MonoBehaviourPunCallbacks
{
  public static ManagerNetwork Instance {get; private set;}

  private void Awake() {
    if(Instance != null && Instance !=this){
      gameObject.SetActive(false);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);

  }

  private void Start() {
    PhotonNetwork.ConnectUsingSettings();
  }

  public override void OnConnectedToMaster() {
    print("Connected Successfully");
  }


  public void createRoom(string nameRoom){
    PhotonNetwork.CreateRoom(nameRoom);
  }

   public void joinRoom(string nameRoom){
    PhotonNetwork.JoinRoom(nameRoom);
  }

  public void changeNickname(string namePlayer){
    PhotonNetwork.NickName = namePlayer;
  }

  internal void exitLobby()
  {
    PhotonNetwork.LeaveRoom();
  }

  [PunRPC]
  internal void startGame(string sceneName)
  {
    PhotonNetwork.LoadLevel(sceneName);
  }

  public string getPlayersList()
  {
    var list = "";
    foreach(var player in PhotonNetwork.PlayerList)
    {
      list +=  player.NickName+"\n";
    }

    return list;
  }


  public bool isOwnerRoom()
  {
    return PhotonNetwork.IsMasterClient;

  }

}


