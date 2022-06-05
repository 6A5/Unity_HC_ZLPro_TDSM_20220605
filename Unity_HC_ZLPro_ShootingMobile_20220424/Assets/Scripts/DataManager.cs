using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NkE1
{

    public class DataManager : MonoBehaviour
    {
        // 新增部屬作業內的連結
        private string gasLink = "https://script.google.com/macros/s/AKfycbxobBK4k85UdWWhgdHoFHu64PZsOAhgKho6NzqjxwxEXpQL44UJqCus_XjG74-9mL0i/exec";
        private WWWForm form;

        private Button btnGetData;
        private Text txtPlayerName;

        private void Start()
        {
            txtPlayerName = GameObject.Find("玩家名稱").GetComponent<Text>();
            btnGetData = GameObject.Find("取得玩家資料").GetComponent<Button>();
            btnGetData.onClick.AddListener(GetGASData);
        }

        /// <summary>
        /// 取得資料
        /// </summary>
        private void GetGASData()
        {
            form = new WWWForm();
            form.AddField("method", "取得");

            StartCoroutine(StartGetGASData());
        }

        private IEnumerator StartGetGASData()
        {
            // 連接某一個網址 - using
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                // 等待網頁連線要求
                yield return www.SendWebRequest();
                // 玩家名稱 = 指連線要求下載的訊息
                txtPlayerName.text = www.downloadHandler.text;
            }
        }
    }
}
