# 给后端同学写的可能的文档

> 大小写什么的可能需要注意一下 不知道flurl.http 是怎么解析json的

# 登录
url| header | methods  
---|--------|-------- 
/login/ | Content-Type:application/json |  POST

## URL Params  
```
no params 
```

## POST DATA:  
```
{
    Email:"734780178@qq.com" //邮件地址 格式：String
    Password:""              //密码 格式:String
}

```

## RETURN DATA:  

```
{
    Msg:"登陆成功 / 并没有登录成功",
    Code:200(200 == 成功/ 400 ==  失败)
}
```

# 注册
url| header | methods  
---|--------|-------- 
/signin/ | Content-Type:application/json |  POST

## URL Params  
```
no params 
```
## POST DATA:  
```
{
    Email:"734780178@qq.com" //邮件地址 格式：String
    Password:""              //密码 格式:String
}

```


## RETURN DATA:  

```
{
    Msg:"注册成功/并没有注册陈工"  
    Code:200/400 //int
}

```

# 获取前三名玩家！


url| header | methods  
---|--------|-------- 
/topthree/ | Content-Type:application/json |  GET

## URL Params  
```
no params
```
## RETURN DATA:  

到时候现在数据库里面初始化三个玩家好了！
```
[
    {
        Email:"习近平",
        NickName:"习主席",
        Score:100
    },
    {
        Email:"江泽民",
        NickName:"前国家主席",
        Score:99
    },
     {
        Email:"邓小平",
        NickName:"改革开放设计师",
        Score:99
    }
    
]
```


# 挑战某人

> 注意 这是一个GET 请求！

url| header | methods  
---|--------|-------- 
/challenge/ | Content-Type:application/json |  GET

## URL Params  
```
?email=xxxx 
```
## RETURN DATA:  

```
{
    Msg:"挑战成功/并没有注册成功"  
    Code:200/400 //int
}

```

# 是否收到别人的挑战？


url| header | methods  
---|--------|-------- 
/status/ | Content-Type:application/json |  GET

## URL Params  
```
no params
```
## RETURN DATA:  

```
{
    Msg:"收到挑战 / 并没有收到挑战",
    Code:200(200 == 收到挑战 / 404 ==  没有挑战)
}
```