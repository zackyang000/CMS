此为 http://www.woshinidezhu.com 网站源码。  

基于领域驱动设计(Based Domain-Driven Design)的类博客系统，将逐步改进为CMS。

1)使用演示以下.NET技术：
ASP.NET MVC 4
Microsoft Entity Framework 5.0 Code First
Microsoft Patterns & Practices Unity Application Block
Microsoft Patterns & Practices Policy Injection Application Block

2)开发环境：
Visual Studio 2010 Professional/Ultimate with SP1
ASP.NET MVC4 (下载地址:http://www.microsoft.com/zh-cn/download/details.aspx?id=30683)
IIS Express 7.5 (下载地址:http://www.microsoft.com/zh-cn/download/details.aspx?id=1038)
或
Visual Studio 2012 Ultimate

3)启动:
1.使用Visual Studio打开解决方案。
2.使用Nuget获取丢失的Packages。
2.修改YangKai.BlogEngine.Web.Mvc项目下web.config文件中数据库链接字符串。(采用SQL Server 2005或以上)
3.编译运行,程序将自动创建数据库并插入测试数据。