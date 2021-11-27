using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    /// <summary>
    /// Интерфейс определяет функционал FileService
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DataList OpenFile(string path);
        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public void SaveFile(string path, DataList data);
    }
}
