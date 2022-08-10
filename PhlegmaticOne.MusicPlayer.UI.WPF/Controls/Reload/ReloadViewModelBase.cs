using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using Calabonga.UnitOfWork;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public abstract class ReloadViewModelBase<T> : BaseViewModel where T: BaseViewModel
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;

    protected ReloadViewModelBase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
        ReloadCommand = new DelegateCommand(ReloadAction, _ => true);
    }
    public ICommand ReloadCommand { get; }
    protected abstract Task ReloadViewModel(T viewModel);

    private async void ReloadAction(object? parameter)
    {
        if (parameter is T viewModel)
        {
            await ReloadViewModel(viewModel);
        }
    }
}