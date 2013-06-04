'use strict';

/* http://docs.angularjs.org/guide/dev_guide.e2e-testing */


describe('my app', function() {

  describe('文章列表页面', function() {
    it('文章列表能被显示.', function() {
        browser().navigateTo('/#!/list/funs');
        expect(repeater('#posts article').count()).toBeGreaterThan(0);
    });
  });
    
  describe('文章明细页面', function () {
      beforeEach(function () {
          browser().navigateTo('/#!/post/asp-net-mvc2-to-mvc3-rc');
      });
      
      it('评论功能能正常使用.', function () {
          browser().navigateTo('/#!/post/asp-net-mvc2-to-mvc3-rc');
          var content = '这是一条用于测试的评论.(' + new Date().toLocaleString() + ")";
          input('entity.Author').enter('Tester');
          input('entity.Email').enter('');
          input('entity.Content').enter(content);
          element(':button').click();
          expect(element('.comment li:last-child div.text div p').text()).toEqual(content);
      });
  });
});
