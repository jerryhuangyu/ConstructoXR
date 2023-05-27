using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

namespace XrRover.Utilities
{
    public static class Map
    {
        static private GameObject mapObject = null;

        static public void RotateWorldMap(SliderEventData eventData)
        {
            mapObject = GetMapObject();
            if (mapObject != null)
            {
                GlobalState.totalRotateAngle += (eventData.NewValue - 0.5f);
                mapObject.transform.RotateAround(GlobalState.afterFitPosition, Vector3.up, eventData.NewValue - 0.5f);

                UpdateMaptransform();
            }
        }
        static public void TranslateWorldMap(SliderEventData eventData, XrRover.MapDir direction)
        {
            float speed = eventData.NewValue - 0.5f;

            mapObject = GetMapObject();
            if (mapObject != null)
            {
                Vector3 dirVector3 = (direction == XrRover.MapDir.DirX) ? Vector3.right : (direction == XrRover.MapDir.DirZ) ? Vector3.forward : Vector3.zero;
                mapObject.transform.Translate(speed * Time.deltaTime * dirVector3);
                
                UpdateMaptransform();
            }
        }

        // helper function
        public static GameObject GetMapObject()
        {
            if (mapObject == null && GameObject.Find("RosMap"))
            {
                var rosMap = GameObject.Find("RosMap");
                return rosMap;
            }
            return mapObject;
        }

        public static void UpdateMaptransform()
        {
            GlobalState.afterFitPosition = mapObject.GetComponent<Renderer>().bounds.center;
        }
    }
}
