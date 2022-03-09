using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Map;
using Mapbox.Unity.Map;

public class DrentheMarker : SpawnOnMap
{
    private GameObject _map;
    private GameObject[] _markerstToDisable;
    private GameObject _hunebedMarker;
    private GameObject _haringHappenMarker;

    public override void Start()
    {
        base.Start();
        _map = GameObject.FindWithTag("Map");
        _hunebedMarker = GameObject.FindGameObjectWithTag("Hunebed");
        _haringHappenMarker = GameObject.FindGameObjectWithTag("HaringHappen");
        _hunebedMarker.SetActive(false);
        _haringHappenMarker.SetActive(false);
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            // Reset ray with new mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Drenthe")
                {
                    hit.collider.gameObject.SetActive(false);
                    _map.GetComponent<AbstractMap>().SetZoom(8.6f);
                    _map.GetComponent<AbstractMap>().UpdateMap(new Mapbox.Utils.Vector2d(52.9167f, 6.5833f));
                    _hunebedMarker.SetActive(true);
                    _haringHappenMarker.SetActive(true);
                    GameObject.Find("Map").GetComponent<QuadTreeCameraMovement>().maxZoomer = 8.6f;
                }
                if (hit.collider.gameObject.tag == "Hunebed")
                {
                    hit.collider.gameObject.GetComponent<MarkerInfo>().SetMarkerInfo("Hunebedden","At this location you can explore the Hunebedden of Drenthe.", 1);
                    hit.collider.gameObject.GetComponent<MarkerInfo>().ShowInfo();
                    _haringHappenMarker.GetComponent<MarkerInfo>().HideInfo();
                }
                if (hit.collider.gameObject.tag == "HaringHappen")
                {
                    hit.collider.gameObject.GetComponent<MarkerInfo>().SetMarkerInfo("Haring Happen", "At this location you can litterly taste Drenthe trough a small fish.", 1);
                    hit.collider.gameObject.GetComponent<MarkerInfo>().ShowInfo();
                    _hunebedMarker.GetComponent<MarkerInfo>().HideInfo();
                }
            }
        }

    }
}
