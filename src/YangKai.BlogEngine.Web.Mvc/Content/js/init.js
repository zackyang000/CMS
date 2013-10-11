var paceOptions;

paceOptions = {
  start: function() {
    return $('.pace-loadingbox').show();
  },
  stop: function() {
    return $('.pace-loadingbox').hide();
  }
};
