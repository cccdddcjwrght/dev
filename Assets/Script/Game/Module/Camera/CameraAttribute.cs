using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/**@defgroup group100017 战斗系统-战斗相机-曾长付
 *-----------------------------
 *[作者:]{zcf}\n
 *[时间:]{2015年6月17日 16:11:13}\n
 *[模块描述:]{战斗相机系统--控制相机对象}\n
 *[类名:]CameraAttribute\n
 *-----------------------------
 *@{
*/
/// <summary>
/// 控制相机对象
/// </summary>
[System.Serializable]
public class CameraAttribute
{
    /// <summary>
    /// 绑定对象
    /// </summary>
    public GameObject bindTarget;
    /// <summary>
    /// 老对象
    /// </summary>
    public GameObject oldTarget;
    /// <summary>
    /// 开始点
    /// </summary>
    public Vector3 startPoint;
    /// <summary>
    /// 相机
    /// </summary>
    public Camera camera;
    /// <summary>
    /// 是否跟随坐标
    /// </summary>
    public bool isFollowPoint = true;
    /// <summary>
    /// 是否跟随旋转
    /// </summary>
    public bool isFollowRotation = true;
    /// <summary>
    /// 是否看向对象
    /// </summary>
    public bool isLookAt = true;
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float rotationSpeed;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;
    private Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private Vector3 position = Vector3.zero;

    /// <summary>
    /// 设置绑定对象
    /// </summary>
    /// <param name="theObject"></param>
    /// <param name="apply"></param>
    public void SetTarget(GameObject theObject, bool apply = false)
    {
        if (bindTarget == theObject && !apply) return;
        oldTarget = bindTarget;
        bindTarget = theObject;
        startPoint = bindTarget.transform.position;
        rotation = Quaternion.Euler(0, 0, 0);
        position = Vector3.zero;
    }

    /// <summary>
    /// 设置旋转
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTargetRotation(Quaternion rotation)
    {
        this.rotation = rotation;
    }

    /// <summary>
    /// 设置坐标点
    /// </summary>
    /// <param name="position"></param>
    public void SetTargetPosition(Vector3 position)
    {
        this.position = position;
        this.startPoint = position;
    }

    /// <summary>
    /// 获取坐标点
    /// </summary>
    /// <param name="isStartPoint"></param>
    /// <returns></returns>
    public Vector3 GetPosition(bool isStartPoint = false)
    {
        if (isStartPoint) return startPoint;
        if (bindTarget == null || position != Vector3.zero) return position;
        return bindTarget.transform.position;
    }

    /// <summary>
    /// 获取旋转
    /// </summary>
    /// <returns></returns>
    public Quaternion GetRotation()
    {
        if (bindTarget == null || rotation != Quaternion.Euler(Vector3.zero)) return rotation;
        return bindTarget.transform.rotation;

    }

    public void Reset()
    {
        if (bindTarget != null)
            SetTarget(bindTarget, true);
    }
} 
//@}
