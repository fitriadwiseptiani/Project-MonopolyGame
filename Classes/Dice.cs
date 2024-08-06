namespace MonopolyGame;

public class Dice : IDice
{
    private static Random _random = new Random();   //static = satu untuk semua, berbagi satu objek yang sama
    public int NumberOfSides { get; private set; }  //menyimpan jumlah sisi dadu
    public int[] SideValues { get; private set; }

    public Dice(int[] sideValues)
    {
        if(sideValues == null || sideValues.Length == 0){
            throw new ArgumentNullException("Pastikan nilai yang diberikan tidak null atau kosong");
        }  
        NumberOfSides = sideValues.Length;
        SideValues = sideValues;

    }
    public int Roll()
    {
        int index = _random.Next(0, NumberOfSides);
        return SideValues[index];
    }

    public int RollTwoDice(out int firstRoll, out int secondRoll, out int totalRoll)
    {
        firstRoll = Roll();
        secondRoll = Roll();
        totalRoll = firstRoll + secondRoll;
        return totalRoll;
    }

}
