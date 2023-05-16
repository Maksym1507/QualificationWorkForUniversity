﻿using AutoMapper;
using Microsoft.Extensions.Options;
using QualificationWorkForUniversity.Configurations;
using QualificationWorkForUniversity.Data.Entities;
using QualificationWorkForUniversity.Models.Dtos.Catalog;

namespace QualificationWorkForUniversity.Mapping
{
    public class CatalogItemPictureResolver : IMemberValueResolver<CatalogEntity, CatalogItemDto, string, object>
    {
        private readonly CatalogConfig _config;

        public CatalogItemPictureResolver(IOptionsSnapshot<CatalogConfig> config)
        {
            _config = config.Value;
        }

        public object Resolve(CatalogEntity source, CatalogItemDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.Host}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}