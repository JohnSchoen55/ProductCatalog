using AutoMapper;
using ProductCatalog.Core.Dtos;
using ProductCatalog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Api.Extensions
{
    public static class AutomapperConfigurationExtension
    {
        public static void ConfigureAutomapperAPI(this IMapperConfigurationExpression config)
        {
            config.CreateMap<Products, ProductDto>();
            config.CreateMap<AddProductDto, Products>();
            config.CreateMap<PriceHistory, PriceHistoryDto>();
        }
    }
}
