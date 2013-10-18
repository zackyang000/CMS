paceOptions =
  start: -> $('.pace-loadingbox').show(),
  stop: -> $('.pace-loadingbox').hide()

$(document).ready ->
  #代码高亮
  SyntaxHighlighter.defaults['gutter'] = true;
  SyntaxHighlighter.defaults['collapse'] = false;
  SyntaxHighlighter.defaults['quick-code'] = false;
  SyntaxHighlighter.all();