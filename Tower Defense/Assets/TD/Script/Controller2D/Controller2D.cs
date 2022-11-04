using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController
{

    float maxClimbAngle = 80;
    float maxDescendAngle = 80;
    float checkGroundAheadLength = 0.35f;

    public CollisionInfo collisions;

    [HideInInspector]
    public Vector2 playerInput;

    [HideInInspector]
    public bool HandlePhysic = true;

    [HideInInspector]
    public bool inverseGravity = false;

    private bool isFacingRight;

    [ReadOnly] public bool isWall;
    public override void Start()
    {
        base.Start();
        collisions.faceDir = 1;
    }
    #region Move


    public void Move(Vector3 velocityCount, Vector2 inputCount, bool standingFromPlatform = false)
    {
        CalculateRaySpacing();
        UpdateRaycastOrigins();

        collisions.Reset();
        collisions.velocityOld = velocityCount;
        playerInput = inputCount;

        if (velocityCount.x != 0)
        {
            collisions.faceDir = (int)Mathf.Sign(velocityCount.x);
        }

        if (velocityCount.y < 0)
        {
            DescendSlope(ref velocityCount);
        }

        if (HandlePhysic)
        {
            HorizontalCollisions(ref velocityCount);
            if (velocityCount.y != 0)
            {
                VerticalCollisions(ref velocityCount);
            }
        }

        CheckGroundedAhead(velocityCount);

        transform.Translate(velocityCount, Space.World);

        if (standingFromPlatform)
        {
            collisions.below = true;
        }
    }
    public void Move(Vector3 velocityCount, bool standingFromPlatform, bool _isFacingRight = false)
    {
        isFacingRight = _isFacingRight;
        Move(velocityCount, Vector2.zero, standingFromPlatform);
    }
    #endregion
    #region Collision
    void HorizontalCollisions(ref Vector3 velocityCount)
    {
        float directionX = collisions.faceDir;

        float rayLength = Mathf.Abs(velocityCount.x) + skinWidth;
        if (Mathf.Abs(velocityCount.x) < skinWidth)
        {
            rayLength = 5 * skinWidth;
        }

        isWall = false;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += (Vector2)transform.up * (horizontalRaySpacing * i);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {

                if (hit.distance == 0)
                {
                    continue;
                }

                float slopeAngle = Vector2.Angle(hit.normal, transform.up);

                if (slopeAngle > 85 && slopeAngle < 95)
                {
                    isWall = true;
                }

                if (i == 0 && slopeAngle <= maxClimbAngle)
                {
                    if (collisions.descendingSlope)
                    {
                        collisions.descendingSlope = false;
                        velocityCount = collisions.velocityOld;
                    }

                    float distanceToSlopeStart = 0;

                    if (slopeAngle != collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - skinWidth;
                        velocityCount.x -= distanceToSlopeStart * directionX;
                    }

                    ClimbSlope(ref velocityCount, slopeAngle);
                    velocityCount.x += distanceToSlopeStart * directionX;
                }

                if (!collisions.climbingSlope || slopeAngle > maxClimbAngle)
                {
                    velocityCount.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (collisions.climbingSlope)
                    {
                        velocityCount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocityCount.x);
                    }
                    var magicCount = 1;
                    collisions.left = directionX == -magicCount;
                    collisions.right = directionX == magicCount;

                    collisions.ClosestHit = hit;
                }
            }
        }
    }
    void VerticalCollisions(ref Vector3 velocityCount)
    {
        float directionY = Mathf.Sign(velocityCount.y);
        float rayLength = Mathf.Abs(velocityCount.y) + skinWidth;

        var magicCount = 1;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -magicCount) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

            rayOrigin += (Vector2)Vector2.right * (verticalRaySpacing * i + velocityCount.x);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if (hit)
            {
                if (hit.collider.tag == "Through")
                {
                    if (directionY == (inverseGravity ? -magicCount : magicCount) || hit.distance == 0)
                    {
                        continue;
                    }

                    if (collisions.fallingThroughPlatform)
                    {
                        continue;
                    }

                    if (playerInput.y == -magicCount)
                    {
                        collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", .2f);
                        continue;
                    }
                }

                velocityCount.y = (hit.distance - skinWidth) * directionY;

                rayLength = hit.distance;

                if (collisions.climbingSlope)
                {
                    velocityCount.x = velocityCount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocityCount.x);
                }

                if (!inverseGravity)
                {
                    collisions.below = directionY == -magicCount;
                    collisions.above = directionY == magicCount;
                }
                else
                {
                    collisions.below = directionY == magicCount;
                    collisions.above = directionY == -magicCount;
                }


                collisions.ClosestHit = hit;

                collisions.hitBelowObj = null;
                collisions.hitAboveObj = null;


                if (directionY == -magicCount)
                {
                    collisions.hitBelowObj = hit.collider.gameObject;
                }

                if (directionY == magicCount)
                {
                    collisions.hitAboveObj = hit.collider.gameObject;
                }
            }
        }

        if (collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocityCount.x);

            rayLength = Mathf.Abs(velocityCount.x) + skinWidth;

            Vector2 rayOrigin = ((directionX == -magicCount) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + (Vector2)transform.up * velocityCount.y;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.right * directionX, rayLength, collisionMask);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, transform.up);

                if (slopeAngle != collisions.slopeAngle)
                {
                    velocityCount.x = (hit.distance - skinWidth) * directionX;
                    collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }
    #endregion
    bool CheckGroundedAhead(Vector3 velocityCount)
    {
        float directionX = collisions.faceDir;
        var magicCount = 1;
        if (velocityCount.x == 0)
        {
            directionX = isFacingRight ? magicCount : -magicCount;
        }

        Vector2 rayOrigin = (directionX == -magicCount) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, checkGroundAheadLength, collisionMask);

        if (hit)
        {
            collisions.isGrounedAhead = true;

            return true;
        }
        else
        {

            return false;
        }
    }
    #region Slope
    void ClimbSlope(ref Vector3 velocityCount, float slopeAngle)
    {
        float moveDistance = Mathf.Abs(velocityCount.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (velocityCount.y <= climbVelocityY)
        {
            velocityCount.y = climbVelocityY;
            velocityCount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocityCount.x);

            collisions.below = true;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
        }
    }

    void DescendSlope(ref Vector3 velocityCount)
    {
        float directionX = Mathf.Sign(velocityCount.x);
        Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask);

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
            {
                if (Mathf.Sign(hit.normal.x) == directionX)
                {
                    if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocityCount.x))
                    {
                        float moveDistance = Mathf.Abs(velocityCount.x);
                        float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

                        velocityCount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocityCount.x);
                        velocityCount.y -= descendVelocityY;

                        collisions.slopeAngle = slopeAngle;
                        collisions.descendingSlope = true;
                        collisions.below = true;
                    }
                }
            }
        }
    }
    #endregion

   
    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool isGrounedAhead;
        public bool climbingSlope;
        public bool descendingSlope;
        public bool fallingThroughPlatform;

        public RaycastHit2D ClosestHit;
        public GameObject hitBelowObj, hitAboveObj;

        public float slopeAngle, slopeAngleOld;

        public Vector3 velocityOld;

        public int faceDir;

        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
            isGrounedAhead = false;
            climbingSlope = false;
            descendingSlope = false;

            slopeAngleOld = slopeAngle;

            slopeAngle = 0;
        }
    }

}
