using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuEnter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text namePlayer;

    [SerializeField]
    private TMP_Text  nameRoom;


  public void createRoom(){
    ManagerNetwork.Instance.changeNickname(namePlayer.text);
    ManagerNetwork.Instance.createRoom(nameRoom.text);
  }

   public void joinRoom(){
    ManagerNetwork.Instance.changeNickname(namePlayer.text);
    ManagerNetwork.Instance.joinRoom(nameRoom.text);
  }
}
