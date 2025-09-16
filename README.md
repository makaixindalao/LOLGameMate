# LOLGameMate

作者: mkx
日期: 2025-09-16

## 简介
一个 Windows 游戏助手，支持多种输入模式：
- 全局热键（默认 Alt+1）
- 简单 UI 配置用户名/密码
- 可插拔的输入提供器架构：
  - SendKeys 模式（默认，合规）
  - DD 驱动模式（需要 dd.54900.dll）
- 凭证安全存储（DPAPI 加密，用户作用域）

## 使用
1. 启动程序，在主界面输入用户名与密码，点击"保存"。
2. **选择输入模式**：
   - **SendKeys 模式**：默认模式，使用系统 API
   - **DD 驱动模式**：点击"选择..."按钮，选择 dd.54900.dll 文件
3. 切到目标客户端登录界面，按 Alt+1 将自动输入：用户名 -> Tab -> 密码 -> Enter。

## DD 驱动说明
- DD 驱动模式使用第三方 dd.54900.dll 库实现底层键鼠控制
- 需要用户自行获取 dd.54900.dll 文件（项目中的 Lib 目录包含示例代码）
- 支持的 DD 码映射：Tab(300), Enter(284), A-Z(401-426) 等
- 使用前请确保遵守相关软件的使用条款和法律法规

## 安全与合规声明
本项目提供了可插拔的输入架构，用户可根据需要选择不同的输入实现。使用任何第三方驱动时，请自行承担合规和法律责任。

## 结构
- Services/HotkeyManager.cs：全局热键
- Input/KeyboardMouse.cs：输入封装（可插拔架构）
- Input/Providers/SendKeysInputProvider.cs：默认输入提供器（SendKeys）
- Input/Providers/DDInputProvider.cs：DD 驱动输入提供器
- Input/Providers/DDWrapper.cs：DD 库封装器
- Security/CredentialStore.cs：凭证存储（DPAPI）
- Form1：UI 及接线

## 编译
使用 .NET 8 WinForms，直接构建运行即可：
```
dotnet build
```

## 功能特性
- **可插拔架构**：支持多种输入实现，易于扩展
- **安全存储**：使用 Windows DPAPI 加密存储凭证
- **全局热键**：支持系统级热键注册
- **DD 驱动支持**：集成第三方 DD 库实现底层控制
- **简洁 UI**：直观的配置界面