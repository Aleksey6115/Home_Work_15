using System;
using System.Windows.Input;

namespace MVVMWpfLibraryBase
{
    /// <summary>
    /// ����� ��� �������� �������
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> can_execute;

        public event EventHandler CanExecuteChanged // ������� ���������� ��� ��������� ������� ������� �������
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// ����������� ������ RelayCommand
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canexecute"></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canexecute = null)
        {
            this.execute = execute;
            this.can_execute = canexecute;
        }

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) // ������� ���������� ��������
        {
            return this.can_execute == null || this.can_execute(parameter);
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) // �������� ��������
        {
            this.execute(parameter);
        }
    }
}
