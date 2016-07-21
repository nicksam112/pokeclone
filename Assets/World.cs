using Assets;
using UnityEngine;
using System.Collections;

public class World : MonoBehaviour 
{
    private Vector2 Center;
    private Vector2 Position;
    private int Zoom = 16;
    string status = "start";

    //BASICALLY UNUSED
    //LEGACY
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Center = calcTile(37.322880f, -121.995769f);
            Debug.Log(Center);
            Position = posInTile(37.322880f, -121.995769f);
            Debug.Log(Position);
            CreateTile();
            Vector3 pos = new Vector3((Position.x-0.5f) * 600, 0, (0.5f-Position.y) * 600);
            GameObject.FindGameObjectWithTag("Player").transform.position = pos;
            Debug.Log(pos);
            status = "no location service";
            yield break;
        }
            

        // Start service before querying location
        Input.location.Start();
        status = "rev up";

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            status = "timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            status = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            Center = calcTile(Input.location.lastData.latitude, Input.location.lastData.longitude);
            status = "Creating tile " + Center.x + ", " + Center.y;
            Position = posInTile(Input.location.lastData.latitude, Input.location.lastData.longitude);
            status = "Pos tile " + Position.x + ", " + Position.y;
            Vector3 pos = new Vector3((Position.x - 0.5f) * 300, 0, (0.5f - Position.y) * 300);
            GameObject.FindGameObjectWithTag("Player").transform.position = pos;
            CreateTile();
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
    void CreateTile () 
    {
        var go = new GameObject("tile");
        var tile = go.AddComponent<Tile>();
        StartCoroutine(tile.CreateTile(new Vector2(Center.x, Center.y), new Vector2(transform.position.x, transform.position.z), Zoom));
    }

    public void CreateTile(Vector2 pos)
    {
        var go = new GameObject("tile");
        var tile = go.AddComponent<Tile>();
        StartCoroutine(tile.CreateTile(new Vector2(pos.x, pos.y), new Vector2(transform.position.x, transform.position.z), Zoom));
    }

    Vector2 calcTile(float lat, float lng)
    {
        //pseudo
        //n = 2 ^ zoom
        //xtile = n * ((lon_deg + 180) / 360)
        //ytile = n * (1 - (log(tan(lat_rad) + sec(lat_rad)) / π)) / 2

        float n = Mathf.Pow(2, Zoom);
        float xtile = n * ((lng + 180) / 360);
        float ytile = n * (1 - (Mathf.Log(Mathf.Tan(Mathf.Deg2Rad * lat) + (1f / Mathf.Cos(Mathf.Deg2Rad * lat))) / Mathf.PI)) / 2f;
        return new Vector2((int)xtile, (int)ytile);
    }

    Vector2 posInTile(float lat, float lng)
    {
        float n = Mathf.Pow(2, Zoom);
        float xtile = n * ((lng + 180) / 360);
        float ytile = n * (1 - (Mathf.Log(Mathf.Tan(Mathf.Deg2Rad * lat) + (1f / Mathf.Cos(Mathf.Deg2Rad * lat))) / Mathf.PI)) / 2f;
        return new Vector2(xtile - (int)xtile, ytile - (int)ytile);
    }
	
	void Update () 
    {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 30), Input.location.lastData.latitude.ToString());
        GUI.Label(new Rect(10, 50, 500, 30), Input.location.lastData.longitude.ToString());
        GUI.Label(new Rect(10, 100, 500, 30), status);
        //GUI.Label(new Rect(10, 10, 500, 30), Center.x.ToString());
        //GUI.Label(new Rect(10, 50, 500, 30), Center.y.ToString());
    }
}
