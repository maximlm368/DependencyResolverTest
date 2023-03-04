using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDependencyResolverTesting
{
    static class RandomIntGenerator
    {
        private static Random _generator;


        static RandomIntGenerator ()
        {
            _generator = new Random ( );
        }


        public static int GetIntBetween ( int lowestValue , int highestValue )
        {
            return _generator.Next ( lowestValue , highestValue );
        }

    }
}
