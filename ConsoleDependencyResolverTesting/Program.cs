using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleDependencyResolverTesting
{
    class Program
    {
        static void Main ( string [ ] args )
        {
            //var newType = BuildDynamicType ( );
            //var ctors = newType.GetConstructors ( );
            //Console.WriteLine ( ctors [ 1 ].GetParameters ( ).Length );

            ////var newTypeItem = ctors [ 1 ].Invoke ( Type.EmptyTypes );
            //var newTypeItem = Activator.CreateInstance ( newType , new object [ ] { 9 , "string" } );
            //var methInfo = newType.GetMethod ( "MyMethod" );
            //var result = methInfo.Invoke ( newTypeItem , new object [ ] { 2 } );


            var result = ( int ) KindOfType.String;

            Console.WriteLine ( result );
            Console.ReadLine ( );
        }


        static Type BuildDynamicType ()
        {
            AssemblyName aName = new AssemblyName ( "DynamicAssemblyExample" );
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly ( aName , AssemblyBuilderAccess.Run );
            ModuleBuilder mb = ab.DefineDynamicModule ( aName.Name );
            TypeBuilder tb = mb.DefineType ( "MyDynamicType" , TypeAttributes.Public );

            FieldBuilder fbNumber = tb.DefineField ( "m_number" , typeof ( int ) , FieldAttributes.Private );

            Type [ ] parameterTypes = { typeof ( int ) , typeof ( string ) };
            ConstructorBuilder ctor1 = tb.DefineConstructor ( MethodAttributes.Public , CallingConventions.Standard , parameterTypes );
            ILGenerator ctor1IL = ctor1.GetILGenerator ( );
            ctor1IL.Emit ( OpCodes.Ldarg_0 );
            ctor1IL.Emit ( OpCodes.Call , typeof ( object ).GetConstructor ( Type.EmptyTypes ) );
            ctor1IL.Emit ( OpCodes.Ldarg_0 );
            ctor1IL.Emit ( OpCodes.Ldarg_1 );
            ctor1IL.Emit ( OpCodes.Stfld , fbNumber );
            ctor1IL.Emit ( OpCodes.Ret );

            ConstructorBuilder ctor0 = tb.DefineConstructor ( MethodAttributes.Public , CallingConventions.Standard , Type.EmptyTypes );
            ILGenerator ctor0IL = ctor0.GetILGenerator ( );
            ctor0IL.Emit ( OpCodes.Ldarg_0 );
            ctor0IL.Emit ( OpCodes.Ldc_I4_S , 42 );
            ctor0IL.Emit ( OpCodes.Call , ctor1 );
            ctor0IL.Emit ( OpCodes.Ret );

            PropertyBuilder pbNumber = tb.DefineProperty ( "Number" , PropertyAttributes.HasDefault , typeof ( int ) , null );
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            MethodBuilder mbNumberGetAccessor = tb.DefineMethod ( "get_Number" , getSetAttr , typeof ( int ) , Type.EmptyTypes );
            ILGenerator numberGetIL = mbNumberGetAccessor.GetILGenerator ( );
            numberGetIL.Emit ( OpCodes.Ldarg_0 );
            numberGetIL.Emit ( OpCodes.Ldfld , fbNumber );
            numberGetIL.Emit ( OpCodes.Ret );

            MethodBuilder mbNumberSetAccessor = tb.DefineMethod ( "set_Number" , getSetAttr , null , new Type [ ] { typeof ( int ) } );
            ILGenerator numberSetIL = mbNumberSetAccessor.GetILGenerator ( );
            numberSetIL.Emit ( OpCodes.Ldarg_0 );
            numberSetIL.Emit ( OpCodes.Ldarg_1 );
            numberSetIL.Emit ( OpCodes.Stfld , fbNumber );
            numberSetIL.Emit ( OpCodes.Ret );
            pbNumber.SetGetMethod ( mbNumberGetAccessor );
            pbNumber.SetSetMethod ( mbNumberSetAccessor );

            MethodBuilder meth = tb.DefineMethod ( "MyMethod" , MethodAttributes.Public , typeof ( int ) , new Type [ ] { typeof ( int ) } );
            ILGenerator methIL = meth.GetILGenerator ( );
            methIL.Emit ( OpCodes.Ldarg_0 );
            methIL.Emit ( OpCodes.Ldfld , fbNumber );
            methIL.Emit ( OpCodes.Ldarg_1 );
            methIL.Emit ( OpCodes.Mul );
            methIL.Emit ( OpCodes.Ret );

            return tb.CreateType ( );

        }

    }



    class Class2
    {
    }
}
