Blog System
===========

####<a href="http://www.woshinidezhu.com">Live Demo</a>

## Why?
For research.

## What?
It's a Blog System use the following techniques build:

Front-end: AngularJS(CoffeeScript)+Bootstrap

Back-end: ASP.NET WEB API 2(OData 5.0)+Entity Framework 6

## How?

1.Open the solution using Visual Studio 2012.2+

2.Use nuget get lost packages (Web Api & OData library need use <a href='http://aspnetwebstack.codeplex.com/wikipage?title=Use%20Nightly%20Builds'>Nightly Builds</a>)

2.Modify the `ConnectionString` at `web.config` under Mvc project (SQL2005+)

3.Compile and run, it will automatically create the database and insert test data.

## License

base MIT.

