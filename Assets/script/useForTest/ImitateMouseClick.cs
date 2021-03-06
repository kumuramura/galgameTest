
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ImitateMouseClick : MonoBehaviour
{

    public int a=365;
    public int b=113;
    public int c = 365;
    public int d = 120;

    public static int times = 1;
    /// <summary>
    /// 鼠标事件
    /// </summary>
    /// <param name="flags">事件类型</param>
    /// <param name="dx">x坐标值(0~65535)</param>
    /// <param name="dy">y坐标值(0~65535)</param>
    /// <param name="data">滚动值(120一个单位)</param>
    /// <param name="extraInfo">不支持</param>
    [DllImport("user32.dll")]
    static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

    /// <summary>
    /// 鼠标操作标志位集合
    /// </summary>
    [Flags]
    enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        /// <summary>
        /// 设置鼠标坐标为绝对位置（dx,dy）,否则为距离最后一次事件触发的相对位置
        /// </summary>
        Absolute = 0x8000
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("手动触发A键Down");
            DoMouseClick(a,b);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("模拟鼠标左键Down事件");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("模拟鼠标左键Up事件");
        }

        //print(Input.mousePosition);
        print(Screen.width + "     " + Screen.height);
    }

    /// <summary>
    /// 调用鼠标点击事件（传的参数x,y要在应用内才会调用鼠标事件）
    /// </summary>
    /// <param name="x">相对屏幕左上角的X轴坐标</param>
    /// <param name="y">相对屏幕左上角的Y轴坐标</param>
    private static void DoMouseClick(int x, int y)
    {
        int dx = (int)((double)x / Screen.width * 65535); //屏幕分辨率映射到0~65535(0xffff,即16位)之间
        int dy = (int)((double)y / Screen.height * 0xffff); //转换为double类型运算，否则值为0、1
        mouse_event(MouseEventFlag.Move | MouseEventFlag.LeftDown | MouseEventFlag.LeftUp | MouseEventFlag.Absolute, dx, dy, 0, new UIntPtr(0)); //点击
    }

    public void click()
    {
        print("success");
        if(times>0)
        DoMouseClick(a, b);
        times--;
    }
}

