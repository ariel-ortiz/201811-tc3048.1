//==========================================================
// Type your name and student ID here.
//==========================================================

%{

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int yylex(void);
void yyerror(char *s, ...);

%}

/* Declare tokens */
%token SYMBOL EOL ILLEGAL

%%

fooklist:
    /* nothing */ { } /* matches at beginning of input */
    | fooklist expr EOL { printf("%c\n> ", $2 + 'a'); } /* EOL is end of an expression */
;

expr:
    SYMBOL
;

%%

int main(int argc, char **argv) {
    printf("> ");
    yyparse();
    return 0;
}

void yyerror(char * s, ...) {
    fprintf(stderr, "Syntax Error!\n");
    exit(1);
}
