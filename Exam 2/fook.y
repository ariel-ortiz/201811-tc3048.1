%{

//==========================================================
// Solution de problem 2.
//==========================================================


#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int yylex(void);
void yyerror(char *s, ...);

%}

/* Declare tokens */
%token SYMBOL SUCCESSOR PREDECESSOR MAX_LEFT MAX_RIGHT
%token COMMA EOL ILLEGAL

%%

fooklist:
    /* nothing */ { } /* matches at beginning of input */
    | fooklist exp EOL { printf("%c\n> ", $2); } /* EOL is end of an expression */
;

exp:
    SYMBOL
    | exp SUCCESSOR              { $$ = $1 == 'z' ? 'a' : $1 + 1; }
    | exp PREDECESSOR            { $$ = $1 == 'a' ? 'z' : $1 - 1; }
    | MAX_LEFT maxlist MAX_RIGHT { $$ = $2; }
;

maxlist:
    exp
    | maxlist COMMA exp { $$ = $1 > $3 ? $1 : $3; }

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