
using UnityEngine;

public static class HexMetrics
{
    public const float OuterRadius = 10f;
    public const float InnerRadius=OuterRadius* 0.866025404f;

    public static Vector3[] corners = {
                            new Vector3(0,0,OuterRadius),
                            new Vector3(InnerRadius,0,OuterRadius*0.5f),
                            new Vector3(InnerRadius,0,-0.5f*OuterRadius),
                            new Vector3(0,0,-OuterRadius),
                            new Vector3(-InnerRadius,0,-0.5f*OuterRadius),
                            new Vector3(-InnerRadius,0,OuterRadius*0.5f),
                            new Vector3(0,0,OuterRadius)
    };
}
