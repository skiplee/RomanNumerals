namespace RomanNumerals
{
    class RomanCharacter
    {
        public string Character { get; set; }
        public int Value { get; set; }
        public int DecrementValue { get; set; }
        public int MaxSequential 
        { 
            get 
            {
                if (DecrementValue == 0) return 3;
                return Value/DecrementValue == 10 ? 3 : 1; 
            }
        }
    }
}