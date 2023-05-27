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
        /// �M���B�z ros tcp connection �������B�z�C
        /// </summary>
        static public void StartConnect2Ros(ROSConnection rosConnection)
        {
            /// <summary>
            /// �������e�s�u�A�ϥΥΤ��J�� ip �P�w�]�� port �i��s���C
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
            /// �����s�u
            /// </summary>
            if (rosConnection)
            {
                rosConnection.Disconnect();
            }
        }
    }
}
