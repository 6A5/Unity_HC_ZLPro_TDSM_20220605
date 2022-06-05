using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System.Collections;

namespace KID
{
    /// <summary>
    /// ���˺޲z
    /// </summary>
    public class DamageManager : MonoBehaviourPun
    {
        [SerializeField, Header("��q"), Range(0, 1000)]
        private float hp = 200;
        [SerializeField, Header("�����S��")]
        private GameObject goVFXHit;
        [SerializeField, Header("���ѵۦ⾹")]
        private Shader shaderDissolve;

        private float hpMax;

        private string nameBullet = "�l�u";

        // �ҫ��Ҧ��������V���� �̭��]�t����y
        private SkinnedMeshRenderer[] smr;

        Material materialDissolve;

        private SystemControl systemControl;
        private SystemAttack systemAttack;

        [HideInInspector]
        public Image imgHp;
        [HideInInspector]
        public TextMeshProUGUI textHp;

        private void Awake()
        {
            systemControl = GetComponent<SystemControl>();
            systemAttack = GetComponent<SystemAttack>();

            hpMax = hp;

            // ���o�l����"��"������
            smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            // �s�W ���ѵۦ⾹ ����y
            materialDissolve = new Material(shaderDissolve);
            // �Q�ΰj�����C�Ӥl���󳣮M�W����y
            for (int i = 0; i < smr.Length; i++)
            {
                smr[i].material = materialDissolve;
            }

            if (photonView.IsMine) textHp.text = hp.ToString();
        }

        // �i�J
        private void OnCollisionEnter(Collision collision)
        {
            // �p�G ���O�ۤv�����a���� �N���X
            if (!photonView.IsMine) return;

            // �p�G �I������W�� �]�t �l�u �N�B�z ����
            if (collision.gameObject.name.Contains(nameBullet))
            {
                // collision.contacts[0] �I�쪺�Ĥ@�Ӫ���
                // point �I�쪫�󪺮y��
                Damage(collision.contacts[0].point);
            }
        }

        // ����
        private void OnCollisionStay(Collision collision)
        {
            
        }

        // ���}
        private void OnCollisionExit(Collision collision)
        {
            
        }

        private void Damage(Vector3 posHit)
        {
            hp -= 20;
            imgHp.fillAmount = hp / hpMax;

            // ��q = �ƾ�.����(��q�A�̤p�ȡA�̤j��)
            hp = Mathf.Clamp(hp, 0, hpMax);
            textHp.text = hp.ToString();

            // �s�u.�ͦ�(�S�ġA�����y�СA����)
            PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);

            if (hp <= 0) photonView.RPC("Dead", RpcTarget.All);
        }

        [PunRPC]
        private void Dead() 
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve() 
        {
            systemControl.traDirectionIcon.gameObject.SetActive(false);
            systemControl.enabled = false;
            systemAttack.enabled = false;

            float valueDissolve = 3;

            for (int i = 0; i < 20; i++)
            {
                valueDissolve -= 0.3f;
                // ��s�ۦ⾹�ݩ�
                materialDissolve.SetFloat("dissolve", valueDissolve);
                yield return new WaitForSeconds(0.06f);
            }
        }
    }
}

