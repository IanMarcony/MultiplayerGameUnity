using System.Collections;
using System.Collections.Generic;
using Arcaedion.Multiplayer;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ControleJogadorNet : MonoBehaviourPunCallbacks
{
  [SerializeField] private float _velocidadeDeMovimento = 5;
  [SerializeField] private float _forcaDoPulo = 300;
  [SerializeField] private Rigidbody _rb;
  [SerializeField] private TMP_Text playerName;


  private ControleJogador _jogador = null;
  private Camera _camera;

  private Player photonPlayer;
  private int idPhoton;

  public Rigidbody Rb { get => _rb; set => _rb = value; }


  [PunRPC]
  public void initializePlayer(Player player)
  {
    photonPlayer = player;
    idPhoton = player.ActorNumber;
    playerName.text = player.NickName;
    GameManager.Instance.Players.Add(this);
  


    if (!photonView.IsMine)
    {
      Rb.isKinematic = true;
    }
  }

  private void Start()
  {
    _rb = GetComponent<Rigidbody>();
    _camera = Camera.main;
  }

  private void Update()
  {
    Move();
    Pula();

    if (Input.GetButtonDown("Fire1"))
    {
      
      Ray cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
      Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
      float rayLength;

      if (groundPlane.Raycast(cameraRay, out rayLength))
      {
        Vector3 pointToLook = cameraRay.GetPoint(rayLength);
        Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z ));
        _jogador.Rb.AddForce(new Vector3(pointToLook.x * 100, transform.position.y, pointToLook.z * 100));
      }
    }
  }

  private void Pula()
  {
    if (Input.GetButtonDown("Jump"))
    {
      var ray = new Ray(transform.position, Vector3.down);

      if (Physics.Raycast(ray, 1f))
      {
        Rb.AddForce(Vector3.up * _forcaDoPulo , ForceMode.Impulse);

      }
    }
  }

  private void Move()
  {
    var x = Input.GetAxis("Horizontal") * _velocidadeDeMovimento;
    var z = Input.GetAxis("Vertical") * _velocidadeDeMovimento;

    Rb.velocity = new Vector3(x, Rb.velocity.y, z);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag.Equals("Player"))
      _jogador = other.gameObject.GetComponent<ControleJogador>();
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag.Equals("Player"))
      _jogador = null;
  }
}
