using System.Runtime.CompilerServices;
using System;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;

using System.Text.RegularExpressions;
    /// <summary>
    /// this class contains the initialization of levels data 
    /// this class is called on the first initialization of the game and don't use any more
class LevelData{

    public  LevelData(){
   
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string[] capitals = Regex.Split(alphabet, "(?<=\\G.)");
    for (int i  = 0;   i< capitals.Length;   i++){
            
            this.SourceLevels[i]=new LevelDiscription(capitals[i],"Level_"+capitals[i],true);
        }
    }

    public LevelDiscription[] SourceLevels=new LevelDiscription[28];
}