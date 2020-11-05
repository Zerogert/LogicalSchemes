using Logical_cxem.Enumerations;

namespace Logical_cxem
{
    public struct PointPin
    {
        public double X;
        public double Y;
        public EDirection Direction;
        public int IdParent;
        public int Id;
        public ETypePin Type;

        public PointPin(double x, double y, EDirection direction, ETypePin type, int parentid, int id)
        {
            X = x;
            Y = y;
            IdParent = parentid;
            Id = id;
            Type = type;
            Direction = direction;
        }
    }
}