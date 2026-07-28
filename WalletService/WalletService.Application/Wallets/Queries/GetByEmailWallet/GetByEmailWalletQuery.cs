using ErrorOr;
using MediatR;
using WalletService.Application.Wallets.Dtos;

namespace WalletService.Application.Wallets.Queries.GetByEmailWallet;
public sealed record GetByEmailWalletQuery(string Email) : IRequest<ErrorOr<WalletDto>>;
