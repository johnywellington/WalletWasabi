using System.IO;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using WalletWasabi.Fluent.ViewModels.Navigation;
using WalletWasabi.Legal;

namespace WalletWasabi.Fluent.ViewModels.AddWallet
{
	public class TermsAndConditionsViewModel : RoutableViewModel
	{
		private bool _isAgreed;

		public TermsAndConditionsViewModel(NavigationStateViewModel navigationState, LegalDocuments legalDocuments, RoutableViewModel next) : base(navigationState)
		{
			ViewTermsCommand = ReactiveCommand.CreateFromTask(
				async () =>
				{
					var content = await File.ReadAllTextAsync(legalDocuments.FilePath);

					var legalDocs = new LegalDocumentsViewModel(
						navigationState,
						content);

					legalDocs.NavigateToSelf(CurrentTarget);
				});

			NextCommand = ReactiveCommand.Create(
				() =>
				{
					NavigateTo(next);
				},
				this.WhenAnyValue(x => x.IsAgreed).ObserveOn(RxApp.MainThreadScheduler));
		}

		public bool IsAgreed
		{
			get => _isAgreed;
			set => this.RaiseAndSetIfChanged(ref _isAgreed, value);
		}

		public ICommand ViewTermsCommand { get; }
	}
}