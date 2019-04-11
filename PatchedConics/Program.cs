using Kelson.Physics;
using static Kelson.Physics.Unit;
using static System.Console;

#region .
class Program
{
    static void Main(string[] args)
    {
            #endregion

var start = (Scalar2)((3, Meters), (4, Meters));
var velocity = (Scalar2)((2, Meters / Seconds), (-0.2, Feet / Seconds));
var time = (Scalar)(5, Seconds);
var end = start + (velocity * time);

WriteLine($"{start} + ({velocity}) for {time}) = {end}");

#region .
    }
}

#endregion
