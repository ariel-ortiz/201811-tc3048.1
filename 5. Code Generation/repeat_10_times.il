.assembly extern mscorlib
{
  .ver 4:0:0:0
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 ) // .z\V.4..
}
.assembly 'repeat_10_times'
{
  .custom instance void class [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::'.ctor'() =  (
		01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
		63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01       ) // ceptionThrows.

  .hash algorithm 0x00008004
  .ver  0:0:0:0
}
.module repeat_10_times.exe // GUID = {6F638107-12D2-4452-B563-522740573D1E}


  .class public auto ansi beforefieldinit Repeat10Times
  	extends [mscorlib]System.Object
  {

    // method line 1
    .method public hidebysig specialname rtspecialname 
           instance default void '.ctor' ()  cil managed 
    {
        // Method begins at RVA 0x2050
	// Code size 7 (0x7)
	.maxstack 8
	IL_0000:  ldarg.0 
	IL_0001:  call instance void object::'.ctor'()
	IL_0006:  ret 
    } // end of method Repeat10Times::.ctor

    // method line 2
    .method public static hidebysig 
           default void Main ()  cil managed 
    {
        // Method begins at RVA 0x2058
	.entrypoint
	// Code size 36 (0x24)
	.maxstack 2
	.locals init (
		int32	V_0)
	IL_0000:  ldc.i4.0 
	IL_0001:  stloc.0 
	IL_0002:  br IL_001b

	IL_0007:  ldloc.0 
	IL_0008:  call void class [mscorlib]System.Console::Write(int32)
	IL_000d:  ldstr " Hello"
	IL_0012:  call void class [mscorlib]System.Console::WriteLine(string)
	IL_0017:  ldloc.0 
	IL_0018:  ldc.i4.1 
	IL_0019:  add 
	IL_001a:  stloc.0 
	IL_001b:  ldloc.0 
	IL_001c:  ldc.i4.s 0x0a
	IL_001e:  blt IL_0007

	IL_0023:  ret 
    } // end of method Repeat10Times::Main

  } // end of class Repeat10Times

