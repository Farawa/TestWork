public class EndMessage : Message
{
    public int points;

    public EndMessage(int pointsCount)
    {
        points = pointsCount;
    }
}
