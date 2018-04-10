// CIL example program.
//
// To assemble:
//                 ilasm add.il

.assembly 'example' { }

.class public 'Test' extends ['mscorlib']'System'.'Object' {
    .method public static void 'whatever'() {
        .entrypoint
	.locals init (int32 'i')
	ldc.i4 0
	stloc  'i'
'start_loop':
	ldloc  'i'
	ldc.i4 10
	bge    'end_loop'
        ldloc  'i'
	call void class ['mscorlib']'System'.'Console'::'WriteLine'(int32)
        ldloc  'i'
	ldc.i4 1
        add
	stloc  'i'
	br     'start_loop'
'end_loop':
        ret
    }
}

