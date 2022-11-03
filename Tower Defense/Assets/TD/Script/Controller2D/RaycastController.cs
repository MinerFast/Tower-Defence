using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    public const float skinWidth = .015f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    [HideInInspector]
    public float horizontalRaySpacing;

    [HideInInspector]
    public float verticalRaySpacing;

    public BoxCollider2D boxcollider2D;

    public bool disableBoxColliderOnStart = false;

    public RaycastOrigins raycastOrigins;

    #region MonoBehaviour
    public virtual void Awake()
    {
        if (boxcollider2D == null)
        {
            boxcollider2D = GetComponent<BoxCollider2D>();
        }

        if (disableBoxColliderOnStart)
        {
            boxcollider2D.enabled = false;
        }
    }
    public virtual void Start()
    {
        CalculateRaySpacing();
    }
    #endregion
    public void UpdateRaycastOrigins()
    {
        Bounds bound = boxcollider2D.bounds;
        bound.Expand(skinWidth * -2);
        raycastOrigins.bottomLeft = BottomLeft(bound);
        raycastOrigins.bottomRight = BottomRight(bound);
        raycastOrigins.topLeft = TopLeft(bound);
        raycastOrigins.topRight = TopRight(bound);
    }
    #region Vectors
    private Vector2 BottomLeft(Bounds bounds)
    {
        return new Vector2(bounds.min.x, bounds.min.y);
    }
    private Vector2 BottomRight(Bounds bounds)
    {
        return new Vector2(bounds.max.x, bounds.min.y);
    }
    private Vector2 TopLeft(Bounds bounds)
    {
        return new Vector2(bounds.min.x, bounds.max.y);
    }
    private Vector2 TopRight(Bounds bounds)
    {
        return new Vector2(bounds.max.x, bounds.max.y);
    }
    #endregion
    public void CalculateRaySpacing()
    {
        var magicCount = 2;
        Bounds bound = boxcollider2D.bounds;
        bound.Expand(skinWidth * -magicCount);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, magicCount, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, magicCount, int.MaxValue);

        horizontalRaySpacing = bound.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bound.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
