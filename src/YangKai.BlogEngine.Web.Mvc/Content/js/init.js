var paceOptions;

paceOptions = {
  start: function() {
    return $('.pace-loadingbox').show();
  },
  stop: function() {
    return $('.pace-loadingbox').hide();
  }
};

$(document).ready(function() {
  SyntaxHighlighter.defaults['gutter'] = true;
  SyntaxHighlighter.defaults['collapse'] = false;
  SyntaxHighlighter.defaults['quick-code'] = false;
  return SyntaxHighlighter.all();
});
