using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Models
{
    public class WaterHolder
    {
        public GameObject GameObject { get; set; }
        public Vector3 Center { get; set; }
        public float Rotation { get; set; }
        public bool IsModelCreated;
        private List<Vector3> _verts;

        public WaterHolder(Vector3 center, List<Vector3> verts)
        {
            IsModelCreated = false;
            Center = center;
            _verts = verts;
        }

        public GameObject CreateModel()
        {
            if (IsModelCreated)
                return null;

            var m = new GameObject().AddComponent<WaterPolygon>();
            m.gameObject.transform.position = Center;
            m.gameObject.GetComponent<Renderer>().material = Resources.Load("waterMaterial") as Material;
            m.Initialize(_verts);
            IsModelCreated = true;
            return m.gameObject;
        }

    }
}
