using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    private Vector3 clickPosition;
    public Vector3 HitPos;
    public bool CanCreate;
    public GameObject CubePref;
    public string GroundTag;
    public string GeometryTag;
    public string BundleName;
    void Start()
    {
        
    }

    void Update()
    {
        CreateObject(); 
    }

    void CreateObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 1));


            int layerMask = 1 << 8;

            layerMask = ~layerMask;

            RaycastHit hit;
            Vector3 fromPosition = Camera.main.transform.position;
            Vector3 toPosition = new Vector3(clickPosition.x, clickPosition.y, clickPosition.z);
            Vector3 direction = toPosition - fromPosition;
            if (Physics.Raycast(fromPosition, direction, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(fromPosition, direction * hit.distance, Color.yellow);
                HitPos = hit.point;
                if (hit.transform.tag == GroundTag)
                {
                    CanCreate = true;
                }
                if(hit.transform.tag == GeometryTag)
                {
                    hit.transform.gameObject.GetComponent<GeometryObjectModel>().ClickCount++;
                }
            }
            else
            {
                Debug.DrawRay(fromPosition, direction * 1000, Color.white);
                CanCreate = false;
            }


            if (CanCreate)
            {
                LoadPrefabGameObject(HitPos);
                CanCreate = false;
            }
        }
    }


    void LoadPrefabGameObject(Vector3 position)
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + BundleName);
        if (localAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        int index = Random.Range(0, localAssetBundle.GetAllAssetNames().Length);
        string assetName = localAssetBundle.GetAllAssetNames()[index];
        Debug.Log(name);
        GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName);
        Instantiate(asset,position, Quaternion.identity);
        localAssetBundle.Unload(false);
    }
}
