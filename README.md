# LOLGameMate

作者: mkx  
日期: 2025-09-16

## 简介
一个合规的 Windows 游戏助手示例：
- 全局热键（默认 Alt+1）
- 简单 UI 配置用户名/密码
- 合规的键盘输入封装（不包含任何规避检测或驱动级实现）
- 凭证安全存储（DPAPI 加密，用户作用域）

## 使用
1. 启动程序，在主界面输入用户名与密码，点击“保存”。
2. 切到目标客户端登录界面，按 Alt+1 将自动输入：用户名 -> Tab -> 密码 -> Enter。

## 安全与合规声明
本项目不提供、也不指导任何“绕过客户端检测/驱动规避”的实现。若需扩展底层能力，请自建合规模块并自行负责法务与风控。

## 结构
- Services/HotkeyManager.cs：全局热键
- Input/KeyboardMouse.cs：输入封装（SendKeys）
- Security/CredentialStore.cs：凭证存储（DPAPI）
- Form1：UI 及接线

## 编译
使用 .NET 8 WinForms，直接构建运行即可：
```
dotnet build
```

