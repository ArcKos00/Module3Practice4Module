using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLogger.Configs;
using Newtonsoft.Json;

namespace NewLogger.Services
{
    public class FileService : IFileService
    {
        private readonly int _count;
        private readonly string _config = "config.json";
        private readonly string _backupPath;
        private readonly string _backup = "Backup";
        private static readonly string _fileType = ".txt";
        private static readonly string _logFile = "Log" + _fileType;
        private static readonly StreamWriter _stream = new StreamWriter(_logFile);
        private static readonly SemaphoreSlim _slim = new SemaphoreSlim(1);

        public FileService()
        {
            var config = ReadJsonFile();
            _count = config.Count;
            _backupPath = Path.Combine(Directory.GetCurrentDirectory(), config.SomePath, _backup);
        }

        public int GetCount => _count;

        public async Task BackupAsync()
        {
            var backupFile = DateTime.Now.ToString("hh.mm.ss.ffff") + _fileType;
            if (!Directory.Exists(_backupPath))
            {
                Directory.CreateDirectory(_backupPath);
            }

            var path = Path.Combine(_backupPath, backupFile);
            await using (var writer = new StreamWriter(path))
            {
                using (var reader = new StreamReader(
                    new FileInfo(_logFile).Open(
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite)))
                {
                    writer.Write(reader.ReadToEnd());
                }
            }
        }

        public async Task WriteLogFileAsync(string message)
        {
            await _slim.WaitAsync();
            await _stream.WriteLineAsync(message);
            await _stream.FlushAsync();
            _slim.Release();
        }

        private Config ReadJsonFile()
        {
            var json = File.ReadAllText(_config);
            var config = JsonConvert.DeserializeObject<Config>(json);
            if (config != null)
            {
                return config;
            }

            return new Config() { Count = byte.MaxValue, SomePath = string.Empty };
        }
    }
}