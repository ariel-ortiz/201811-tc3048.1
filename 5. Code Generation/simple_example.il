// CIL example program.
//
// To assemble:
//                 ilasm add.il

.assembly 'example' { }

.class public 'Test' extends ['mscorlib']'System'.'Object' {
    .method public static void 'whatever'() {
        .entrypoint
        ldc.i8 2
        ldc.i8 40
        add
        call void class ['mscorlib']'System'.'Console'::'WriteLine'(int64)
        ret
    }
}

