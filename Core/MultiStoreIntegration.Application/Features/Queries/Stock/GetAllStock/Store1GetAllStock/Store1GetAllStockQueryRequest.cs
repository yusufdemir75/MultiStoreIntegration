using MediatR;
using MultiStoreIntegration.Application.Features.Queries.Stock.GetAllStock.Store1GetAllStock;


namespace MultiStoreIntegration.Application.Features.Queries.Stock.GetAll.Store1GetAll
{
    public class Store1GetAllStockQueryRequest:IRequest<Store1GetAllStockQueryResponse>
    {
    }
}
