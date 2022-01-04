using ProsperousUniverse.API.DataLoaders;
using ProsperousUniverse.API.DTOs;
using ProsperousUniverse.API.Interfaces;
using ProsperousUniverse.API.JsonModels;

namespace ProsperousUniverse.API;

public sealed class Query
{
    [GraphQLName("userById")]
    public Task<UserDTO> GetUserById(string id, [Service] UserByIdDataLoader userByIdDataLoader)
        => userByIdDataLoader.LoadAsync(id);
    
    [GraphQLName("countryById")]
    public Task<CountryDTO> GetCountryById(string id, [Service] CountryByIdDataLoader countryByIdDataLoader)
        => countryByIdDataLoader.LoadAsync(id);
    
    [GraphQLName("companyById")]
    public Task<CompanyDTO> GetCompanyById(string id, [Service] CompanyByIdDataLoader companyByIdDataLoader)
        => companyByIdDataLoader.LoadAsync(id);


    [GraphQLName("countries")]
    [UsePaging]
    public Task<IEnumerable<CountryDTO>> GetCountries([Service] CountryRegistry countryRegistry)
    {
        return countryRegistry.GetCountries();
    }

    [GraphQLName("presentUsers")]
    [UsePaging]
    public IEnumerable<PresentUser> GetPresentUsers([Service] IServerInterface serverInterface)
    {
        return serverInterface.PresentUsers;
    }

    [GraphQLName("systemById")]
    public Task<SystemDTO> GetSystemById(string id, [Service] SystemByIdDataLoader systemByIdDataLoader)
        => systemByIdDataLoader.LoadAsync(id);

    [GraphQLName("planetById")]
    public Task<PlanetDTO> GetPlanetById(string id, [Service] PlanetByIdDataLoader planetByIdDataLoader)
        => planetByIdDataLoader.LoadAsync(id);

    [GraphQLName("corporationById")]
    public Task<CorporationDTO> GetCorporationById(string id, [Service] CorporationByIdDataLoader corporationByIdDataLoader)
        => corporationByIdDataLoader.LoadAsync(id);

    [GraphQLName("materialByTicker")]
    public Task<MaterialDTO> GetMaterialByTicker(string ticker, [Service] MaterialByTickerDataLoader materialByTickerDataLoader)
        => materialByTickerDataLoader.LoadAsync(ticker);

    [GraphQLName("materialById")]
    public Task<MaterialDTO> GetMaterialById(string id, [Service] MaterialByIdDataLoader materialByIdDataLoader)
        => materialByIdDataLoader.LoadAsync(id);

    [GraphQLName("reactorById")]
    public Task<ReactorDTO> GetReactorById(string id, [Service] ReactorByIdDataLoader reactorByIdDataLoader)
        => reactorByIdDataLoader.LoadAsync(id);

    [GraphQLName("populationById")]
    public Task<PopulationDTO> GetPopulationById(string id, [Service] PopulationByIdDataLoader populationByIdDataLoader)
        => populationByIdDataLoader.LoadAsync(id);

    [GraphQLName("materialCategories")]
    [UsePaging]
    public Task<IEnumerable<MaterialCategoryDTO>> GetMaterialCategories([Service] MaterialCategories materialCategories)
        => materialCategories.GetCategories();

    [GraphQLName("commodityExchanges")]
    [UsePaging]
    public Task<IEnumerable<ComexDTO>> GetCommodityExchanges([Service] CommodityExchangeList commodityExchangeList)
        => commodityExchangeList.GetCommodityExchanges();

    [GraphQLName("comexById")]
    public Task<ComexDTO> GetComexById(string id, [Service] ComexByIdDataLoader comexByIdDataLoader)
        => comexByIdDataLoader.LoadAsync(id);

    [GraphQLName("brokerByTicker")]
    public Task<BrokerDTO> GetBrokerByTicker(string ticker, [Service] BrokerByTickerDataLoader brokerByTickerDataLoader)
        => brokerByTickerDataLoader.LoadAsync(ticker);

    [GraphQLName("brokerCategoryByMaterialCategoryIdAndComexId")]
    [UsePaging]
    public Task<BrokerCategoryDTO> GetBrokerCategoryByMaterialCategoryIdAndComexId(
        string materialCategoryId,
        string comexId,
        [Service]
        BrokerCategoryByMaterialCategoryIdAndComexIdDataLoader brokerCategoryByMaterialCategoryIdAndComexIdDataLoader)
        => brokerCategoryByMaterialCategoryIdAndComexIdDataLoader.LoadAsync((materialCategoryId, comexId));

}
