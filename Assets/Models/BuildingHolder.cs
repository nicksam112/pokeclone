using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Models
{
    public class BuildingHolder
    {
        public GameObject GameObject { get; set; }
        public Vector3 Center { get; set; }
        public float Rotation { get; set; }
        public bool IsModelCreated;
        private List<Vector3> _verts;

        public BuildingHolder(Vector3 center, List<Vector3> verts)
        {
            IsModelCreated = false;
            Center = center;
            _verts = verts;
        }

        public GameObject CreateModel()
        {
            if (IsModelCreated)
                return null;

            var m = new GameObject().AddComponent<BuildingPolygon>();
            m.gameObject.transform.position = Center;
            m.gameObject.GetComponent<Renderer>().material = Resources.Load("buildingMaterial") as Material;
            m.Initialize(_verts);
            IsModelCreated = true;
            return m.gameObject;
        }

    }
}
