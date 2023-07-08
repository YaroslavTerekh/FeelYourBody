using FYB.Data.Common;
using FYB.Data.Entities;
using LiqPay.SDK.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Abstractions;

public interface ILiqPayService
{
    public Task<string> GenerateForm(Guid goodId, Guid currentUserId, PurchaseProductType productType, CancellationToken cancellationToken = default);
    
    public LiqPayResponse DecodeResponse(Dictionary<string, string> data);

    public Task ProcessCallbackAsync(Dictionary<string, string> data, CancellationToken cancellationToken);
}
