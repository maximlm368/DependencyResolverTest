using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDependencyResolverTesting
{
    class Tree
    {
        private DesigningNode _root;
        private List<DesigningNode> _nodes;
        private List<DesigningNode> _childlesses;
        private int _deepestLevel;
        private int _depth;


        public Tree ( int depth )
        {
            _nodes = new List<DesigningNode> ( );
            _childlesses = new List<DesigningNode> ( );
            _depth = depth;
        }


        private void BuildYourself ( )
        {
            var currentLevel = new List<DesigningNode> ( );
            currentLevel.Add ( _root );
            int levelCounter = 0;

            while ( true )
            {
                SetChildless ( currentLevel , levelCounter );
                var childLevel = new List<DesigningNode> ( );
                levelCounter++;

                for ( var i = 0;    i < currentLevel.Count;    i++ )
                {
                    var beingProcessedNode = currentLevel [ i ];
                    var children = beingProcessedNode.DefineChildren ( );
                    childLevel.AddRange ( children );
                    _nodes.AddRange ( children );
                }

                var timeToBreak = ( childLevel.Count < 1 )    ||    ( levelCounter == _depth );

                if ( childLevel.Count < 1 )
                {
                    break;
                }

                currentLevel = childLevel;
                _deepestLevel = levelCounter;
            }
        }


        private void SetChildless ( List<DesigningNode> range , int numberOfLevel )
        {
            if ( numberOfLevel < _depth )
            {
                int preferedCountOfChildless = range.Count / 2;
                var max = range.Count;
                var min = 0;
                
                for ( int i = 0;    i < preferedCountOfChildless;    i++ )
                {
                    var selected = RandomIntGenerator.GetIntBetween ( min , max );
                    _childlesses.Add ( range [ selected ] );
                    range [ selected ]._isChildless = true;
                }
            }
            else if ( numberOfLevel == _depth )
            {
                for ( int i = 0;    i < range.Count;    i++ )
                {
                    _childlesses.Add ( range [ i ] );
                    range [ i ]._isChildless = true;
                }
            }
        }


        private void DistributeKindsToChildlesses ( KindOfType kindForSetting , List<DesigningNode> range , int preferedCountForSetting )
        {
            
        }


        private void DistributeKindsToHavingChildren ( KindOfType kindForSetting , List<DesigningNode> range , int preferedCountForSetting )
        {

        }


        //private void SetKind ( List<DesigningNode> range , int preferedCountForSetting )
        //{
        //    SetKind ( KindOfType.Interface , range , preferedCountForSetting );
        //}


        //private void SetGeneric ( List<DesigningNode> range , int preferedCountForSetting )
        //{
        //    SetKind ( KindOfType.Generic , range , preferedCountForSetting );
        //}

    }
}
