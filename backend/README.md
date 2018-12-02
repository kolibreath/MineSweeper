# 给后端同学写的可能的文档

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
    email:"734780178@qq.com" //邮件地址 格式：String
    password:""              //密码 格式:String
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
    msg:"7注册成功" // 返回值 
}

```


# 发送"邮件"

url| header | methods  
---|--------|-------- 
/send/ | Content-Type:application/json |  POST


