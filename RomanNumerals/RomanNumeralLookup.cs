using System;
using System.Collections.Generic;

namespace RomanNumerals
{
    //sealed class RomanNumeralLookup
    //{
    //    private Dictionary<string, RomanCharacter> _romanCharacters;
    //    private static volatile RomanNumeralLookup _instance;
    //    private static object syncRoot = new Object();
        
    //    private RomanNumeralLookup()
    //    {
    //        _romanCharacters = new Dictionary<String, RomanCharacter>();
    //        _romanCharacters.Add("I", new RomanCharacter { Character = "I", Value = 1,    Decrementor = 0   });
    //        _romanCharacters.Add("V", new RomanCharacter { Character = "V", Value = 5,    Decrementor = 1   });
    //        _romanCharacters.Add("X", new RomanCharacter { Character = "X", Value = 10,   Decrementor = 1   });
    //        _romanCharacters.Add("L", new RomanCharacter { Character = "L", Value = 50,   Decrementor = 10  });
    //        _romanCharacters.Add("C", new RomanCharacter { Character = "C", Value = 100,  Decrementor = 10  });
    //        _romanCharacters.Add("D", new RomanCharacter { Character = "D", Value = 500,  Decrementor = 100 });
    //        _romanCharacters.Add("M", new RomanCharacter { Character = "M", Value = 1000, Decrementor = 100 });
    //    }

    //    public static RomanNumeralLookup Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //            {
    //                lock (syncRoot)
    //                {
    //                    if (_instance == null)
    //                        _instance = new RomanNumeralLookup();
    //                }
    //            }
    //            return _instance;
    //        }
    //    }

    //    public Dictionary<String, RomanCharacter> RomanCharacter
    //    {
    //        get { return _romanCharacters; }
    //    } 
    //}
}