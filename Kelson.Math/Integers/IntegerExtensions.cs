namespace Kelson.Math.Integers
{
    public static class IntegerExtensions
    {
        public static int ISqrt(this int of)
        {                        
            int result = 0;
            int one = 1 << 30; 
            
            while (one > of)            
                one >>= 2;            

            while (one != 0)
            {
                if (of >= result + one)
                {
                    of = of - (result + one);
                    result = result + 2 * one;
                }
                result >>= 1;
                one >>= 2;
            }
            return result;            
        }
    }
}
