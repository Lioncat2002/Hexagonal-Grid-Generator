using System;
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
    public GameObject cubes;
    public LayerMask gnd;
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
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }
    void HandleInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit, Mathf.Infinity,gnd))
        {
            TouchCell(hit.point);
        }
    }

    private void TouchCell(Vector3 point)
    {
        point = transform.InverseTransformPoint(point);
         HexCoordinates coordinates = HexCoordinates.FromPosition(point);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        Debug.Log(cell.transform.position);
        if (!cell.isOccupied)
        {
            Instantiate(cubes, cell.transform.position, Quaternion.identity);
            cell.isOccupied = true;
        }
        Debug.Log("Touched at:" + coordinates);
    }
}
