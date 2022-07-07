using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyComputerManager.Mvvm
{
    public abstract class AsyncCommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract Task ExecuteAsync();

        public abstract bool CanExecuteTask();

        [DebuggerStepThrough]
        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            this.Execute();
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        protected bool CanExecute()
        {
            return this.CanExecuteTask();
        }

        protected async void Execute()
        {
            if (this.CanExecuteTask() == false)
            {
                return;
            }

            this.RaiseCanExecuteChanged();

            try
            {
                await this.ExecuteAsync();
            }
            finally
            {
                this.RaiseCanExecuteChanged();
            }
        }
    }

    public abstract class AsyncCommandBase<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public abstract Task ExecuteAsync(T parameter);

        public abstract bool CanExecuteTask(T parameter);

        bool ICommand.CanExecute(object parameter)
        {
            if (typeof(T).IsValueType && parameter == null)
            {
                return false;
            }

            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (typeof(T).IsValueType && parameter == null)
            {
                return;
            }

            this.Execute((T)parameter);
        }

        protected bool CanExecute(T parameter)
        {
            return this.CanExecuteTask(parameter);
        }

        protected async void Execute(T parameter)
        {
            if (this.CanExecuteTask(parameter) == false)
            {
                return;
            }

            this.RaiseCanExecuteChanged();

            try
            {
                await this.ExecuteAsync(parameter);
            }
            finally
            {
                this.RaiseCanExecuteChanged();
            }
        }

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}