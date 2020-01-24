using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static void CheckNull(object obj, string msg)
    {
        if (obj == null)
            throw new Exception(msg);
    }
}
