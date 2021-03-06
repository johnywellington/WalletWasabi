using WalletWasabi.Fluent.ViewModels.Navigation;

namespace WalletWasabi.Fluent.ViewModels.AddWallet
{
	public class LegalDocumentsViewModel : RoutableViewModel
	{
		public LegalDocumentsViewModel(NavigationStateViewModel navigationState, string content) :
			base(navigationState)
		{
			Content = content;

			NextCommand = BackCommand;
		}

		public string Content { get; }
	}
}