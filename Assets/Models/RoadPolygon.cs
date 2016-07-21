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
                GameObject roadPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                roadPlane.transform.position = tile.transform.position + ((verts[i] + verts[i-1]) / 2);
                Vector3 scale = roadPlane.transform.localScale;
                scale.z = Vector3.Distance(verts[i], verts[i-1]) / 10;
                roadPlane.transform.localScale = scale;
                roadPlane.transform.LookAt(tile.transform.position + verts[i-1]);
                roadPlane.transform.parent = tile.transform;
            }
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
