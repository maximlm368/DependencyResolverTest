using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDependencyResolverTesting
{
    class DesigningNode
    {
        private static int _minChildCount;
        private static int _maxChildCount;
        private DesigningNode _parent;
        private List<DesigningNode> _children;
        private KindOfType _kindOfType;
        public bool _isChildless;


        static DesigningNode ( )
        {
            _minChildCount = 0;
            _maxChildCount = 5;
        }


        public DesigningNode ( DesigningNode parent )
        {
            _parent = parent;
            _children = new List<DesigningNode> ( );
            _kindOfType = KindOfType.Sealed;
        }


        public List<DesigningNode> DefineChildren ()
        {
            var children = new List<DesigningNode> ( );

            if ( ! _isChildless )
            {
                var childCount = RandomIntGenerator.GetIntBetween ( _minChildCount , _maxChildCount );

                for ( int i = 0;    i < childCount;    i++ )
                {
                    var child = new DesigningNode ( this );
                    children.Add ( child );
                }

                _children = children;
            }

            return children;
        }


        public void ResetKind ( KindOfType neededKind )
        {
            _kindOfType = neededKind;
        }

    }
}
