namespace Orbitaldrop.Cyberelegans
{
    public class Connector
    {
        public int status { get; set; }
        public MassPoint P1 { get; set; }
        public MassPoint P2 { get; set; }

        public Connector(MassPoint p1, MassPoint p2)
        {
            status = 1;
            this.P1 = p1;
            this.P2 = p2;
        }
    }
}