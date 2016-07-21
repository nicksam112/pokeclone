using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Helpers
{
    public static class Extensions
    {
        public static Vector2 ToVector2xz(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        public static Vector3 ToVector3xz(this Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }

        public static RoadType ToRoadType(this string s)
        {
            switch (s)
            {
                case "highway":
                    return RoadType.Highway;
                case "major_road":
                    return RoadType.MajorRoad;
                case "minor":
                    return RoadType.MinorRoad;
                case "rail":
                    return RoadType.Rail;
                case "path":
                    return RoadType.Path;
            }

            return RoadType.Path;
        }

        public static float ToWidthFloat(this RoadType s)
        {
            switch (s)
            {
                case RoadType.Highway:
                    return 10;
                case RoadType.MajorRoad:
                    return 5;
                case RoadType.MinorRoad:
                    return 3;
                case RoadType.Rail:
                    return 3;
                case RoadType.Path:
                    return 2;
            }

            return 2;
        }
    }
}
