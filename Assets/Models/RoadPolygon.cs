using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Helpers;
using UnityEngine;

namespace Assets
{
    public enum RoadType
    {
        Highway,
        MajorRoad,
        MinorRoad,
        Rail,
        Path
    }

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    internal class RoadPolygon : MonoBehaviour
    {
        private Tile _tile;
        public string Id { get; set; }
        public RoadType Type { get; set; }
        private List<Vector3> _verts;

        public void Initialize(string id, Tile tile, List<Vector3> verts, string halfWidth)
        {
            Id = id;
            _tile = tile;
            Type = halfWidth.ToRoadType();
            _verts = verts;
            for (int i = 1; i < _verts.Count; i++)
            {
                
             GameObject roadPlane = CreateMesh(5);
             roadPlane.GetComponent<Renderer>().material = Resources.Load("roadMaterial") as Material;
             
                roadPlane.transform.position = tile.transform.position + ((verts[i] + verts[i-1]) / 2);
                Vector3 scale = roadPlane.transform.localScale;
                scale.z = Vector3.Distance(verts[i], verts[i-1]) / 10;
                roadPlane.transform.localScale = scale;
                roadPlane.transform.LookAt(tile.transform.position + verts[i-1]);
                roadPlane.transform.parent = tile.transform;
            }
        }
        
      private  GameObject CreateMesh(float size)
	{
		Mesh m = new Mesh();
		
		m.vertices = new Vector3[] {
			new Vector3(-size,0, size),
			new Vector3(size, 0, size),
			new Vector3(size, 0, -size),
			new Vector3(-size, 0, -size)
		};
		
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();

		GameObject scriptedPlane = new GameObject ();
		scriptedPlane.AddComponent<MeshRenderer>();
		MeshFilter meshas = scriptedPlane.AddComponent<MeshFilter>();
		meshas.mesh =m;

		return scriptedPlane;
	}

        private void Update()
        {
            for (int i = 1; i < _verts.Count; i++)
            {
                Debug.DrawLine(_tile.transform.position + _verts[i], _tile.transform.position + _verts[i - 1]);
            }

        }
    }
}
