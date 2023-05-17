﻿using QualificationWorkForUniversity.Models.Dtos.Catalog;
using QualificationWorkForUniversity.Repositories.Catalog.Abstractions;
using QualificationWorkForUniversity.Services.Catalog.Abstractions;

namespace QualificationWorkForUniversity.Services.Catalog
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogService> _loggerService;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository,
            IMapper mapper,
            ILogger<CatalogService> loggerService)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);

                if (!result.Data.Any())
                {
                    _loggerService.LogWarning($"Not founded catalog items on page = {pageIndex}, with page size = {pageSize}");
                    return null;
                }

                return new PaginatedItemsResponse<CatalogItemDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<CatalogItemDto?> GetCatalogItemByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogItemRepository.GetByIdAsync(id);

                if (result == null)
                {
                    _loggerService.LogWarning($"Not founded catalog item with Id = {id}");
                    return null;
                }

                return _mapper.Map<CatalogItemDto>(result);
            });
        }
    }
}