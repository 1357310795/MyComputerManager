using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyComputerManager.Mvvm
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> execute;

        private readonly Func<bool> canExecute;

        public AsyncRelayCommand(Func<Task> execute)
            : this(execute, () => true)
        {
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override Task ExecuteAsync()
        {
            return this.execute();
        }

        public override bool CanExecuteTask()
        {
            return this.canExecute();
        }
    }

    public class AsyncRelayCommand<TParameter> : AsyncCommandBase<TParameter>
    {
        private readonly Func<TParameter, Task> execute;

        private readonly Predicate<TParameter> canExecute;

        public AsyncRelayCommand(Func<TParameter, Task> execute)
            : this(execute, param => true)
        {
        }

        public AsyncRelayCommand(Func<TParameter, Task> execute, Predicate<TParameter> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public override Task ExecuteAsync(TParameter parameter)
        {
            return this.execute(parameter);
        }

        public override bool CanExecuteTask(TParameter parameter)
        {
            return this.canExecute(parameter);
        }
    }
}