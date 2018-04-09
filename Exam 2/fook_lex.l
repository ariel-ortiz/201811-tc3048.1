%{

#include "fook_tokens.h"

void yyerror(char *s, ...);

%}

%%

[a-z] { return SYMBOL; }
\n    { return EOL; }
.     { return ILLEGAL; }

%%
