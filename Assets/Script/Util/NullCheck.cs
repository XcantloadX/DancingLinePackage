using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 用来检查字段是否为空
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class NullCheck : Attribute
{
    /// <summary>
    /// 如果字段为 null，是否抛异常
    /// </summary>
    public bool IsErr { get; set; }

    /// <summary>
    /// 字段为空时的提示信息
    /// </summary>
    public string Message { get; set; }

    #region 构造函数
    /// <summary>
    /// 检查该字段是否为空
    /// </summary>
    public NullCheck()
    {
        this.IsErr = true;
        this.Message = null;
    }

    /// <summary>
    /// 检查该字段是否为空
    /// </summary>
    /// <param name="isErr">如果字段为 null，是否抛异常</param>
    public NullCheck(bool isErr)
    {
        this.IsErr = isErr;
    }

    /// <summary>
    /// 检查该字段是否为空
    /// </summary>
    /// <param name="isErr">如果字段为 null，是否抛异常</param>
    /// <param name="message">字段为空时的提示信息</param>
    public NullCheck(bool isErr, string message)
    {
        this.IsErr = isErr;
        this.Message = message;
    }
    #endregion

    /// <summary>
    /// 检查实例的指定字段是否为空
    /// </summary>
    /// <param name="obj">要检查的实例</param>
    public static void Check(object obj)
    {
        //确保 obj 不为空
        if (obj == null)
            throw new ArgumentNullException("obj");

        //获取字段
        Type t = obj.GetType();
        FieldInfo[] infos = t.GetFields();


        //遍历字段
        for (int i = 0; i < infos.Length; i++)
        {
            //遍历特性
            object[] attributes = infos[i].GetCustomAttributes(typeof(NullCheck), false);
            for (int j = 0; j < attributes.Length; j++)
            {
                //如果在该字段中找到 NullCheck
                if (attributes[j].GetType() == typeof(NullCheck))
                {
                    object value = infos[i].GetValue(obj); //获取字段的值
                    if (value == null)
                    {
                        NullCheck ins = (NullCheck)attributes[j];
                        string msg = string.IsNullOrEmpty(ins.Message) ? string.Format("\"{0}\" is empty at {1}.", infos[i].Name, t.Name) : ins.Message;

                        //报错或提示
                        if (ins.IsErr)
                            throw new Exception(msg);
                        else
                            Debug.LogWarning(msg);

                    }
                    break;
                }

            }
        }


    }
}
