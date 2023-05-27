using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Nav;
using System;
using System.Linq;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;

namespace XrRover.Ros
{
    public class MapUpdatePublisher : MonoBehaviour
    {
        public GameObject pointRef;
        [SerializeField]
        private string topicName = "/map";

        private ROSConnection ros;
        private Texture2D updatedMapTexture;

        void Start()
        {
            AppEvents.current.onPublishUpdateMapTrigger += PublishUpdatedMap;

            ros = ROSConnection.GetOrCreateInstance();
            ros.RegisterPublisher<OccupancyGridMsg>(topicName);
        }

        public void PublishUpdatedMap()
        {
            updatedMapTexture = GameObject.Find("Marker").GetComponent<Marker>().mapTexture;
            RosMapSubscriber originMap = GameObject.Find("RosMapSubscriber").GetComponent<RosMapSubscriber>();

            var reverTransform = RevertFitRevise(originMap.mapOriginPosition.From<FLU>(), originMap.mapOriginOrientation.From<FLU>());

            OccupancyGridMsg updatedMapMsg = new OccupancyGridMsg();
            updatedMapMsg.data = (sbyte[])(Array)updatedMapTexture.GetRawTextureData();
            updatedMapMsg.info.resolution = originMap.mapResolution;
            updatedMapMsg.info.height = originMap.mapHeight;
            updatedMapMsg.info.width = originMap.mapWidth;
            updatedMapMsg.info.origin.position = originMap.mapOriginPosition;
            updatedMapMsg.info.origin.orientation = originMap.mapOriginOrientation;
            //updatedMapMsg.info.origin.position = reverTransform.Item1.To<FLU>();
            //updatedMapMsg.info.origin.orientation = reverTransform.Item2.To<FLU>();

            ros.Publish(topicName, updatedMapMsg);

            GlobalState.afterFitPosition = GlobalState.originPosition;
            GlobalState.totalRotateAngle = 0.0f;
            GlobalState.needMapShow = true;

            // TODO: image size is not match

            //MeshRenderer updatedMapRender = updatedMapObject.GetComponent<MeshRenderer>();
            //updatedMapMsg.data = updatedMapRender.material.mainTexture.
            //updatedMap.info.origin.position;
            //updatedMap.info.origin.orientation;
            //updatedMap.info.height;
            //updatedMap.info.width;
            //updatedMap.info.resolution;
            //updatedMap.data;
        }

        /// <summary>
        /// �٭�]�ե��a�ϩҳy��������P����
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public (Vector3, Quaternion) RevertFitRevise(Vector3 position, Quaternion rotation)
        {
            // �Q�� ref ����A�٭�ե���(1)����P(2)����y�����������v�T
            GameObject refObj = Instantiate(pointRef, position, rotation);
            refObj.transform.RotateAround(GlobalState.afterFitPosition, Vector3.up, -GlobalState.totalRotateAngle);
            Quaternion reviceRotation = refObj.transform.rotation;
            // �٭�ե��������v�T
            Vector3 revicePosition = refObj.transform.position - (GlobalState.afterFitPosition - GlobalState.originPosition);
            Destroy(refObj);
            return (revicePosition, reviceRotation);
        }
    }
}
