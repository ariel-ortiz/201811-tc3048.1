calc:  calc_lex.l calc.y
	bison --defines=calc_tokens.h -o calc.c calc.y
	flex -o calc_lex.c calc_lex.l
	gcc -o calc calc.c calc_lex.c -lfl -lm
	
clean: 
	rm calc calc.c calc_lex.c calc_tokens.h
