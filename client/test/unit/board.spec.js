describe('BoardCtrl', function(){
  var scope,
  $httpBackend;

  beforeEach(angular.mock.module('app'));

  beforeEach(angular.mock.inject(function($rootScope, $controller, _$httpBackend_){
    scope = $rootScope.$new();
    $httpBackend = _$httpBackend_;
    $controller('BoardCtrl', {$scope: scope, messages:[{}, {}]});
  }));

  it('should load messages into list.', function(){
    expect(scope.list.length).toBe(2);
  });

  it('should init new message.', function(){
    expect(scope.entity).toBeDefined();
  });

  it('should add new message.', function(){
    scope.form ={$invalid:true};
    scope.entity.Author="tester";
    scope.entity.Content="tester";
    scope.save()
    expect(scope.list.length).toBe(3);
  });
});