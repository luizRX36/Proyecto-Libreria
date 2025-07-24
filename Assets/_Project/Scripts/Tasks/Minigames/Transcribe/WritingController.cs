using UnityEngine;
using System.Collections.Generic;

public class WritingController : MonoBehaviour
{
    [Header("Configuración")]
    public Camera playerCamera;
    public Transform penTip;
    public GameObject linePrefab;
    public LayerMask writingSurfaceLayer;
    public float maxDistance = 5f;
    public float minPointDistance = 0.01f;

    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, writingSurfaceLayer))
        {
            penTip.position = hit.point + hit.normal * 0.01f; // posicionar la pluma sobre la superficie

            if (Input.GetMouseButtonDown(0))
            {
                StartNewLine();
                AddPoint(hit.point);
                isDrawing = true;
            }
            else if (Input.GetMouseButton(0) && isDrawing)
            {
                if (Vector3.Distance(points[points.Count - 1], hit.point) > minPointDistance)
                    AddPoint(hit.point);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDrawing = false;
                currentLine = null;
                points.Clear();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDrawing = false;
                currentLine = null;
                points.Clear();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMinigame();
        }
    }

    void StartNewLine()
    {
        GameObject newLine = Instantiate(linePrefab);
        currentLine = newLine.GetComponent<LineRenderer>();
        currentLine.positionCount = 0;
        points.Clear();
    }

    void AddPoint(Vector3 point)
    {
        points.Add(point);
        currentLine.positionCount = points.Count;
        currentLine.SetPosition(points.Count - 1, point);
    }

  

void ExitMinigame()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    Time.timeScale = 1f;
    gameObject.SetActive(false);
}
}