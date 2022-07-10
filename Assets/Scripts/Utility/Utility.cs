using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
  public static T GetObjectFromJson<T>(string path)
  {
    string prefix = Const.DATABASE_PREFIX;
    using (var reader = new StreamReader(prefix + path))
    {
      string json = reader.ReadToEnd();
      T obj = JsonUtility.FromJson<T>(json);
      return obj;
    }
  }

  public static T ParseToEnum<T>(string value)
  {
    T result = (T)Enum.Parse(typeof(T),value, true);
    return result;
  }
}
