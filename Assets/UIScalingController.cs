using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIScalingController : MonoBehaviour
{

    public Transform leftPanel, rightPanel, topPanel, botPanel;
    public Canvas uiCanvas;
    public Camera cam;

    private Vector2 referenceResolution;
    private float viewReferenceHeight;
    private float referenceSidePanelWidth;
    // Use this for initialization
    void Start()
    {
        CanvasScaler uiCanvasScaler = uiCanvas.GetComponent<CanvasScaler>();
        referenceResolution = uiCanvasScaler.referenceResolution;
        viewReferenceHeight = referenceResolution.y - topPanel.GetComponent<RectTransform>().rect.height - botPanel.GetComponent<RectTransform>().rect.height;
        referenceSidePanelWidth = leftPanel.GetComponent<RectTransform>().rect.width;
    }
    void OnGUI()
    {
        float aspectRatio = Screen.width / Screen.height;
        float desiredAspectRatio = referenceResolution.x / referenceResolution.y;
        float w = Screen.width;
        float h = Screen.height;
        float realViewScreenHeight = (viewReferenceHeight / referenceResolution.y) * Screen.height;

        float desiredRealViewWidth = realViewScreenHeight * desiredAspectRatio;
        float desiredRealPanelSize = (Screen.width - desiredRealViewWidth) / 2;
        float desiredPanelSize = desiredRealPanelSize / Screen.width * referenceResolution.x;

        leftPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(desiredPanelSize, leftPanel.GetComponent<RectTransform>().sizeDelta.y);
        rightPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(desiredPanelSize, rightPanel.GetComponent<RectTransform>().sizeDelta.y);

        float cameraY = botPanel.GetComponent<RectTransform>().rect.height / referenceResolution.y;
        float cameraHeight = viewReferenceHeight / referenceResolution.y;
        float cameraWidth = cameraHeight / desiredAspectRatio;
        float cameraX = (1 - cameraWidth) / 2;

        cam.rect = new Rect(cameraX, cameraY, cameraWidth, cameraHeight);
    }
}
