%{

//==========================================================
// Solution de problem 2.
//==========================================================

#include "fook_tokens.h"

void yyerror(char *s, ...);

%}

%%

[a-z] {
        yylval = yytext[0];
        return SYMBOL;
}
"+"   { return SUCCESSOR; }
"-"   { return PREDECESSOR; }
"{"   { return MAX_LEFT; }
"}"   { return MAX_RIGHT; }
","   { return COMMA; }
\n    { return EOL; }
[ \t] { /* ignore whitespace */ }
.     { return ILLEGAL; }

%%
