using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using TMPro;

namespace XrRover.Utilities.Ros
{
    public class RosIpHandler
    {
        /// <summary>
        /// 專門處理 ros tcp connection 相關的處理。
        /// </summary>
        static public void StartConnect2Ros(ROSConnection rosConnection)
        {
            /// <summary>
            /// 結束先前連線，使用用戶輸入的 ip 與預設的 port 進行連接。
            /// </summary>
            int ros_connect_port = GameObject.Find("ROSConnectionPrefab").GetComponent<ROSConnection>().RosPort;
            string input_ip      = GameObject.Find("NumberContent").GetComponent<TextMeshPro>().text;
            if (rosConnection)
            {
                rosConnection.Connect(input_ip, ros_connect_port);
            }
        }

        static public void Disconnect2Ros(ROSConnection rosConnection)
        {
            /// <summary>
            /// 結束連線
            /// </summary>
            if (rosConnection)
            {
                rosConnection.Disconnect();
            }
        }
    }
}
