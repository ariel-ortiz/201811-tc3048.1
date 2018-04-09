all: fook1.exe fook2

fook1.exe: fook.cs
	mcs -out:fook1.exe fook.cs

fook2: fook_lex.l fook.y
	bison --defines=fook_tokens.h -o fook.c fook.y
	flex -o fook_lex.c fook_lex.l
	gcc -o fook2 fook.c fook_lex.c -lfl

clean:
	rm fook1.exe fook2 fook.c fook_lex.c fook_tokens.h
