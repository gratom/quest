using UnityEngine;

namespace Tools
{
    public static class TransformTool
    {
        #region set position

        public static Transform SetPosition(this Transform transform, Vector3 Pos)
        {
            transform.position = Pos;
            return transform;
        }

        public static Transform SetPositionX(this Transform transform, float XPos)
        {
            transform.position = new Vector3(XPos, transform.position.y, transform.position.z);
            return transform;
        }

        public static Transform SetPositionY(this Transform transform, float YPos)
        {
            transform.position = new Vector3(transform.position.x, YPos, transform.position.z);
            return transform;
        }

        public static Transform SetPositionZ(this Transform transform, float ZPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, ZPos);
            return transform;
        }

        #endregion set position

        #region offset position

        public static Transform OffsetPosition(this Transform transform, Vector3 Pos)
        {
            transform.position = transform.position + Pos;
            return transform;
        }

        public static Transform OffsetPositionX(this Transform transform, float XPos)
        {
            transform.position = new Vector3(transform.position.x + XPos, transform.position.y, transform.position.z);
            return transform;
        }

        public static Transform OffsetPositionY(this Transform transform, float YPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + YPos, transform.position.z);
            return transform;
        }

        public static Transform OffsetPositionZ(this Transform transform, float ZPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ZPos);
            return transform;
        }

        #endregion offset position

        #region set local position

        public static Transform SetLocalPosition(this Transform transform, Vector3 Pos)
        {
            transform.localPosition = Pos;
            return transform;
        }

        public static Transform SetLocalPositionX(this Transform transform, float XPos)
        {
            transform.localPosition = new Vector3(XPos, transform.localPosition.y, transform.localPosition.z);
            return transform;
        }

        public static Transform SetLocalPositionY(this Transform transform, float YPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, YPos, transform.localPosition.z);
            return transform;
        }

        public static Transform SetLocalPositionZ(this Transform transform, float ZPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, ZPos);
            return transform;
        }

        #endregion set local position

        #region offset local position

        public static Transform OffsetLocalPosition(this Transform transform, Vector3 Pos)
        {
            transform.localPosition = transform.localPosition + Pos;
            return transform;
        }

        public static Transform OffsetLocalPositionX(this Transform transform, float XPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + XPos, transform.localPosition.y, transform.localPosition.z);
            return transform;
        }

        public static Transform OffsetLocalPositionY(this Transform transform, float YPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + YPos, transform.localPosition.z);
            return transform;
        }

        public static Transform OffsetLocalPositionZ(this Transform transform, float ZPos)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + ZPos);
            return transform;
        }

        #endregion offset local position

        #region truncate position

        public static Transform TruncatePositionMax(this Transform transform, Vector3 Pos)
        {
            Vector3 v = transform.position;
            v.x = v.x > Pos.x ? Pos.x : v.x;
            v.y = v.y > Pos.y ? Pos.y : v.y;
            v.z = v.z > Pos.z ? Pos.z : v.z;
            transform.position = v;
            return transform;
        }

        public static Transform TruncatePositionMin(this Transform transform, Vector3 Pos)
        {
            Vector3 v = transform.position;
            v.x = v.x < Pos.x ? Pos.x : v.x;
            v.y = v.y < Pos.y ? Pos.y : v.y;
            v.z = v.z < Pos.z ? Pos.z : v.z;
            transform.position = v;
            return transform;
        }

        public static Transform TruncateLocalPositionMax(this Transform transform, Vector3 Pos)
        {
            Vector3 v = transform.localPosition;
            v.x = v.x > Pos.x ? Pos.x : v.x;
            v.y = v.y > Pos.y ? Pos.y : v.y;
            v.z = v.z > Pos.z ? Pos.z : v.z;
            transform.localPosition = v;
            return transform;
        }

        public static Transform TruncateLocalPositionMin(this Transform transform, Vector3 Pos)
        {
            Vector3 v = transform.localPosition;
            v.x = v.x < Pos.x ? Pos.x : v.x;
            v.y = v.y < Pos.y ? Pos.y : v.y;
            v.z = v.z < Pos.z ? Pos.z : v.z;
            transform.localPosition = v;
            return transform;
        }

        public static Transform TruncateByRadius(this Transform transform, Vector3 center, float radius)
        {
            if ((transform.position - center).magnitude > radius)
            {
                transform.position = center + (transform.position - center).normalized * radius;
            }
            return transform;
        }

        #endregion truncate position
    }
}