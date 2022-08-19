using System;
using UnityEngine;

public static class PlayerCollisions
{
    public enum Side
    {
        Top,
        Left,
        Bottom,
        Right
    }

    public static bool CollidedWithSide(GameObject self, GameObject other, Side targetSide)
    {
        switch (targetSide)
        {
            case Side.Bottom:
                float selfBottomBound = self.transform.position.y - self.transform.localScale.y / 2f;
                float otherTopBound = other.transform.position.y + other.transform.localScale.y / 2f;
                return otherTopBound <= selfBottomBound;
            case Side.Top:
                float selfTopBound = self.transform.position.y + self.transform.localScale.y / 2f;
                float otherBottomBound = other.transform.position.y - other.transform.localScale.y / 2f;
                return otherBottomBound >= selfTopBound;
            case Side.Left:
                float selfLeftBound = self.transform.position.x - self.transform.localScale.x / 2f;
                float otherRightBound = other.transform.position.x + other.transform.localScale.x / 2f;
                return otherRightBound <= selfLeftBound;
            case Side.Right:
                float selfRightBound = self.transform.position.x + self.transform.localScale.x / 2f;
                float otherLeftBound = other.transform.position.x - other.transform.localScale.x / 2f;
                return otherLeftBound >= selfRightBound;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetSide), targetSide, "Side out of range.");
        }
    }
}
