﻿namespace WalletWasabi.Fluent.ViewModels.Dialog
{
	/// <summary>
	/// Foundational class for <see cref="DialogViewModelBase{TResult}"/>.
	/// Don't use this class directly since it doesn't provide the
	/// functionality required for Dialogs.
	/// </summary>	
	public abstract class DialogViewModelBase : ViewModelBase
	{
		public DialogViewModelBase(IDialogHost dialogHost)
		{
			DialogHost = dialogHost;
		}

		/// <summary>
		/// An instance of <see cref="IDialogHost"/> that owns this dialog.
		/// </summary>
		protected IDialogHost DialogHost { get; }
	}
}
