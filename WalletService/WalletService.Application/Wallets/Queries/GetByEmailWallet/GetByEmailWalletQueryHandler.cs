using ErrorOr;
using MediatR;
using WalletService.Application.Wallets.Dtos;

namespace WalletService.Application.Wallets.Queries.GetByEmailWallet;

public sealed class GetByEmailWalletQueryHandler : IRequestHandler<GetByEmailWalletQuery, ErrorOr<WalletDto>>
{
    private readonly IWalletRepository _walletRepository;

    public GetByEmailWalletQueryHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<ErrorOr<WalletDto>> Handle(GetByEmailWalletQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (wallet == null)
        {
            return Error.NotFound("Wallet.NotFound", $"Wallet with email {request.Email} not found.");
        }

        return new WalletDto(
            wallet.Id.Value,
            wallet.Name,
            wallet.LastName,
            wallet.Document.Type.ToString(),
            wallet.Document.Number,
            wallet.Email.ToString(),
            wallet.Phone.ToString(),
            wallet.WalletStatus.ToString(),
            wallet.WalletLimit.Currency.ToString(),
            wallet.WalletLimit.DailyLimit,
            wallet.WalletBalance.BalanceAmount,
            wallet.WalletLimit.Id.Value
        );
    }
}
