using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NkE1
{

    public class DataManager : MonoBehaviour
    {
        // �s�W���ݧ@�~�����s��
        private string gasLink = "https://script.google.com/macros/s/AKfycbxobBK4k85UdWWhgdHoFHu64PZsOAhgKho6NzqjxwxEXpQL44UJqCus_XjG74-9mL0i/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text txtPlayerName;

        private void Start()
        {
            txtPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a���").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);
        }

        /// <summary>
        /// ���o���
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StartGetGASData());
        }

        private IEnumerator StartGetGASData()
        {
            // �s���Y�@�Ӻ��} - using
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                // ���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                // ���a�W�� = ���s�u�n�D�U�����T��
                txtPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}
