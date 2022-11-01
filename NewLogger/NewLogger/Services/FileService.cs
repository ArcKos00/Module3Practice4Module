using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using NewLogger.Models;

namespace NewLogger.Services
{
    public class FileService : IFileService
    {
        private readonly int _count;
        private readonly string _backupFormat;
        private readonly string _backupPath;
        private readonly string _logFile;
        private readonly string _fileType;
        private readonly StreamWriter _stream;
        private static readonly SemaphoreSlim _slim = new SemaphoreSlim(1);

        public FileService(Config config)
        {
            _count = config.Count;
            _backupPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + Path.Combine(config.BackupPath, config.BackupFolder);
            _fileType = config.LogFileType;
            _logFile = config.NameLogFile + config.LogFileType;
            _backupFormat = config.FormatBackupFile;
            _stream = new StreamWriter(_logFile);
        }

        public int GetCount => _count;

        public async Task BackupAsync()
        {
            var backupFile = DateTime.Now.ToString(_backupFormat) + _fileType;
            if (!Directory.Exists(_backupPath))
            {
                Directory.CreateDirectory(_backupPath);
            }

            using (var writer = new StreamWriter(Path.Combine(_backupPath, backupFile)))
            {
                using (var reader = new StreamReader(
                    new FileInfo(_logFile).Open(
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite)))
                {
                    await writer.WriteAsync(reader.ReadToEnd());
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
    }
}