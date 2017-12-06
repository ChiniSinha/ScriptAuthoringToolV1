
public static class CrapiVisemeMap
{
    public static int MapViseme(int inputViseme)
    {
        switch (inputViseme)
        {
            case 1: // A
                return 2;
            case 2: // B
                return 21;
            case 3: // E
                return 6;
            case 4: // F
                return 18;
            case 5: // K
                return 20;
            case 6: // O
                return 8;
            case 7: // OOO
                return 10;
            case 8: // R
                return 13;
            case 9: // TH
                return 17;
        }
        return 0;
    }
}
