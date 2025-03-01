using System;
using System.Runtime.InteropServices;
using UnityEngine;
public class TransparentWindow : MonoBehaviour
{
    Vector3 worldPosition;
    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x00080000;
    const int WS_EX_TRANSPARENT = 0x00000020;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private IntPtr hWnd;
    private void Start()
    {
#if !UNITY_EDITOR_
        hWnd = GetActiveWindow();
        MARGINS margins = new MARGINS { cxLeftWidth = -1};
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetWindowPos(hWnd, HWND_TOPMOST, 0,0,0,0,0);
#endif
    }
    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        SetClickThrough(Physics2D.OverlapPoint(worldPosition) == null);
    }
    private void SetClickThrough(bool clickthrough)
    {
        if (clickthrough)
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        else
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
    }
}