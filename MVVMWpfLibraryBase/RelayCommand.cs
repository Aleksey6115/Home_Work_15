using System;
using System.Windows.Input;

namespace MVVMWpfLibraryBase
{
    /// <summary>
    /// Класс для создания комманд
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> can_execute;

        public event EventHandler CanExecuteChanged // События вызывается при изменении условий запуска команды
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Конструктор класса RelayCommand
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canexecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canexecute = null)
        {
            this.execute = execute;
            this.can_execute = canexecute;
        }

        /// <summary>
        /// Условие выполнение команды
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) // Условие выполнение комманды
        {
            return this.can_execute == null || this.can_execute(parameter);
        }

        /// <summary>
        /// Действие комманды
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) // Действие комманды
        {
            this.execute(parameter);
        }
    }
}
