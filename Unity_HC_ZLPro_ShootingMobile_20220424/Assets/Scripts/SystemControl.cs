using UnityEngine;
using Photon.Pun;
using Cinemachine;

// namespace �R�W�Ŷ��G�{���϶�
namespace KID
{
    /// <summary>
    /// ����t�ΡG��ð����ʥ\��
    /// �����n�챱��Ⲿ��
    /// </summary>
    public class SystemControl : MonoBehaviourPun
    {
        [SerializeField, Header("���ʳt��"), Range(0, 300)]
        private float speed = 3.5f;
        [SerializeField, Header("�����V�ϥܽd��"), Range(0, 5)]
        private float rangeDirectionIcon = 2.5f;
        [SerializeField, Header("�������t��"), Range(0, 100)]
        private float speedTurn = 1.5f;
        [SerializeField, Header("�ʵe�Ѽƶ]�B")]
        private string parameterWalk = "�}���]�B";
        [SerializeField, Header("�e��")]
        private GameObject goCanvas;
        [SerializeField, Header("�e�����a��T")]
        private GameObject goCanvasPlayerInfo;
        [SerializeField, Header("�����V�ϥ�")]
        private GameObject goDirection;

        private Rigidbody rig;
        private Animator ani;
        private Joystick joystick;
        private Transform traDirectionIcon;
        private CinemachineVirtualCamera cvc;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            ani = GetComponent<Animator>();

            // �p�G�O�s�u�i�J�����a �N�ͦ����a�ݭn������
            if (photonView.IsMine)
            {
                PlayerUIFollow follow = Instantiate(goCanvasPlayerInfo).GetComponent<PlayerUIFollow>();
                follow.traPlayer = transform;

                traDirectionIcon = Instantiate(goDirection).transform;                                              // ���o�����V�ϥ�

                // transform.Find(�l����W��) - �z�L�W�ٷj�M�l����
                joystick = Instantiate(goCanvas).transform.Find("Dynamic Joystick").GetComponent<Joystick>();       // ���o�e�����������n��

                cvc = GameObject.Find("CM �޲z��").GetComponent<CinemachineVirtualCamera>();                         // ���o��v�� CM �޲z��
                cvc.Follow = transform;                                                                             // ���w�l�ܪ���
            }
            // �_�h ���O�i�J�����a �N��������t�ΡA�קK�����h�Ӫ���
            else
            {
                enabled = false;
            }
        }

        private void Update()
        {
            // GetJoystickValue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// ���o�����n���
        /// </summary>
        private void GetJoystickValue()
        {
            print("<color=yellow>�����G" + joystick.Horizontal + "</color>");
        }

        /// <summary>
        /// ���ʥ\��
        /// </summary>
        private void Move()
        {
            // ����.�[�t�� = �T���V�q(X�AY�AZ)
            rig.velocity = new Vector3(-joystick.Horizontal, 0, -joystick.Vertical) * speed;
        }

        /// <summary>
        /// ��s�����V�ϥܪ��y��
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            // �s�y�� = ���⪺�y�� + �T���V�q(�����n�쪺�����P����) * ��V�ϥܪ��d��)
            Vector3 pos = transform.position + new Vector3(-joystick.Horizontal, 0.5f, -joystick.Vertical) * rangeDirectionIcon;
            // ��s��V�ϥܪ��y�� = �s�y��
            traDirectionIcon.position = pos;
        }

        /// <summary>
        /// ���V��V�ϥ�
        /// </summary>
        private void LookDirectionIcon()
        {
            // ���o���V���� = �|�줸.���V����(��V�ϥ� - ����) - ��V�ϥܻP���⪺�V�q
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            // ���⪺���� = �|�줸.����(���⪺���סA���V���סA����t�� * �@�V���ɶ�)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            // ���⪺�کԨ��� = �T���V�q(0�A�쥻���کԨ���Y�A0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAnimation()
        {
            // �O�_�]�B = �����n�� ���� �����s �Ϊ� ���� �����s
            bool run = joystick.Horizontal != 0 || joystick.Vertical != 0;
            ani.SetBool(parameterWalk, run);
        }
    }
}
