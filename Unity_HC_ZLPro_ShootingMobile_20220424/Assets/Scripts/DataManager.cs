using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NkE1
{

    public class DataManager : MonoBehaviour
    {
        // �s�W���ݧ@�~�����s��
        private string gasLink = "https://script.google.com/macros/s/AKfycbz23GxHNGSe19oZnGO-sBhQaOObPNJ3WtPOXrQz7HBsoeLbWVptGJ-bhr9PHgml8wy5Jg/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text txtPlayerName;
        private TMP_InputField inputField;

        private void Start()
        {
            txtPlayerName = GameObject.Find("���a�W��").GetComponent<Text>();
            btnGetData = GameObject.Find("���o���a���").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);

            inputField = GameObject.Find("��s���a�W��").GetComponent<TMP_InputField>();
            inputField.onEndEdit.AddListener(SetGASData);
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

        private void SetGASData(string value) 
        {
            form = new WWWForm();
            form.AddField("method", "�]�w");
            form.AddField("playerName", inputField.text);

            StartCoroutine(StartSetGASData());
        }

        private IEnumerator StartSetGASData() 
        {
            // �s���Y�@�Ӻ��} - using
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                // ���ݺ����s�u�n�D
                yield return www.SendWebRequest();
                txtPlayerName.text = inputField.text;
                print(www.downloadHandler.text);
            }
        }
    }
}
