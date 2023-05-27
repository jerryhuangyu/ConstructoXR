using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using OccupancyGridMsg = RosMessageTypes.Nav.OccupancyGridMsg;
using RosMessageTypes.Geometry;

public class RosMapSubscriber : MonoBehaviour
{
    public float mapResolution;
    public uint mapHeight;
    public uint mapWidth;
    public PointMsg mapOriginPosition;
    public QuaternionMsg mapOriginOrientation;

    [SerializeField]
    private string m_TopicName = "/map";
    [SerializeField]
    private float y_displace = (float)-1.65;
    
    private void Start()
    {
        // Subscribe to the ROS topic for map messages and register the DrawMap method as the callback
        ROSConnection.GetOrCreateInstance().Subscribe<OccupancyGridMsg>(m_TopicName, DrawMap);
    }

    private void DrawMap(OccupancyGridMsg map_msg)
    {
        RemoveExistingMap();

        GetMapMsgData(map_msg);
        var (origin, rotation) = ConvertOriginToFLUCoordinates(mapOriginPosition, mapOriginOrientation);

        rotation.eulerAngles += new Vector3(0, -90, 0);

        Vector3 drawOrigin = CalculateDrawOrigin(origin, rotation, y_displace, mapResolution);
        Vector3 localScale = CalculateLocalScale(mapWidth, mapHeight, mapResolution);

        GameObject mapObject = CreateMapObject(drawOrigin, rotation, localScale);

        Mesh mapMesh = CreateMapMesh();
        ApplyMeshToMapObject(mapObject, mapMesh);
        Texture2D mapTexture = CreateMapTexture(map_msg.data);
        ApplyTextureToMapObject(mapObject, mapTexture);

        AddColliderToMapObject(mapObject);
        InputHandlerForMarker(mapObject);
        InitGlobalStateForPositionAndQuaternionAndResolution(mapObject, rotation, mapResolution);
    }

    /// <summary>
    /// Adds the MapPointerHandler component to the specified GameObject for handling map marker input.
    /// </summary>
    /// <param name="obj"></param>
    private void InputHandlerForMarker(GameObject obj)
    {
        obj.AddComponent<MapPointerHandler>();
    }

    /// <summary>
    /// Removes any existing map by finding and destroying the map object with the name "RosMap".
    /// </summary>
    private void RemoveExistingMap()
    {
        GameObject existingMap = GameObject.Find("RosMap");
        if (existingMap != null)
        {
            Destroy(existingMap);
        }
    }

    /// <summary>
    /// Retrieves the map message data from ros msg and assigns it to the corresponding variables.
    /// </summary>
    /// <param name="map_msg"></param>
    private void GetMapMsgData(OccupancyGridMsg map_msg)
    {
        mapResolution = map_msg.info.resolution;
        mapHeight = map_msg.info.height;
        mapWidth = map_msg.info.width;
        mapOriginPosition = map_msg.info.origin.position;
        mapOriginOrientation = map_msg.info.origin.orientation;
    }

    private (Vector3, Quaternion) ConvertOriginToFLUCoordinates(PointMsg originPosition, QuaternionMsg originOrientation)
    {
        Vector3 fluPosition = originPosition.From<FLU>();
        Quaternion fluOrientation = originOrientation.From<FLU>();
        return (fluPosition, fluOrientation);
    }

    /// <summary>
    /// Calculates the draw origin position for the map object based on the specified origin, rotation, y displacement, and resolution.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="rotation"></param>
    /// <param name="yDisplace"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    private Vector3 CalculateDrawOrigin(Vector3 origin, Quaternion rotation, float yDisplace, float resolution)
    {
        Vector3 drawOrigin = origin - rotation * new Vector3(resolution * 0.5f, 0, resolution * 0.5f) + new Vector3(0, yDisplace, 0);
        return drawOrigin;
    }

    /// <summary>
    /// Calculates the local scale vector for the map object based on the specified width, height, and resolution.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="resolution"></param>
    /// <returns></returns>
    private Vector3 CalculateLocalScale(uint width, uint height, float resolution)
    {
        Vector3 localScale = new Vector3(width * resolution, 1, height * resolution);
        return localScale;
    }

    /// <summary>
    /// Creates a map object with the specified position, rotation, and scale.
    /// </summary>
    /// <param name="drawOrigin">The position to set for the map object.</param>
    /// <param name="rotation">The rotation to set for the map object.</param>
    /// <param name="localScale">The scale to set for the map object.</param>
    /// <returns>Map object.</returns>
    private GameObject CreateMapObject(Vector3 drawOrigin, Quaternion rotation, Vector3 localScale)
    {
        GameObject mapObject = new GameObject("RosMap");
        mapObject.transform.SetPositionAndRotation(drawOrigin, rotation);
        mapObject.transform.localScale = localScale;
        return mapObject;
    }

    private Mesh CreateMapMesh()
    {
        Mesh mapMesh = new Mesh();
        mapMesh.vertices = new[] { Vector3.zero, new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0) };
        mapMesh.uv = new[] { Vector2.zero, Vector2.up, Vector2.one, Vector2.right };
        mapMesh.triangles = new[] { 0, 1, 2, 2, 3, 0 };
        return mapMesh;
    }

    /// <summary>
    /// Applies the provided mesh to the map object by adding a MeshFilter and MeshRenderer components.
    /// </summary>
    /// <param name="mapObject">The map object to apply the mesh to.</param>
    /// <param name="mapMesh">The mesh to apply.</param>
    private void ApplyMeshToMapObject(GameObject mapObject, Mesh mapMesh)
    {
        MeshFilter meshFilter = mapObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = mapMesh;
        meshFilter.sharedMesh.name = "RosMap";
        mapObject.AddComponent<MeshRenderer>();
    }

    /// <summary>
    /// Create a texture based on the occupancy grid values
    /// </summary>
    /// <param name="textureData"></param>
    /// <returns></returns>
    private Texture2D CreateMapTexture(sbyte[] textureData)
    {
        Texture2D mapTexture = new Texture2D((int)mapWidth, (int)mapHeight, TextureFormat.R8, true);
        mapTexture.wrapMode = TextureWrapMode.Clamp;
        mapTexture.filterMode = FilterMode.Point;
        mapTexture.SetPixelData(textureData, 0);
        mapTexture.Apply();
        return mapTexture;
    }

    private void ApplyTextureToMapObject(GameObject mapObject, Texture2D mapTexture)
    {
        MeshRenderer renderer = mapObject.GetComponent<MeshRenderer>();
        renderer.material.mainTexture = mapTexture;
        renderer.material.shader = Shader.Find("Unlit/OccupancyGrid");
    }

    /// <summary>
    /// Add collider for raycast to hit on map object (layer: MapTag)
    /// </summary>
    /// <param name="mapObject"></param>
    private void AddColliderToMapObject(GameObject mapObject)
    {
        mapObject.AddComponent<MeshCollider>();
        mapObject.tag = "MapTag";
    }

    /// <summary>
    ///  Init original position and rotation and map's resolution in Global state
    /// </summary>
    /// <param name="mapObject"></param>
    /// <param name="rotation"></param>
    /// <param name="resolution"></param>
    private void InitGlobalStateForPositionAndQuaternionAndResolution(GameObject mapObject, Quaternion rotation, float resolution)
    {
        XrRover.GlobalState.originPosition = mapObject.GetComponent<Renderer>().bounds.center;
        XrRover.GlobalState.afterFitPosition = mapObject.GetComponent<Renderer>().bounds.center;
        XrRover.GlobalState.originQuaternion = rotation;
        XrRover.GlobalState.afterFitQuaternion = rotation;
        XrRover.GlobalState.resolution = resolution;
    }
}