namespace Enigma.domain.extensions
{
    public static class CharExtensions
    {
        
        public static char GetNextCharacter(this char c) => c.GetCharacterWithOffset(1);
        public static int offset(this char c) => ((int) c) - 65;
        //assuming only upper case letters if it was Z for example return to A with 1 offset
        public static char GetCharacterWithOffset(this char c, int offset) => (offset < 0) ? c.GetNegativeCharacterWithOffset(offset) : c.GetPositiveCharacterWithOffset(offset);
        //assuming only upper case letters if it was Z for example return to A with 1 offset
        private static char GetPositiveCharacterWithOffset(this char c, int offset) => ((int) c) - 65 + offset >= 26 ? (char)((int)c + offset - 26) : (char)((int)c + offset);
        //assuming only upper case letters if it was A for example return to Z with -1 offset
        private static char GetNegativeCharacterWithOffset(this char c, int offset) => ((int) c) - 65 + offset < 0 ? (char)((int)c + offset + 26) : (char)((int)c + offset);
        
    }
}