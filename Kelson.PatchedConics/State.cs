namespace Kelson.PatchedConics
{
    // A position/velocity pair
    public readonly struct State
    {
        public readonly double X;
        public readonly double Y;
        public readonly double dX;        
        public readonly double dY;        

        public State(double x, double y, double dx, double dy) =>
            (X, Y, dX, dY) = (x, y, dx, dy);

        public State(StateRef state) =>
            (X, Y, dX, dY) = state;

        public void Deconstruct(out double x, out double y, out double dx, out double dy)
        {
            x = X; y = Y;
            dx = dX; dy = dY;
        }

        public State WithPosition(double x, double y) =>
            new State(x, y, dX, dY);

        public State WithVelocity(double dx, double dy) =>
            new State(X, Y, dx, dy);

        public StateRef AsRefStruct() => new StateRef(in this);

        public readonly struct StateRef
        {
            public readonly double X;
            public readonly double Y;
            public readonly double dX;
            public readonly double dY;

            public StateRef(double x, double y, double dx, double dy) =>
                (X, Y, dX, dY) = (x, y, dx, dy);

            public StateRef(in State state) =>
                (X, Y, dX, dY) = state;

            public void Deconstruct(out double x, out double y, out double dx, out double dy)
            {
                x = X; y = Y;
                dx = dX; dy = dY;
            }

            public StateRef WithPosition(double x, double y) =>
            new StateRef(x, y, dX, dY);

            public StateRef WithVelocity(double dx, double dy) =>
                new StateRef(X, Y, dx, dy);

            public static implicit operator State(StateRef state) => new State(state);
        }
    }
}
