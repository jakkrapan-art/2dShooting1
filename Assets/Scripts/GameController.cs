using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private static GameController _instance;
  public static GameController GetInstance
  {
    get
    {
      if (!_instance) _instance = new GameController();
      return _instance;
    }
  }

  void Awake()
  {
    WeaponStats.Setup();
    DontDestroyOnLoad(gameObject);
  }
}
