# Mine Sweeper 进度情况/想法池！

- [x] 注册/登录UI
- [x] 加一下闪屏 默认的闪屏太丑了
- [x] 保存已经登录过的用户的信息
- [ ] 人机对战！
- [ ] 背景音乐？ 和一些音效？
- [x] 用户主界面
- [ ] 时间:用户开始点击第一个方格的时候，就开始游戏记时
- [x] 用户可以通过注册账号 保存自己的游戏记录
- [x] 网络请求？
- [ ] 挑战某个玩家


> 核心玩法：
- 基本操作
鼠标左键选择揭开这块地板，观察有没有地雷<br>
鼠标右键第一次选择标注(插旗子)<br>
鼠标右键第二次显示❓<br>
鼠标右键第三次操作显示空白(回复原来的地板样式)

- Arcade
初始化 16*16的方格，有 easy normal hard 模式<br>
游戏中： 记录用户的扫雷时间 <br>
游戏结束：显示用户的游戏时间/扫到几颗地雷/展示游戏版<br>
(胜利)：用户上传分数 /可以在这里查看排名情况(失败):是否重玩这一局游戏？<br>
展示游戏版： 展示用户扫雷情况，标出了几颗地雷
根据三个类型上传用户的分数 进行排名

- Combat
>这里说的邮件不是广义上的邮件，作用类似一条消息

游戏进入页面显示排名top10的玩家消息<br>
表单右侧有邮件选项，可以通过邮件约战！<br>

发起约战和收到约战的晚间将会在游戏主页面的头像显示有约战，可以选择拒绝，拒绝的话，游戏将会发送一封右键返回挑战者<br>
选择同意的话，游戏将会使用两个相同游戏场景，发送给两个玩家，任意玩家完成的话都会通过邮件方式发送给另外一个玩家完成的情况。

# UI组
使用C# 开发
## 参考内容
[官方文档](https://docs.microsoft.com/zh-cn/windows/uwp/get-started/your-first-app)
[入门中文翻译视频](https://zhuanlan.zhihu.com/p/20364660)
根据文档可以下载相关依赖 熟悉设计风格 基本控件使用

书籍： C# 图解教程

# 后端组
使用python flask 框架开发后端.


# git 工作流
[git 入门教程](https://git-scm.com/book/zh/v1/Git-%E5%9F%BA%E7%A1%80)

- ui开发文件放在ui路径下
- 后端开发文件放在backend路径下

# UI图标
- 网盘链接 https://pan.baidu.com/s/18J7mMFCZ_hT4eBSn-ENuQg
- filed为游戏开始时默认的土地
- monster为“炸弹”
- nomal中有gift和tree两种，作为不同的得分（gift*2，tree*1）
- people为默认头像
- flashScreen为闪屏
- easy，medium，hard为选择难度等级的图标

# 音效
- music目录
- game为游戏进行时音效
- begin为进入游戏时音效，直到开始了游戏才停止
- click为点击雪地音效
- fail为失败音效（1,2,3）
- message为收到邀请信息的音效
- victory为成功音效（1,2）

