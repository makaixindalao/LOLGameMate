// 作者: mkx
// 日期: 2025-09-16
// 说明: 使用 DPAPI (ProtectedData) 加密存储用户凭证到本地 AppData。
// 修改记录:
// - 2025-09-16 mkx: 首次创建。

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace LOLGameMate.Security
{
    /// <summary>
    /// 凭证存储（用户作用域）。
    /// 作者: mkx, 日期: 2025-09-16
    /// 修改记录: 2025-09-16 首次创建
    /// </summary>
    public sealed class CredentialStore
    {
        private readonly string _dir;
        private readonly string _filePath;

        public CredentialStore()
        {
            _dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LOLGameMate");
            _filePath = Path.Combine(_dir, "credentials.dat");
        }

        public void Save(Credential cred)
        {
            Directory.CreateDirectory(_dir);
            string json = JsonSerializer.Serialize(cred);
            byte[] plain = Encoding.UTF8.GetBytes(json);
            byte[] protectedBytes = ProtectedData.Protect(plain, null, DataProtectionScope.CurrentUser);
            File.WriteAllBytes(_filePath, protectedBytes);
        }

        public Credential? Load()
        {
            if (!File.Exists(_filePath)) return null;
            byte[] protectedBytes = File.ReadAllBytes(_filePath);
            byte[] plain = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
            string json = Encoding.UTF8.GetString(plain);
            return JsonSerializer.Deserialize<Credential>(json);
        }
    }

    /// <summary>
    /// 凭证模型。
    /// </summary>
    public sealed class Credential
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

