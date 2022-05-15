using UnityEngine;
using Photon.Pun;

namespace KID
{
    /// <summary>
    /// ���a��T�����l��
    /// �����l�ܪ��a����y��
    /// </summary>
    public class PlayerUIFollow : MonoBehaviourPun
    {
        [SerializeField, Header("�첾")]
        private Vector3 v3Offset;
        private string namePlayer = "�Ԥh";
        private Transform traPlayer;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                // ���a�ܧΤ��� = �C������.�M��(����W��).�ܧΤ���
                traPlayer = GameObject.Find(namePlayer).transform;
            }
        }

        private void Update()
        {
            Follow();
        }

        /// <summary>
        /// �l�ܪ��a
        /// </summary>
        private void Follow()
        {
            transform.position = traPlayer.position + v3Offset;
        }
    }
}

