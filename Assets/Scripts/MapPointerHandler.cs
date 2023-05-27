using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using XrRover;

public class MapPointerHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    private GameObject NavGoalArrowRef;
    private InputSourceType sourceType = InputSourceType.Hand;
    private GameObject markerTip;
    private float planeSpot;
    private Vector3 navGoalRefStartPoint;
    private bool isEndPlace = false;

    void Start()
    {
        markerTip = GameObject.Find("Marker");
        NavGoalArrowRef = GameObject.Find("NavGoalArrowRef");
        planeSpot = markerTip.transform.position.y; // TODO: 使用真的地圖的高程
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // CHECK: 如果拖曳沒有用，換回 OnPointerClicked 底下
        if (GlobalState.isDrawMapEnble)
        {
            MoveMarkerToPointerHitSpot(eventData);
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (GlobalState.isPlaceNavGoalRef)
        {
            if (!isEndPlace)
            {
                // 設定導航目標參考之起始位置
                isEndPlace = !isEndPlace;
                navGoalRefStartPoint = GetPoint(eventData);
                MoveNavRefStartPoint2HitSpot(eventData);
            }
            else
            {
                // 將導航目標指向設定方向
                isEndPlace = !isEndPlace;
                Vector3 navGoalRefEndPoint = GetPoint(eventData);
                Vector3 direction = navGoalRefEndPoint - navGoalRefStartPoint;
                RotateNavRef2Pointer(direction);
            }
        }
    }


    /// <summary>
    /// 將 Marker 參考物件移動到此位置
    /// </summary>
    private void MoveMarkerToPointerHitSpot(MixedRealityPointerEventData eventData)
    {
        var hitResult = GetHitResultFromHandInput(eventData, sourceType);
        if (hitResult != null)
        {
            markerTip.transform.position = new Vector3(hitResult.Details.Point.x, hitResult.Details.Point.y + 0.1f, hitResult.Details.Point.z);
        }
    }

    /// <summary>
    /// 將 Navigation Goal 參考物件移動到此位置
    /// </summary>
    private void MoveNavRefStartPoint2HitSpot(MixedRealityPointerEventData eventData)
    {
        var hitResult = GetHitResultFromHandInput(eventData, sourceType);
        if (hitResult != null)
        {
            NavGoalArrowRef.transform.position = new Vector3(hitResult.Details.Point.x, hitResult.Details.Point.y + 0.05f, hitResult.Details.Point.z);
        }
    }

    /// <summary>
    /// 將 Navigation Goal 參考物件面對此方向
    /// </summary>
    private void RotateNavRef2Pointer(Vector3 direction)
    {

        NavGoalArrowRef.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0); // 因為模型初始角度關係，修正 90 度
    }

    /// <summary>
    /// 檢查 Pointer's Input 來源是否與 Target 來源相同，並回傳 Pointer's Result
    /// </summary>
    private IPointerResult GetHitResultFromHandInput(MixedRealityPointerEventData eventData, InputSourceType targetSource)
    {
        if (eventData.InputSource.SourceType == targetSource)
        {
            var hitResult = eventData.Pointer.Result;
            return hitResult;
        }
        return null;
    }

    /// <summary>
    /// get Point's position in Pointer event
    /// </summary>
    private Vector3 GetPoint(MixedRealityPointerEventData eventData)
    {
        var resultPoint = eventData.Pointer.Result.Details.Point;
        return new Vector3(resultPoint.x, resultPoint.y, resultPoint.z);
    }
}
