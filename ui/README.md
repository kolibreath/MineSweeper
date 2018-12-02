# 开发过程问题备注

- 使用Nuget 作为包管理系统
[Nuget 包管理系统介绍](https://www.cnblogs.com/nizhenghua/p/6422078.html)

- C# 集合问题
[ArrayList](https://docs.microsoft.com/zh-cn/dotnet/api/system.collections.arraylist?view=netframework-4.7.2)
Task<> 泛型中貌似只会接受一个IList 的实现<br>
这个地方不能够田间ArrayList，因为ArrayList是不支持泛型的
- JSON 序列化和反序列化
[数据契约](https://blog.csdn.net/Percy__Lee/article/details/48286035)
[C# 进行序列化和反序列化实例](http://www.cnblogs.com/caofangsheng/p/5687994.html)

- 使用的网络请求框架
[Flurl.http](https://flurl.io/)

- POST GET 请求方式
https://www.cnblogs.com/Andy-Blog/p/5666180.html
