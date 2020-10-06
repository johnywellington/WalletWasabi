using System;
using System.Threading.Tasks;

namespace WalletWasabi.Fluent.ViewModels.Dialog
{
	/// <summary>
	/// Base ViewModel class for Dialogs that returns a value back.
	/// </summary>
	/// <typeparam name="TResult">The type of the value to be returned when the dialog is finished.</typeparam>
	public abstract class DialogViewModelBase<TResult> : DialogViewModelBase
	{
		public DialogViewModelBase(IDialogHost dialogViewModel) : base(dialogViewModel)
		{
		}

		private TaskCompletionSource<TResult> CurrentTaskCompletionSource { get; set; } 
		
		/// <summary>
		/// Method to be called when the dialog intends to close
		/// and ready to pass a value back to the caller.
		/// </summary>
		/// <param name="value">The return value of the dialog</param>
		public void CloseDialog(TResult value)
		{
			CurrentTaskCompletionSource.SetResult(value);
			CurrentTaskCompletionSource = null;
			DialogHost.CloseDialog();
		}

		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <returns>The value to be returned when the dialog is finished.</returns>
		public Task<TResult> ShowDialogAsync()
		{
			if (CurrentTaskCompletionSource is { })
			{
				throw new InvalidOperationException("Can't open a new dialog since there's already one active.");
			}
			
			CurrentTaskCompletionSource = new TaskCompletionSource<TResult>();

			DialogHost.ShowDialog(this);
			DialogShown();

			return CurrentTaskCompletionSource.Task;
		}

		/// <summary>
		/// Method that is triggered when the dialog
		/// is to be shown.
		/// </summary>
		protected abstract void DialogShown();
	}
}
