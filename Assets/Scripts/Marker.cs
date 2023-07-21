using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class Marker : MonoBehaviour, IMixedRealityPointerHandler
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _markerSize = 20;

    public Texture2D mapTexture;

    private Color _tipColor;
    private Color[] _colors;
    private float _tipHeight;
    private RaycastHit _hit;
    private Vector2 _hitPos, _lastHitPos;
    private Quaternion _lastHitRot;
    private bool _hitedLastFrame;
    // Start is called before the first frame update
    void Start()
    {
        _tipHeight = _tip.localScale.y;
        _tipColor = _tip.GetComponent<Renderer>().material.color;
        _colors = Enumerable.Repeat(_tipColor, _markerSize * _markerSize).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        // check (1).map exist (2).raycast hit (3).and tag is MapTag
        if (IsMapExist() && Physics.Raycast(_tip.position, -transform.up, out _hit, 0.5f) && _hit.transform.CompareTag("MapTag"))
        {
            GameObject mapObject = GameObject.Find("RosMap");
            mapTexture = (Texture2D)mapObject.GetComponent<MeshRenderer>().material.mainTexture;

            _hitPos = new Vector2(_hit.textureCoord.x, _hit.textureCoord.y);
            var x = (int)(_hitPos.x * mapTexture.width - (_markerSize / 2));
            var y = (int)(_hitPos.y * mapTexture.height - (_markerSize / 2));
            if (y < 0 || y > mapTexture.height || x < 0 || x > mapTexture.width) return; // handle marker out of range

            if (_hitedLastFrame)
            {
                mapTexture.SetPixels(x, y, _markerSize, _markerSize, _colors);

                // lerp make drawing more smoothly
                for (float f = 0.01f; f < 1.00f; f += 0.03f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastHitPos.x, x, f);
                    var lerpY = (int)Mathf.Lerp(_lastHitPos.y, y, f);
                    mapTexture.SetPixels(lerpX, lerpY, _markerSize, _markerSize, _colors);
                }

                transform.rotation = _lastHitRot;
                mapTexture.Apply();
            }
            _lastHitPos = new Vector2(x, y);
            _lastHitRot = transform.rotation;
            _hitedLastFrame = true;
            return;
        }

        _hitedLastFrame = false;

        //if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        //{
        //    if (_touch.transform.CompareTag("Whiteboard")) // MapTag
        //    {
        //        if (_whiteboard == null)
        //        {
        //            _whiteboard = _touch.transform.GetComponent<Whiteboard>(); // GetComponent<MeshRenderer>()
        //        }

        //        _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

        //        var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2)); // .material.mainTexture.height
        //        var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2)); // .material.mainTexture.width

        //        if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

        //        if (_touchedLastFrame)
        //        {
        //            _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors); //.material.mainTexture.SetPixels(...)

        //            for (float f = 0.01f; f < 1.00f; f += 0.03f)
        //            {
        //                var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
        //                var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
        //                _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
        //            }

        //            transform.rotation = _lastTouchRot;
        //            _whiteboard.texture.Apply();
        //        }

        //        _lastTouchPos = new Vector2(x, y);
        //        _lastTouchRot = transform.rotation;
        //        _touchedLastFrame = true;
        //        return;
        //    }
        //}
    }

    private bool IsMapExist()
    {
        if (GameObject.Find("RosMap") != null)
            return true;
        else 
            return false;
    }


    /// <summary>
    /// 實做 IMixedRealityPointerHandler 的四個 functions
    /// </summary>

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
       
        GameObject markerTip = GameObject.Find("Marker");
        markerTip.transform.position = new Vector3(markerTip.transform.position.x, 100, markerTip.transform.position.z);
    }
}
