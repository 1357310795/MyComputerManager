![title.png](https://s2.loli.net/2022/07/07/o9rWHAm6fiZS4pQ.png)

## 背景
国内流氓软件经常为了某些目的无所不用其极，竟然想到通过Shell Extension在“此电脑”里面塞快捷方式，用户无法轻易删除。除了在这些流氓软件本身的设置里取消这个快捷方式，还有没有更优雅的办法？百度给出的答案无一例外都是修改注册表，这对于电脑小白极不友好，又非常危险。万一误删了系统关键条目，麻烦可就大了。

于是，我萌生了开发这个小工具的念头。4天时间，查了大量资料，终于把这个写完了，又弥补了一片空白！

## 功能介绍
![intro-p1.png](https://s2.loli.net/2022/07/07/IULqP6W3crB4Vxk.png)
![intro-p3.png](https://s2.loli.net/2022/07/07/49FYxJUWj5l6Z8a.png)
![intro-p2.png](https://s2.loli.net/2022/07/07/mC9lh4SvHUIrD1W.png)

## 使用方法
在[Github Releases](https://github.com/1357310795/MyComputerManager/releases)下载最新版程序，双击直接运行

## 开发者相关
项目基于 .NET Framework 4.7.2 开发（为了兼容性就用老版本啦😓），又是一个极好的 WPF 学习材料。程序涉及到了：
- 自定义控件（基于xaml/基于cs代码）
- 重写控件样式
- 数据绑定（绑定到其他控件/DataContext，设置RelativeSource）
- Mvvm模式（PropertyChanged/Command，DataTemplate）
- 附加事件+控件行为（Microsoft.Xaml.Behaviors库）
- 异步方法
- 依赖注入（Dependency Injection）模式
- 页面导航

## 开源许可
本程序通过 GNU General Public License v3.0 许可在 [GitHub](https://github.com/1357310795/MyComputerManager) 开源，如果您觉得软件好用，请不要吝惜您的 Star 哦，这会对我有非常大的帮助！

## 致谢
感谢 @lepoco 的 [wpf-ui](https://github.com/lepoco/wpfui) 项目，Win11风格的控件来自于此。

感谢 @walterlv 和 @XIU2 [在 TileTool 下的讨论](https://github.com/XIU2/TileTool/pull/4)，本程序部分 UI 设计参考了这里。
