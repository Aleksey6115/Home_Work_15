using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace MVVMWpfLibraryBase
{
    /// <summary>
    /// Базовый класс для ViewModel
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // Оповещение внешних клиентов об изменениях

        /// <summary>
        /// Генерирование события PropertyChanged
        /// </summary>
        /// <param name="prop"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        /// <summary>
        /// Проверка на то, что свойство действительно изменилось, 
        /// Если нет события PropertyChanged не генерируется
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string prop = "")
        {
            if (Equals(field, value)) return false; // Св-во не было измененно

            else // Св-во было измененно
            {
                field = value;
                OnPropertyChanged(prop);
                return true;
            }
        }
    }
}
