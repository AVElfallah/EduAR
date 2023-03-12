using System;
using System.IO;
using UnityEngine;

[Serializable]
class LevelDiscription{
    /// <summary>
    /// class created to discripe the level
    ///variable [name] is the name of the level 
    /// variable [path] is the level scene path
    /// variable [enabled] to determine player is allowed to access this level or not
    /// </summary>
  public  String name ;
  public  String path;
    
  public  bool enabled;

    public LevelDiscription(String name, String path, bool enabled){
        this.name = name;
        this.path = path;
        this.enabled = enabled;
        
    }

string ToJson ()=> JsonUtility.ToJson(this);

public static LevelDiscription FromJson(string json)=> JsonUtility.FromJson<LevelDiscription>(json);
    
}