using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Arcaedion.Multiplayer;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get; private set; }

  [SerializeField] private string pathPlayerPrefab;
  [SerializeField] private Transform[] spawns;

  private List<ControleJogadorNet> players;
  public List<ControleJogadorNet> Players { get => players; private set => players = value; }
    
  private int playerCount = 0;


  private void Awake()
  {
    if (Instance != null && Instance !=this)
    {
      gameObject.SetActive(false);

      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    photonView.RPC("addPlayer", RpcTarget.AllBuffered);
    players = new List<ControleJogadorNet>();
  }


  [PunRPC]
  private void addPlayer()
  {
    playerCount++;

    if (playerCount == PhotonNetwork.PlayerList.Length)
    {
      createPlayer();
    }
  }

  private void createPlayer()
  {
    var playerObject = PhotonNetwork.Instantiate(pathPlayerPrefab,
      spawns[Random.Range(0, spawns.Length)].position,
      Quaternion.identity);

    var player = playerObject.GetComponent<ControleJogadorNet>();


    player.photonView.RPC("initializePlayer", RpcTarget.All, PhotonNetwork.LocalPlayer);
  
  }


}
