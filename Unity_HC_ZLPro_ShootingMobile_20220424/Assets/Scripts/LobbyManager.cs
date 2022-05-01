using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�ǰt�ж�
/// </summary>
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // GameObject �C������G�s�� Unity �������Ҧ�����
    // SerializeField �N�p�H�����ܦb�ݩʭ��O�W
    // Heder ���D�A�b�ݩʭ��O�W��ܲ���r���D
    [SerializeField, Header("�s�u���e��")]
    private GameObject goConnectView;
    [SerializeField, Header("��ԫ��s")]
    private Button btnBattle;
    [SerializeField]
    private Text textCountPlayer;

    // ���ѡG����
    // �����s��{�����q���y�{
    // 1. ���Ѥ��}����k Public Method
    // 2. ���s�b�I�� On Click ��]�w�I�s����k

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        btnBattle.interactable = true;
    }

    public void StartConnect()
    {
        goConnectView.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom("room", roomOptions);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("�[�J�ж��G" + PhotonNetwork.CurrentRoom.PlayerCount);
        textCountPlayer.text = "�s�u�H�� " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("���a�[�J�ж��G" + PhotonNetwork.CurrentRoom.PlayerCount);
        textCountPlayer.text = "�s�u�H�� " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
