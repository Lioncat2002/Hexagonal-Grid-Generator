﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct HexCoordinates
{
    public int X { get; private set; }
    public int Z { get; private set; }
    public int Y { get
        {
            return -X - Z;
        } }
    public HexCoordinates(int x,int z)
    {
        X = x;
        Z = z;
    }
    public static HexCoordinates FromOffsetCoordinates(int x,int z)
    {
        return new HexCoordinates(x-z/2, z);
    }
    public override string ToString()
    {
        return "("+X.ToString()+","+Y.ToString()+","+Z.ToString()+")";
    }
    public string ToStringOnSeperateLines()
    {
        return X.ToString() + "\n" +Y.ToString()+ "\n" + Z.ToString();
    }

    public static HexCoordinates FromPosition(Vector3 point)
    {
       float x = point.x / (HexMetrics.InnerRadius * 2f);
        float y = -x;
        float offset = point.z / (HexMetrics.OuterRadius * 3f);
        x -= offset;
        y -= offset;
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);
        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x-iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x-y-iZ);
            if(dX>dY&&dX>dZ)
            {
                iX = -iY - iZ;
            }
            else if(dZ>dY)
            {
                iZ = -iX - iY;
            }
        }
       return new HexCoordinates(iX,iZ);
   }
}
