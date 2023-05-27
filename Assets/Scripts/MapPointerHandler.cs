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
        planeSpot = markerTip.transform.position.y; // TODO: �ϥίu���a�Ϫ����{
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // CHECK: �p�G�즲�S���ΡA���^ OnPointerClicked ���U
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
                // �]�w�ɯ�ؼаѦҤ��_�l��m
                isEndPlace = !isEndPlace;
                navGoalRefStartPoint = GetPoint(eventData);
                MoveNavRefStartPoint2HitSpot(eventData);
            }
            else
            {
                // �N�ɯ�ؼЫ��V�]�w��V
                isEndPlace = !isEndPlace;
                Vector3 navGoalRefEndPoint = GetPoint(eventData);
                Vector3 direction = navGoalRefEndPoint - navGoalRefStartPoint;
                RotateNavRef2Pointer(direction);
            }
        }
    }


    /// <summary>
    /// �N Marker �ѦҪ��󲾰ʨ즹��m
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
    /// �N Navigation Goal �ѦҪ��󲾰ʨ즹��m
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
    /// �N Navigation Goal �ѦҪ��󭱹惡��V
    /// </summary>
    private void RotateNavRef2Pointer(Vector3 direction)
    {

        NavGoalArrowRef.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0); // �]���ҫ���l�������Y�A�ץ� 90 ��
    }

    /// <summary>
    /// �ˬd Pointer's Input �ӷ��O�_�P Target �ӷ��ۦP�A�æ^�� Pointer's Result
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
