using System.Collections.Generic;

namespace RomanNumerals
{
    public sealed class RomanCharacter
    {
        //"enum" elements are static:
        public static RomanCharacter I { get; private set; }
        public static RomanCharacter V { get; private set; }
        public static RomanCharacter X { get; private set; }
        public static RomanCharacter L { get; private set; }
        public static RomanCharacter C { get; private set; }
        public static RomanCharacter D { get; private set; }
        public static RomanCharacter M { get; private set; }

        //values are non-static
        public char Character { get; private set; }
        public int Value { get; private set; }
        public RomanCharacter Decrementor { get; private set; }
        public RomanCharacter FiveOfThese { get; private set; }
        public RomanCharacter TenOfThese { get; private set; }

        public int MaxSequential
        {
            get
            {
                if (Character == 'I') return 3;
                return Value / Decrementor.Value == 10 ? 3 : 1;
            }
        }
        private RomanCharacter()
        {
            //no op
        }
        
        static RomanCharacter()
        {
            Init();
        }

        private static void Init()
        {
            I = new RomanCharacter { Character = 'I', Value = 1,    Decrementor = null };
            V = new RomanCharacter { Character = 'V', Value = 5,    Decrementor = I    };
            X = new RomanCharacter { Character = 'X', Value = 10,   Decrementor = I    };
            L = new RomanCharacter { Character = 'L', Value = 50,   Decrementor = X    };
            C = new RomanCharacter { Character = 'C', Value = 100,  Decrementor = X    };
            D = new RomanCharacter { Character = 'D', Value = 500,  Decrementor = C    };
            M = new RomanCharacter { Character = 'M', Value = 1000, Decrementor = C    };

            I.FiveOfThese = V;
            I.TenOfThese = X;
            X.FiveOfThese = L;
            X.TenOfThese = C;
            C.FiveOfThese = D;
            C.TenOfThese = M;

            Symbols = new Dictionary<char, RomanCharacter>
                          {
                              {I.Character, I},
                              {V.Character, V},
                              {X.Character, X},
                              {L.Character, L},
                              {C.Character, C},
                              {D.Character, D},
                              {M.Character, M}
                          };
        }

        public static Dictionary<char, RomanCharacter> Symbols { get; private set;}
    }
}