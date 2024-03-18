using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Payment.Bank.Common.Abstractions.Queries;
using OneOf;
using OneOf.Types;

namespace Payment.Bank.Application.Accounts.Features.GetAccount.v1;

[ExcludeFromCodeCoverage]
public sealed record GetAccountQuery(int AccountNumber) : IQuery<OneOf<GetAccountResponse, ReadOnlyCollection<ValidationFailure>, NotFound>>;
