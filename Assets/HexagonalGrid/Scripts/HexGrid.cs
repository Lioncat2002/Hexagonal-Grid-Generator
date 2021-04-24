﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;
    HexCell[] cells;
    public TextMeshProUGUI cellLabelPrefab;
    public Canvas gridCanvas;
    HexMesh hexMesh;
    private void Awake()
    {
        cells = new HexCell[height*width];
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();
        for(int z=0,i=0;z<height;z++)
        {
            for(int x=0;x<width;x++)
            {
                CreateCell(x,z,i++);
            }
        }
    }
    private void Start()
    {
        hexMesh.Triangulate(cells);
    }
    private void CreateCell(int x, int z, int i)
    {
     
        Vector3 position;
        position.x = (x+z*0.5f-z/2) * HexMetrics.InnerRadius*2f;
        position.y = 0f;
        position.z = z * HexMetrics.OuterRadius*1.5f;

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        TextMeshProUGUI label = Instantiate<TextMeshProUGUI>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = x + "\n" + z;
    }
}
